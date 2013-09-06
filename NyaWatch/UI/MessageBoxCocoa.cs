using System;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch.UI
{
	/// <summary>
	/// Message box implementation for cocoa.
	/// </summary>
	public class MessageBoxCocoa : Core.UI.IMessageBox
	{
		/// <summary>
		/// Show message box with title and message.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		public void Show (string title, string message)
		{
			using (var alert = new NSAlert()) {
				alert.MessageText = title;
				alert.InformativeText = message;
				alert.AlertStyle = NSAlertStyle.Informational;
				alert.RunModal ();
			}
		}

		/// <summary>
		/// Show error with the specified title and message.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		public void Error (string title, string message)
		{
			using (var alert = new NSAlert()) {
				alert.MessageText = title;
				alert.InformativeText = message;
				alert.AlertStyle = NSAlertStyle.Critical;
				alert.RunModal ();
			}
		}

		/// <summary>
		/// Show error with the specified title.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="error">Error.</param>
		public void Error (string title, Exception error)
		{
			Error (title, error.Message);
		}
	}
}

