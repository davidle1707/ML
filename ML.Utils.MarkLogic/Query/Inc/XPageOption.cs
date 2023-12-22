using System;

namespace ML.Utils.MarkLogic
{
    [Serializable]
	public sealed class XPageOption
	{
		
        public int PageSize { get; set; } = int.MaxValue;

		public int PageNumber { get; set; }

		internal bool IsValid => PageSize != int.MaxValue;

        public int StartIndex => ((PageNumber > 1 ? PageNumber : 1) - 1) * PageSize + 1;

        public int EndIndex => StartIndex + PageSize - 1;

        internal void NoPaging()
        {
            PageNumber = 1;
            PageSize = int.MaxValue;
        }
	}
}
