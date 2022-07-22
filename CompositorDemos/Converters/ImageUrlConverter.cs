using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace CompositorDemos.Converters;

public class ImageUrlConverter : IValueConverter
{
    public static readonly IValueConverter Instance = new ImageUrlConverter();

    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string imageUrl)
        {
            try
            {
                return GetBitmap(imageUrl);
            }
            catch
            {
                // ignored
            }
        }

        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static Bitmap GetBitmap(string imageUrl)
    {
        return GetBitmapAsset(new Uri($"avares://CompositorDemos{imageUrl}"));
    }

    private static Bitmap GetBitmapAsset(Uri uri)
    {
        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

        if (assets is { })
        {
            using var image = assets.Open(uri);
            return new Bitmap(image);
        }

        throw new Exception($"Failed to load asset from {uri}.");
    }

    private static Bitmap GetBitmapAsset(string path)
    {
        return GetBitmapAsset(new Uri(path));
    }
}
