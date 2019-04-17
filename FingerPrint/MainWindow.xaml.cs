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

        private void ClaheBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ErosionBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DilationBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OpeningBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClosingBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void K3MBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MinutiaeBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = null;
            if (!DialogHelper.ShowOpenFileDialog(out file) == true)
            {
                return;
            }
            InputImage.Source = new BitmapImage(new Uri(file.FullName));
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            using (System.Drawing.Image ab = System.Drawing.Image.FromStream(fs))
            {
                bitmap = new Bitmap(ab);
                reset = new Bitmap(ab);
            }
        }
   
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FileInfo file = null;
            if (!DialogHelper.ShowSaveFileDialog(out file))
                return;
            DialogHelper.Save(file);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InputImage.Source = reset.GetBitmapSource();
            bitmap = new Bitmap(reset);
        }

        Bitmap bitmap;
        Bitmap reset;
    }
}
