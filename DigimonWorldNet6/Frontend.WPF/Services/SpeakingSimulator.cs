using System;
using System.Threading;
using System.Threading.Tasks;

namespace DigimonWorld.Frontend.WPF.Services;

public static class SpeakingSimulator
{
    private static CancellationTokenSource _typingCancellationTokenSource = new();
    private static bool _speechIsActive;
    private static bool _instantDisplayRequested;

    private static readonly char[] Separator = { ' ' };

    public static async Task WriteAsSpeech(string fullText, Action<string> updateTextAction)
    {
        await _typingCancellationTokenSource.CancelAsync();
        
        _speechIsActive = true;
        
        _typingCancellationTokenSource.Dispose();
        _typingCancellationTokenSource = new CancellationTokenSource();

        CancellationToken cancellationToken = _typingCancellationTokenSource.Token;

        string currentText = string.Empty;

        try
        {
            string[] words = fullText.Split(Separator, StringSplitOptions.None);

            foreach (string word in words)
            {
                // If instant display is requested, immediately update with the full text.
                if (_instantDisplayRequested)
                {
                    updateTextAction(fullText);
                    return;
                }

                cancellationToken.ThrowIfCancellationRequested();

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
            // Handle cancellation gracefully
            if (_instantDisplayRequested)
            {
                updateTextAction(fullText);
            }
        }
        finally
        {
            _instantDisplayRequested = false; // Reset for next usage
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