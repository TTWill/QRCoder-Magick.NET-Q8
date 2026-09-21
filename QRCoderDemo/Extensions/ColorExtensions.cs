using System.Drawing;
using ImageMagick;

namespace QRCoderDemo.Extensions;

public static class ColorExtensions
{
    public static MagickColor ToMagickColor(this System.Drawing.Color color)
        => new(color.R, color.G, color.B, color.A);

    public static Color ToDrawingColor(this MagickColor color)
        => Color.FromArgb(color.A, color.R, color.G, color.B);
}
