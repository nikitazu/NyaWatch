using System;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	public static class FontAwesome
	{
	 	public const string FastForwardIcon = "\uf050";
		public const string PlayIcon = "\uf04b";
		public const string StopIcon = "\uf04d";
		public const string PauseIcon = "\uf04c";
		public const string EjectIcon = "\uf052";

		public static readonly NSFont Font = null;

		static FontAwesome()
		{
			Font = NSFont.FromFontName ("fontawesome", 14);
		}
	}
}

