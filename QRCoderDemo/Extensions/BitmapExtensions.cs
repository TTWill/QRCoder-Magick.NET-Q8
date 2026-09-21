using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;

namespace QRCoderDemo.Extensions;

public static class BitmapExtensions
{
    public static MagickImage ToMagickImage(this Bitmap bitmap)
    {
        using var memoryStream = new MemoryStream();
        bitmap.Save(memoryStream, ImageFormat.Png);
        memoryStream.Position = 0;
        return new MagickImage(memoryStream);
    }
}
