using iTextSharp.text;
using System;

namespace ML.Utils.Pdf
{
    [Serializable]
    public class SignaturePosition
    {
        public string FieldKey { get; set; }

        public float LowerLeftX { get; set; }

        public float LowerLeftY { get; set; }

        public float UpperRightX { get; set; }

        public float UpperRightY { get; set; }

        public int PageNumber { get; set; }

        public Rectangle PageSize { get; set; }
    }
}
