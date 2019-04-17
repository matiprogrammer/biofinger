using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace FingerPrint
{
    public static class DialogHelper
    {
        /// <summary>
        /// Shows the open File Dialog to select an Image.
        /// </summary>
        /// <param name="file">Retrieves a File to open.</param>
        /// <returns>True if a File was selected, False otherwise.</returns>
        public static bool ShowOpenFileDialog(out FileInfo file)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select a picture";
            dialog.Filter = "All Images|*.mim;*.bmp;*.png;*.jpg;*.jpeg;*.gif;*.tif|All Files|*.*";
            dialog.Multiselect = false;

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                file = new FileInfo(dialog.FileName);
                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }

        /// <summary>
        /// Shows the save File Dialog.
        /// </summary>
        /// <param name="file">Retrieves a File to save to.</param>
        /// <returns>True if a File was selected, False otherwise.</returns>
        public static bool ShowSaveFileDialog(out FileInfo file)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap|*.bmp|PNG Image|*.png|JPEG Image|*.jpg|GIF Image|*.gif|TIFF Image|*.tif";
            dialog.AddExtension = true;

            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                file = new FileInfo(dialog.FileName);
                return true;
            }
            else
            {
                file = null;
                return false;
            }
        }

        /// <summary>
        /// Shows an Error Message.
        /// </summary>
        /// <param name="message">Message to show.</param>
        public static void ShowCriticalError(string message)
        {
            MessageBox.Show(message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Shows a File Save Request.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <returns>User's Choise.</returns>
        public static MessageBoxResult SaveRequest(string message) =>
            MessageBox.Show(message, string.Empty, MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);

        /// <summary>
        /// Saves image with given format.
        /// </summary>
        /// <param name="file">Represents all of file's properties</param>
        public static void Save(FileInfo file)
        {
            try
            {
                switch (file.Extension.ToLower())
                {
                    case ".bmp" : SaveBitmap(file,  new BmpBitmapEncoder ()); break;
                    case ".png" : SaveBitmap(file,  new PngBitmapEncoder ()); break;
                    case ".jpg" :
                    case ".jpeg": SaveBitmap(file, new JpegBitmapEncoder()); break;
                    case ".gif" : SaveBitmap(file,  new GifBitmapEncoder ()); break;
                    case ".tif" : SaveBitmap(file,  new TiffBitmapEncoder()); break;
                    default:
                        throw new ArgumentException("Doesnt support this format");
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCriticalError(ex.Message);
            }
        }

        private static void SaveBitmap(FileInfo file, BitmapEncoder encoder)
        {
            FileStream output = File.Open(file.FullName, FileMode.OpenOrCreate, FileAccess.Write);
            encoder.Save(output);
            output.Close();
        }
    }
}
