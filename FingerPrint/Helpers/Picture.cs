using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FingerPrint
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Picture
    {
        public Picture(Bitmap bitmap) => this.Bitmap = new Bitmap(bitmap);
        public Picture(int width, int height) : this(new Bitmap(width, height)) { }
        public Bitmap Bitmap { get; private set; }
        public int Width => this.Bitmap.Width;
        public int Height => this.Bitmap.Height;
        public BitmapSource BitmapSource => this.Bitmap.GetBitmapSource();
        public byte[] ByteArray => this.Bitmap.ImageToByte();
        public Bitmap Reset() => this.Bitmap = new Bitmap(this.Bitmap);
        public void UnlockBits(BitmapData bitmapData) 
            => this.Bitmap.UnlockBits(bitmapData);
        public BitmapData LockBits(ImageLockMode lockMode, Rectangle? rect = null) 
            => this.Bitmap.LockBits(rect ?? new Rectangle(Point.Empty, this.Bitmap.Size), lockMode, this.Bitmap.PixelFormat);
    }
}
