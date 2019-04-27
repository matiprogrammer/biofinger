using FingerPrint.Helpers;
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
        public override unsafe Picture Apply(Picture picture)
        {
            Picture workingBitmap = new Picture(picture.Bitmap);
            BitmapData data = workingBitmap.LockBits(ImageLockMode.ReadWrite);
            byte* ptr = (byte*)data.Scan0.ToPointer();
            int stride = data.Stride;
            int bpp = stride / picture.Width;

            var blacks = new List<int>();

            for (int i = stride + bpp; i < stride * workingBitmap.Height - stride + bpp; i += bpp)
                if (ptr[i] == 0)
                    blacks.Add(i);

            var borders = new List<int>();

            bool isChanged = true;
            while (isChanged)
            {
                isChanged = false;

                foreach (int i in blacks)
                    if (ptr[i] == 0)
                        if (Constants.Deletions[0].Contains(Methods.Compute(ptr + i, stride, bpp, Constants.Matrix)))
                            borders.Add(i);

                for (int i = 1; i < Constants.Deletions.Length; i++)
                    foreach (int j in borders)
                        if (ptr[j] == 0)
                            if (Constants.Deletions[i].Contains(Methods.Compute(ptr + j, stride, bpp, Constants.Matrix)))
                                for (int k = 0; k < bpp; k++)
                                {
                                    ptr[j + k] = 255;
                                    isChanged = true;
                                }
            }

            foreach (int i in blacks)
                if (Constants.Deletions[0].Contains(Methods.Compute(ptr + i, stride, bpp, Constants.Matrix)))
                    for (int k = 0; k < bpp; k++)
                        ptr[i + k] = 255;
            workingBitmap.UnlockBits(data);
            return workingBitmap;
        }
    }
}
