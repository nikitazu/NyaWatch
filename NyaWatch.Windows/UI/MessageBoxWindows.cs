using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NyaWatch.Windows.UI
{
    /// <summary>
    /// MessageBox implementation for windows.
    /// </summary>
    public class MessageBoxWindows : Core.UI.IMessageBox
    {
        /// <summary>
        /// Show message box with title and message.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public void Show(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Show error with the specified title and message.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="message">Message.</param>
        public void Error(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// Show error with the specified title.
        /// </summary>
        /// <param name="title">Title.</param>
        /// <param name="error">Error.</param>
        public void Error(string title, Exception error)
        {
            Error(title, error.Message);
        }
    }
}
