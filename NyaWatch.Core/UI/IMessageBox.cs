using System;

namespace NyaWatch.Core.UI
{
	/// <summary>
	/// Message box interface.
	/// </summary>
	public interface IMessageBox
	{
		/// <summary>
		/// Show message box with title and message.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		void Show (string title, string message);

		/// <summary>
		/// Show error with the specified title and message.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="message">Message.</param>
		void Error (string title, string message);

		/// <summary>
		/// Show error with the specified title.
		/// </summary>
		/// <param name="title">Title.</param>
		/// <param name="error">Error.</param>
		void Error (string title, Exception error);
	}
}

