using System;

namespace NyaWatch.Core.Data
{
	public class CategoryNotFoundException : Exception
	{
		public string Category { get; private set; }

		public CategoryNotFoundException (string category)
		{
			Category = category;
		}

		public override string Message {
			get {
				return string.Format("Category [{0}] not found in database", Category);
			}
		}
	}
}

