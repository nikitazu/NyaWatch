using System;
using StructureMap;

namespace NyaWatch.Core.UI
{
	/// <summary>
	/// Dialogs.
	/// </summary>
	public static class Dialogs
	{
		/// <summary>
		/// The message.
		/// </summary>
		public static readonly IMessageBox Message;

		/// <summary>
		/// Initializes the <see cref="NyaWatch.Core.UI.Dialogs"/> class.
		/// </summary>
		static Dialogs()
		{
			Message = ObjectFactory.GetInstance<IMessageBox> ();
		}
	}
}

