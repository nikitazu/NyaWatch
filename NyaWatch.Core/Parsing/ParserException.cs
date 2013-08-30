using System;

namespace NyaWatch.Core.Parsing
{
	public class ParserException : Exception
	{
		public string XPath { get; private set; }
		public string DataChunk { get; private set; }
		public string Phase { get; private set; }

		public ParserException (string xpath, string dataChunk, string phase)
		{
			if (string.IsNullOrWhiteSpace (xpath)) {
				throw new ArgumentException ("Should not be empty", "xpath");
			}
			if (string.IsNullOrWhiteSpace (phase)) {
				throw new ArgumentException ("Should not be empty", "phase");
			}
			XPath = xpath;
			DataChunk = dataChunk.Length > 1500 ? dataChunk.Substring(0, 1500) : dataChunk;
			Phase = phase;
		}

		public override string Message {
			get {
				return string.Format (
					"Parser failed on phase [{0}] at xpath [{1}] for data chunk [{2}] ", 
					Phase, 
					XPath,
					DataChunk ?? "NULL");
			}
		}
	}
}

