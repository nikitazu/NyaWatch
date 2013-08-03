using System;

namespace NyaWatch.Core.Data
{
	public class CorruptStorageException : Exception
	{
		public string PathToFile { get; set; }

		public CorruptStorageException (string pathToFile)
		{
			PathToFile = pathToFile;
		}

		public override string Message {
			get {
				return "Corrupted storage in: " + PathToFile;
			}
		}
	}
}

