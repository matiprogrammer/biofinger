using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Algorithms
{
    class Otsu
    {
        public Otsu()
        {

        }
        public unsafe int ApplyOtsuThreshold(Bitmap bitmap)
        {
            Bitmap source = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format8bppIndexed);
            if (bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                source = bitmap.GrayscaleConversion(RGB.G);

            int[] intHistogram = source.GetHistogram().Values.ToArray();

            int Y, Amount = 0;
            int PixelBack = 0, PixelFore = 0, PixelIntegralBack = 0, PixelIntegralFore = 0, PixelIntegral = 0;
            double OmegaBack, OmegaFore, MicroBack, MicroFore, SigmaB, Sigma;
            int MinValue, MaxValue;
            int Threshold = 0;

            for (MinValue = 0; MinValue < 256 && intHistogram[MinValue] == 0; MinValue++) ;
            for (MaxValue = 255; MaxValue > MinValue && intHistogram[MinValue] == 0; MaxValue--) ;
            if (MaxValue == MinValue)
                return MaxValue;
            if (MinValue + 1 == MaxValue)
                return MinValue;

            for (Y = MinValue; Y <= MaxValue; Y++)
                Amount += intHistogram[Y];

            PixelIntegral = 0;
            for (Y = MinValue; Y <= MaxValue; Y++)
                PixelIntegral += intHistogram[Y] * Y;
            SigmaB = -1;
            for (Y = MinValue; Y < MaxValue; Y++)
            {
                PixelBack = PixelBack + intHistogram[Y];
                PixelFore = Amount - PixelBack;
                OmegaBack = (double)PixelBack / Amount;
                OmegaFore = (double)PixelFore / Amount;
                PixelIntegralBack += intHistogram[Y] * Y;
                PixelIntegralFore = PixelIntegral - PixelIntegralBack;
                MicroBack = (double)PixelIntegralBack / PixelBack;
                MicroFore = (double)PixelIntegralFore / PixelFore;
                Sigma = OmegaBack * OmegaFore * (MicroBack - MicroFore) * (MicroBack - MicroFore);
                if (Sigma > SigmaB)
                {
                    SigmaB = Sigma;
                    Threshold = Y;
                }
            }
            return Threshold;
        }

        public Bitmap ApplyThreshold(int threshold, Bitmap source)
        {
            Bitmap output = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Height; i++)
                for (int j = 0; j < source.Width; j++)
                {
                    if (source.GetPixel(j, i).G > threshold)
                    {
                        output.SetPixel(j, i, Color.White);
                    }
                    else
                    {
                        output.SetPixel(j, i, Color.Black);
                    }
                }
            return output;
        }
    }
}
