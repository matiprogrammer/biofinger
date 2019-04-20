using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Algorithms
{
    internal unsafe class K3M : Alghorithm
    {
        public K3M(Bitmap bitmap) : base(bitmap) { }

        //        public override unsafe void Apply(byte** p, int length, int width, int height)
        public override unsafe Picture Apply(Picture picture)
        {
            Picture workingBitmap = new Picture(picture.Bitmap);
            BitmapData data = workingBitmap.LockBits(ImageLockMode.ReadWrite);

            byte* ptr = (byte*)data.Scan0.ToPointer();
            int length = data.Stride * workingBitmap.Height;
            int stride = data.Stride;
            int bpp = stride / picture.Width;

            int[] offsets =
            {
                -bpp - stride, -stride, bpp -stride,
                -bpp, 0, bpp,
                -bpp + stride,  stride, bpp + stride,
            };

            int offset = stride + bpp;

            var blacks = new HashSet<int>();

            for (int i = offset; i <  length - offset; i += bpp)
                if (ptr[i] == Black)
                    blacks.Add(i);

            var borders = new HashSet<int>();

            bool anyChange = true;
            while (anyChange)
            {
                anyChange = false;

                foreach (int i in blacks)
                    if (ptr[i] == Black)
                        if (A[0].Contains(Compute(ptr + i, offsets)))
                            borders.Add(i);

                for (int i = 1; i < A.Length; i++)
                    foreach (int j in borders)
                        if (ptr[j] == Black)
                            if (A[i].Contains(Compute(ptr + j, offsets)))
                                for (int k = 0; k < bpp; k++)
                                {
                                    ptr[j + k] = White;
                                    anyChange = true;
                                }
            }

            foreach (int i in blacks)
                if (A0pix.Contains(Compute(ptr + i, offsets)))
                    for (int k = 0; k < bpp; k++)
                        ptr[i + k] = White;
            workingBitmap.UnlockBits(data);
            return workingBitmap;
        }

        private unsafe int Compute(byte* ptr, int[] offsets)
        {
            int sum = 0;
            for (int i = 0; i < offsets.Length; i++)
                if (ptr[offsets[i]] == Black)
                    sum += Matrix[i];
            return sum;
        }

        private readonly static int[] Matrix =
        {
            128,  1, 2,
             64,  0, 4,
             32, 16, 8
        };

        private const byte White = 255;
        private const byte Black = 0;

        private readonly static HashSet<int>[] A = new HashSet<int>[]
        {
            new HashSet<int>(new[] { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254 }),
            new HashSet<int>(new[] { 7, 14, 28, 56, 112, 131, 193, 224 }),
            new HashSet<int>(new[] { 7, 14, 15, 28, 30, 56, 60, 112, 120, 131, 135, 193, 195, 224, 225, 240 }),
            new HashSet<int>(new[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 112, 120, 124, 131, 135, 143, 193, 195, 199, 224, 225, 227, 240, 241, 248 }),
            new HashSet<int>(new[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 193, 195, 199, 207, 224, 225, 227, 231, 240, 241, 243, 248, 249, 252 }),
            new HashSet<int>(new[] { 7, 14, 15, 28, 30, 31, 56, 60, 62, 63, 112, 120, 124, 126, 131, 135, 143, 159, 191, 193, 195, 199, 207, 224, 225, 227, 231, 239, 240, 241, 243, 248, 249, 251, 252, 254 }),
        };

        private readonly static HashSet<int> A0pix = new HashSet<int>(new[] { 3, 6, 7, 12, 14, 15, 24, 28, 30, 31, 48, 56, 60, 62, 63, 96, 112, 120, 124, 126, 127, 129, 131, 135, 143, 159, 191, 192, 193, 195, 199, 207, 223, 224, 225, 227, 231, 239, 240, 241, 243, 247, 248, 249, 251, 252, 253, 254 });
    }
}
