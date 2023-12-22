using System;

namespace ML.Utils.Pdf
{
    [Serializable]
    public class MapResponse
    {
        public bool Success { get; set; }

        public object Value { get; set; }
    }
}
