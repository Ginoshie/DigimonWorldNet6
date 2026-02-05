using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DigimonWorld.Frontend.WPF.Windows.GeneralConfig.Utility;

public class ProcessIconHelper
{
    public static ImageSource? GetIcon(Process process)
    {
        try
        {
            string? path = process.MainModule?.FileName;
            
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                return null;
            }

            using Icon? icon = Icon.ExtractAssociatedIcon(path);
            if (icon == null)
            {
                return null;
            }

            using Bitmap bmp = icon.ToBitmap();
            using MemoryStream ms = new();

            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;

            BitmapImage image = new();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();

            return image;
        }
        catch
        {
            return null;
        }
    }
}