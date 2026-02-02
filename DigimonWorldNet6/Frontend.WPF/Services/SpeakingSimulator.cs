using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using DigimonWorld.Frontend.WPF.Constants;
using Shared.Configuration;
using Shared.Enums;
using Shared.Services;

namespace DigimonWorld.Frontend.WPF.Services;

public class SpeakingSimulator : IDisposable
{
    private readonly Lock _lock = new();
    private readonly CompositeDisposable _compositeDisposable;

    private CancellationTokenSource? _typingCancellationTokenSource;
    private volatile bool _instantDisplayRequested;
    private SpeakingSimulatorConfig _speakingSimulatorConfig = null!;

    public SpeakingSimulator()
    {
        _compositeDisposable = new CompositeDisposable
        (
            UserConfigurationManager.CurrentSpeakingSimulatorConfig.Subscribe(OnConfigChanged)
        );
    }

    public async Task WriteTextAsSpeechAsync(string fullText, Action<string> updateTextAction)
    {
        CancelAndReset();
        await StartNewSpeechTask(() => WriteTextAsSpeech(fullText, updateTextAction, null));
    }

    public async Task WriteEvolutionTextAsSpeech(string fullText, Action<string> updateTextAction, Action? showEvolutionAction)
    {
        CancelAndReset();
        await StartNewSpeechTask(() => WriteTextAsSpeech(fullText, updateTextAction, showEvolutionAction));
    }

    public void RequestInstantDisplay()
    {
        _instantDisplayRequested = true;
    }

    private void OnConfigChanged(SpeakingSimulatorConfig speakingSimulatorConfig)
    {
        if (speakingSimulatorConfig.NarratorMode == NarratorMode.Instant)
        {
            RequestInstantDisplay();
        }

        _speakingSimulatorConfig = speakingSimulatorConfig;
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

    private async Task StartNewSpeechTask(Func<Task> speechTask)
    {
        try
        {
            await speechTask();
        }
        catch (OperationCanceledException)
        {
            // Expected cancellation
        }
    }

    private async Task WriteTextAsSpeech(string fullText, Action<string> updateTextAction, Action? showEvolutionAction)
    {
        CancellationToken cancellationToken = _typingCancellationTokenSource!.Token;
        string currentText = string.Empty;

        string cleanText = fullText.Replace(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord, "");

        if (_speakingSimulatorConfig.NarratorMode == NarratorMode.Instant)
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

        foreach (string word in words)
        {
            if (_instantDisplayRequested || cancellationToken.IsCancellationRequested)
            {
                updateTextAction(cleanText);
                showEvolutionAction?.Invoke();
                return;
            }

            if (word == JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord)
            {
                showEvolutionAction?.Invoke();
                await Task.Delay(500, cancellationToken);
                continue;
            }

            currentText += word + " ";
            updateTextAction(currentText);

            string lastFiveChars = currentText.Length >= 6
                ? currentText[^6..]
                : currentText;

            if (lastFiveChars.Contains(". . ."))
            {
                await Task.Delay(800, cancellationToken);
            }
            else
            {
                await Task.Delay(150, cancellationToken);
            }
        }
    }

    public void Dispose()
    {
        _typingCancellationTokenSource?.Dispose();
        _compositeDisposable.Dispose();
    }
}