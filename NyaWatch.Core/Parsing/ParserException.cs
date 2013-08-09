using System;

namespace NyaWatch.Core.Parsing
{
	public class ParserException : Exception
	{
		public string XPath { get; private set; }

		public ParserException (string xpath)
		{
			if (string.IsNullOrWhiteSpace (xpath)) {
				throw new ArgumentException ("Should not be empty", "xpath");
			}
			XPath = xpath;
		}

		public override string Message {
			get {
				return string.Format ("No data at xpath: {0}", XPath);
			}
		}
	}
}

