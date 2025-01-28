using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using DigimonWorld.Frontend.WPF.Configuration;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public class SpeakingSimulator : IDisposable
{
    private readonly object _lock = new();
    private readonly CompositeDisposable _compositeDisposable;

    private CancellationTokenSource? _typingCancellationTokenSource;
    private bool _instantDisplayRequested;
    private SpeakingSimulatorConfig _speakingSimulatorConfig = null!;

    public SpeakingSimulator()
    {
        _compositeDisposable = new CompositeDisposable
        (
            GeneralConfigurationManager.CurrentSpeakingSimulatorConfig.Subscribe(OnConfigChanged)
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
        if (_typingCancellationTokenSource?.IsCancellationRequested != false) return;

        _instantDisplayRequested = true;
        _typingCancellationTokenSource.Cancel();
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
            if (_typingCancellationTokenSource != null)
            {
                _typingCancellationTokenSource.Cancel();
                _typingCancellationTokenSource.Dispose();
            }

            _typingCancellationTokenSource = new CancellationTokenSource();
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
            // Task canceled: swallow the exception as it's expected behavior
        }
    }

    private async Task WriteTextAsSpeech(string fullText, Action<string> updateTextAction, Action? showEvolutionAction)
    {
        CancellationToken cancellationToken = _typingCancellationTokenSource!.Token;
        string currentText = string.Empty;

        try
        {
            string[] words = fullText.Split(' ');

            if (_speakingSimulatorConfig.NarratorMode == NarratorMode.Instant)
            {
                updateTextAction(fullText.Replace(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord, ""));
                showEvolutionAction?.Invoke();

                return;
            }

            if (!words.Contains(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord))
            {
                showEvolutionAction?.Invoke();
            }

            foreach (string word in words)
            {
                cancellationToken.ThrowIfCancellationRequested();

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
        catch (OperationCanceledException)
        {
            if (_instantDisplayRequested)
            {
                updateTextAction(fullText.Replace(JijimonEvolutionCalculatorNarratorText.ShowEvolutionResultKeyWord, ""));
                showEvolutionAction?.Invoke();
            }
        }
        finally
        {
            _instantDisplayRequested = false;
        }
    }

    public void Dispose()
    {
        _typingCancellationTokenSource?.Dispose();
        _compositeDisposable.Dispose();
    }
}