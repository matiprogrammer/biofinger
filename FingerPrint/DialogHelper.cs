using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public static MessageBoxResult SaveRequest(string message)
        {
            return MessageBox.Show(message, string.Empty, MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
        }
    }
}
