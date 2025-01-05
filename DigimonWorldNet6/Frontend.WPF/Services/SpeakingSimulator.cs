using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DigimonWorld.Frontend.WPF.Constants;

namespace DigimonWorld.Frontend.WPF.Services;

public static class SpeakingSimulator
{
    private static CancellationTokenSource _typingCancellationTokenSource = new();
    private static bool _speechIsActive;
    private static bool _instantDisplayRequested;

    public static async Task WriteEvolutionTextAsSpeech(string fullText, Action<string> updateTextAction, Action showEvolutionAction)
    {
        await _typingCancellationTokenSource.CancelAsync();

        _speechIsActive = true;

        _typingCancellationTokenSource.Dispose();
        _typingCancellationTokenSource = new CancellationTokenSource();

        CancellationToken cancellationToken = _typingCancellationTokenSource.Token;

        string currentText = string.Empty;

        try
        {
            string[] words = fullText.Split(JijimonNarratorText.SeparatorChar);

            if (!words.Contains(JijimonNarratorText.ShowEvolutionResultKeyWord))
            {
                showEvolutionAction.Invoke();
            }

            foreach (string word in words)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (word == JijimonNarratorText.ShowEvolutionResultKeyWord)
                {
                    showEvolutionAction.Invoke();

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
                updateTextAction(fullText.Replace(JijimonNarratorText.ShowEvolutionResultKeyWord, ""));

                showEvolutionAction.Invoke();
            }
        }
        finally
        {
            _instantDisplayRequested = false;
            _speechIsActive = false;
        }
    }

    public static void RequestInstantDisplay()
    {
        if (!_speechIsActive) return;

        _instantDisplayRequested = true;
        _typingCancellationTokenSource.Cancel();
    }
}