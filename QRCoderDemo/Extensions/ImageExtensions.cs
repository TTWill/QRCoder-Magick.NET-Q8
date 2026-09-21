using System.Drawing;

namespace QRCoderDemo.Extensions;

public static class ImageExtensions
{
    public static Image ToImage(this ImageMagick.MagickImage magickImage)
    {
        using var memoryStream = new MemoryStream();
        magickImage.Write(memoryStream);
        memoryStream.Position = 0;
        return Image.FromStream(memoryStream);
    }
}
