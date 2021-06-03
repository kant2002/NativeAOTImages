using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace NativeAOTImages
{
    class Program
    {
        static void Main(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Magic string which make System.Drawing.Common works.
                ComWrappers.RegisterForMarshalling(WinFormsComInterop.WinFormsComWrappers.Instance);
            }

            int width = 128;
            int height = 128;
            var file = args[0];
            Console.WriteLine($"Loading {file}");
            using var pngStream = new FileStream(args[0], FileMode.Open, FileAccess.Read);
            using var image = new Bitmap(pngStream);
            var resized = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(resized);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.DrawImage(image, 0, 0, width, height);
            resized.Save($"resized-{file}", ImageFormat.Png);
            Console.WriteLine($"Saving resized-{file} thumbnail");

            // ImageSharp part.
            using (SixLabors.ImageSharp.Image<Rgba32> image2 = SixLabors.ImageSharp.Image.Load<Rgba32>(file))
            {
                image2.Mutate(x => x
                    .Resize(image2.Width / 2, image2.Height / 2)
                    .Grayscale());
                image2.Save($"resized-sharp-{file}"); // Automatic encoder selected based on extension.
                Console.WriteLine($"Saving resized-sharp-{file} thumbnail using ImageSharp");
            }
        }
    }

}
