using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Helpers
{
    public class Methods
    { 
        public static unsafe int Compute(byte* ptr, int stride, int bpp, int[] matrix)
        {
            int[] offsets =
            {
                -bpp - stride, -stride, bpp -stride,
                -bpp, 0, bpp,
                -bpp + stride,  stride, bpp + stride,
            };

            int sum = 0;
            for (int i = 0; i < offsets.Length; i++)
                if (ptr[offsets[i]] == 0)
                    sum += matrix[i];
            return sum;
        }
    }
}
