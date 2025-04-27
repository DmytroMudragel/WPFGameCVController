using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ProjectYZ
{
    public static class CursorClassifier
    {
        public static Bitmap Classify()
        {
            var result = new Bitmap(32, 32);
            try
            {
                Utils.CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(Utils.CURSORINFO));

                using (var g = Graphics.FromImage(result))
                {
                    if (Utils.GetCursorInfo(out pci))
                    {
                        if (pci.flags == Utils.CURSOR_SHOWING)
                        {
                            var hdc = g.GetHdc();
                            Utils.DrawIconEx(hdc, 0, 0, pci.hCursor, 0, 0, 0, IntPtr.Zero, Utils.DI_NORMAL);
                            g.ReleaseHdc();
                        }
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static class ImageHashing
        {
            private static readonly byte[] bitCounts = {
            0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,1,2,2,3,2,3,3,4,
            2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,
            4,5,5,6,5,6,6,7,1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,2,3,3,4,3,4,4,5,
            3,4,4,5,4,5,5,6,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8
        };

            private static uint BitCount(ulong num)
            {
                uint count = 0;
                for (; num > 0; num >>= 8)
                {
                    count += bitCounts[num & 0xff];
                }

                return count;
            }

            public static ulong AverageHash(Image image)
            {
                // Squeeze the image into an 8x8 canvas
                Bitmap squeezed = new Bitmap(8, 8, PixelFormat.Format32bppRgb);
                Graphics canvas = Graphics.FromImage(squeezed);
                canvas.CompositingQuality = CompositingQuality.HighQuality;
                canvas.InterpolationMode = InterpolationMode.HighQualityBilinear;
                canvas.SmoothingMode = SmoothingMode.HighQuality;
                canvas.DrawImage(image, 0, 0, 8, 8);

                // Reduce colors to 6-bit grayscale and calculate average color value
                byte[] grayscale = new byte[64];
                uint averageValue = 0;
                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                    {
                        uint pixel = (uint)squeezed.GetPixel(x, y).ToArgb();
                        uint gray = (pixel & 0x00ff0000) >> 16;
                        gray += (pixel & 0x0000ff00) >> 8;
                        gray += (pixel & 0x000000ff);
                        gray /= 12;

                        grayscale[x + (y * 8)] = (byte)gray;
                        averageValue += gray;
                    }
                averageValue /= 64;

                // Compute the hash: each bit is a pixel
                // 1 = higher than average, 0 = lower than average
                ulong hash = 0;
                for (int i = 0; i < 64; i++)
                {
                    if (grayscale[i] >= averageValue)
                    {
                        hash |= 1UL << (63 - i);
                    }
                }

                squeezed.Dispose();

                return hash;
            }

            public static int AveragePseudoHash(Bitmap image)
            {
                // Squeeze the image into an 8x8 canvas
                int sum = 0;
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        sum += image.GetPixel(i, j).R + image.GetPixel(i, j).G + image.GetPixel(i, j).B;
                    }
                }

                return sum;
            }

            public static ulong AverageHash(String path)
            {
                Bitmap bmp = new Bitmap(path);
                ulong result = AverageHash(bmp);
                bmp.Dispose();
                return result;
            }

            public static double Similarity(ulong hash1, ulong hash2)
            {
                return (64 - BitCount(hash1 ^ hash2)) * 100 / 64.0;
            }

            public static double Similarity(Image image1, Image image2)
            {
                ulong hash1 = AverageHash(image1);
                Console.WriteLine(hash1);
                ulong hash2 = AverageHash(image2);
                return Similarity(hash1, hash2);
            }

            public static double Similarity(String path1, String path2)
            {
                ulong hash1 = AverageHash(path1);
                ulong hash2 = AverageHash(path2);
                return Similarity(hash1, hash2);
            }
        }
    }

    public static class HashConstants
    {
        public static readonly List<ulong> ImageHashes = new List<ulong>()
        {
            63174056523730445,   //0 Sword
            4675976922113260568, //1 Loot
            4678235606810050048, //2 Skin
            5834514740734212096, //3 Vendor
            33843510126706688,   //4 Hand
            4669140166357294088, //5 Repair
            18154583951900416,   //6 Mail

            6981982659047865368, //7 Loot not in range
            5834514740700526592, //8 Vendor not in range 
            4678358471844567048, //9 Repair not in range
            63174056523730445, //10 Sword not in range
            4678235606810050048, //11 Skinning not in range
            4683320813727784992, //12 Herb

        };
    }
}