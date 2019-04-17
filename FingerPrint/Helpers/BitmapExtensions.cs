﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FingerPrint
{

    public static class BitmapExtensions
    {
        /// <summary>
        /// Get histogram of given channel
        /// </summary>
        /// <param name="bitmap">Extension type</param>
        /// <param name="color">Channel for which histogram is caluclated</param>
        /// <returns>Dictionary which key contains number of pixel's intensivity, value contains sums of given pixel's intensivity</returns>
        public static Dictionary<int, int> GetHistogramRgb(this Bitmap bitmap, RGB color)
        {
            Dictionary<int, int> histogramRgb = new Dictionary<int, int>();
            for (int i = 0; i < 256; i++)
            {
                histogramRgb.Add(i, 0);
            }

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var p = bitmap.GetPixel(x, y);
                    switch (color)
                    {
                        case RGB.R:
                            histogramRgb[p.R]++;
                            break;

                        case RGB.G:
                            histogramRgb[p.G]++;
                            break;

                        case RGB.B:
                            histogramRgb[p.B]++;
                            break;
                    }
                }
            }

            return histogramRgb;
        }
        /// <summary>
        /// Get histogram for all channels
        /// </summary>
        /// <param name="bitmap">Extension type</param>
        /// <returns>Dictionary which key contains number of pixel's intensivity, value contains sums of given pixel's intensivity</returns>
        public static Dictionary<int, int> GetHistogram(this Bitmap bitmap)
        {
            Dictionary<int, int> histogram = new Dictionary<int, int>();
            for (int i = 0; i < 256; i++)
            {
                histogram.Add(i, 0);
            }

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    var p = bitmap.GetPixel(x, y);
                    var grey = (p.R + p.G + p.B) / 3;
                    histogram[grey]++;
                }
            }

            return histogram;
        }
        /// <summary>
        /// Convert Bitmap to BitmapSource
        /// </summary>
        /// <param name="bitmap">Extension type</param>
        /// <returns>BitmapSource of given <para>bitmap</para></returns>
        public static BitmapSource GetBitmapSource(this Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHbitmap();
            try
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(bitmap.Width, bitmap.Height)
                );
            }
            finally
            {
                DeleteObject(ptr);
            }
        }
        /// <summary>
        /// Convert Bitmap to bytes array.
        /// </summary>
        /// <param name="img">Extension type</param>
        /// <returns>Bytes array of given bitmap</returns>
        public static byte[] ImageToByte(this Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr ptr);
    }
}

