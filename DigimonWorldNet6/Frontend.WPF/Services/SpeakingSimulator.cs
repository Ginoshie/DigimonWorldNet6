using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigimonWorld.Frontend.WPF.Services;

public class SpeakingSimulator
{
    private CancellationTokenSource? _typingCancellationTokenSource;
    private bool _instantDisplayRequested;
    private readonly object _lock = new();

    public static string ShowEvolutionResultKeyWord => "ShowEvolutionResult";

    public async Task WriteTextAsSpeechAsync(string fullText, Action<string> updateTextAction)
    {
        CancelAndReset();
        await StartNewSpeechTask(() => WriteEvolutionTextAsSpeechInternal(fullText, updateTextAction, null));
    }

    public async Task WriteEvolutionTextAsSpeech(string fullText, Action<string> updateTextAction, Action? showEvolutionAction)
    {
        CancelAndReset();
        await StartNewSpeechTask(() => WriteEvolutionTextAsSpeechInternal(fullText, updateTextAction, showEvolutionAction));
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

    private async Task WriteEvolutionTextAsSpeechInternal(string fullText, Action<string> updateTextAction, Action? showEvolutionAction)
    {
        CancellationToken cancellationToken = _typingCancellationTokenSource!.Token;
        string currentText = string.Empty;

        try
        {
            string[] words = fullText.Split(' ');

            if (!words.Contains(ShowEvolutionResultKeyWord))
            {
                showEvolutionAction?.Invoke();
            }

            foreach (string word in words)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (word == ShowEvolutionResultKeyWord)
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
                updateTextAction(fullText.Replace(ShowEvolutionResultKeyWord, ""));
                showEvolutionAction?.Invoke();
            }
        }
        finally
        {
            _instantDisplayRequested = false;
        }
    }

    public void RequestInstantDisplay()
    {
        if (_typingCancellationTokenSource?.IsCancellationRequested != false) return;
        
        _instantDisplayRequested = true;
        _typingCancellationTokenSource.Cancel();
    }
}