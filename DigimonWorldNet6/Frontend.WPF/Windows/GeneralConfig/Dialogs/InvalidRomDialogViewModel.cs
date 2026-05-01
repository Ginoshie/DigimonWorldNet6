using System;
using System.IO;
using NAudio.Wave;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Dialogs;

public class InvalidRomDialogViewModel
{
    public InvalidRomDialogViewModel()
    {
        try
        {
            string sfxPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SFX", "nani.mp3");

            if (!File.Exists(sfxPath))
            {
                return;
            }

            AudioFileReader audioFile = new(sfxPath);
            WaveOutEvent soundOut = new();
            soundOut.Init(audioFile);
            soundOut.Volume = 1.0f;
            soundOut.PlaybackStopped += (_, _) =>
            {
                soundOut.Dispose();
                audioFile.Dispose();
            };
            soundOut.Play();
        } catch (Exception)
        {
            // Sound playback is non-critical
        }
    }
}

