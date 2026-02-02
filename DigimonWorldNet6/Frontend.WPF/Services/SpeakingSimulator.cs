using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using DigimonWorld.Frontend.WPF.Constants;
using DigimonWorld.Frontend.WPF.Enums;
using Shared.Configuration;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Services;

public sealed class SpeakingSimulator : IDisposable
{
    private readonly Lock _lock = new();
    private readonly CompositeDisposable _compositeDisposable;

    private CancellationTokenSource? _typingCancellationTokenSource;
    private volatile bool _instantDisplayRequested;
    private volatile bool _configInstantMode;

    public SpeakingSimulator()
    {
        _compositeDisposable = new CompositeDisposable(
            UserConfigurationManager.CurrentSpeakingSimulatorConfig.Subscribe(OnConfigChanged)
        );
    }

    public async Task SpeakAsync(
        string fullText,
        Action<string> updateTextAction,
        SpeechDelay delay = SpeechDelay.None,
        Action? showEvolutionAction = null)
    {
        CancelAndReset();

        CancellationToken token = _typingCancellationTokenSource!.Token;

        try
        {
            int delayMs = ResolveDelay(delay);

            if (delayMs > 0)
            {
                await DelayResponsiveAsync(delayMs, token);
            }

            await WriteTextAsSpeech(
                fullText,
                updateTextAction,
                showEvolutionAction,
                token);
        }
        catch (OperationCanceledException)
        {
            // expected
        }
    }


    public void RequestInstantDisplay()
    {
        _instantDisplayRequested = true;
    }
    
    private int ResolveDelay(SpeechDelay delay) => delay switch
    {
        SpeechDelay.None => 0,
        SpeechDelay.Short => 600,
        SpeechDelay.Long => 1500,
        _ => 0
    };


    private void OnConfigChanged(SpeakingSimulatorConfig config)
    {
        if (config.NarratorMode == NarratorMode.Instant)
        {
            _configInstantMode = true;
            _instantDisplayRequested = true;
        }
        else
        {
            _configInstantMode = false;
        }
    }

    private void CancelAndReset()
    {
        lock (_lock)
        {
            _typingCancellationTokenSource?.Cancel();
            _typingCancellationTokenSource?.Dispose();

            _typingCancellationTokenSource = new CancellationTokenSource();
            _instantDisplayRequested = false;
        }
    }

    private async Task WriteTextAsSpeech(
        string fullText,
        Action<string> updateTextAction,
        Action? showEvolutionAction,
        CancellationToken token)
    {
        string cleanText = fullText.Replace(
            JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord,
            string.Empty);

        if (_configInstantMode)
        {
            updateTextAction(cleanText);
            showEvolutionAction?.Invoke();
            return;
        }

        string[] words = fullText.Split(' ');

        if (!words.Contains(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord))
        {
            showEvolutionAction?.Invoke();
        }

        string currentText = string.Empty;

        foreach (string word in words)
        {
            token.ThrowIfCancellationRequested();

            if (_instantDisplayRequested)
            {
                updateTextAction(cleanText);
                showEvolutionAction?.Invoke();
                return;
            }

            if (word == JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord)
            {
                showEvolutionAction?.Invoke();
                await DelayResponsiveAsync(500, token);
                continue;
            }

            currentText += word + " ";
            updateTextAction(currentText);

            bool hasEllipsis =
                currentText.Length >= 6 &&
                currentText[^6..].Contains(". . .");

            await DelayResponsiveAsync(hasEllipsis ? 800 : 150, token);
        }

        updateTextAction(cleanText);
    }

    private async Task DelayResponsiveAsync(int milliseconds, CancellationToken token)
    {
        const int slice = 20;
        int elapsed = 0;

        while (elapsed < milliseconds)
        {
            token.ThrowIfCancellationRequested();

            if (_instantDisplayRequested)
            {
                return;
            }

            int delay = Math.Min(slice, milliseconds - elapsed);
            await Task.Delay(delay, token);
            elapsed += delay;
        }
    }

    public void Dispose()
    {
        _typingCancellationTokenSource?.Dispose();
        _compositeDisposable.Dispose();
    }
}