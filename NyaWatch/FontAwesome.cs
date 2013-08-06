using System;
using MonoMac.Foundation;
using MonoMac.AppKit;

namespace NyaWatch
{
	public static class FontAwesome
	{
		public static readonly NSFont Font = null;

		static FontAwesome()
		{
			Font = NSFont.FromFontName (Core.Fonts.Awesome.Name, 14);
		}
	}
}

