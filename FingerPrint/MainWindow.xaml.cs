using FingerPrint.Algorithms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image = System.Drawing.Image;

namespace FingerPrint
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void K3MBtn_Click(object sender, RoutedEventArgs e)
        {
            var algorithm = new K3M(this.picture.Bitmap);
            picture = algorithm.Apply(this.picture);
            OutputImage.Source = picture.BitmapSource;
        }

        private void MinutiaeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            if (!DialogHelper.ShowOpenFileDialog(out var file))
                return;
            this.InputImage.Source = new BitmapImage(new Uri(file.FullName));
            var fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            using (var ab = Image.FromStream(fs))
            {
                this.picture = new Picture((Bitmap)ab);
                this.reset = new Picture((Bitmap)ab);
                this.outputPicture = new Picture((Bitmap)ab);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!DialogHelper.ShowSaveFileDialog(out var file))
                return;
            DialogHelper.Save(file);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            this.InputImage.Source = this.reset.Reset().GetBitmapSource();
            this.picture = new Picture(this.reset.Bitmap);
        }
        private Picture picture;
        private Picture reset;
        private Picture outputPicture;
        private void Binarization_Click(object sender, RoutedEventArgs e)
        {
            Otsu binarization = new Otsu();
            outputPicture = new Picture(binarization.ApplyThreshold(70, outputPicture.Bitmap));
            OutputImage.Source = outputPicture.BitmapSource;
        }

        private void Swap_Click(object sender, RoutedEventArgs e)
        {
            OutputImage.Source = picture.BitmapSource;
            InputImage.Source = outputPicture.BitmapSource;
            var a = new Picture(picture.Bitmap);
            var b = new Picture(outputPicture.Bitmap);
            picture = new Picture(b.Bitmap);
            outputPicture = new Picture(a.Bitmap);
        }
    }
}
