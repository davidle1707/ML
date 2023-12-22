using System;

namespace ML.Utils.MarkLogic.XQuery
{
    [Serializable]
	public sealed class XPaging
	{
		public XPaging()
		{
			PageSize = int.MaxValue;
		}

		public int PageSize { get; set; }

		public int PageNumber { get; set; }

		public bool IsValid => PageSize != int.MaxValue;

        public int StartIndex => ((PageNumber > 1 ? PageNumber : 1) - 1) * PageSize + 1;

        public int EndIndex => StartIndex + PageSize - 1;

        public void NoPaging()
        {
            PageNumber = 1;
            PageSize = int.MaxValue;
        }
	}
}
