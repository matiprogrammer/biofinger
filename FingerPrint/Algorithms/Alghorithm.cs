using FingerPrint.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Algorithms
{
    public abstract class Alghorithm : IAlgorithm
    {
        Picture picture;
        public Alghorithm(Bitmap bitmap) 
            => this.picture = new Picture(bitmap);

        public virtual Picture Apply(Picture picture) 
            => throw new NotImplementedException("You must implement this fuction on your own");
    }
}
