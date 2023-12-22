using System;
using System.Collections.Generic;

namespace ML.Utils.Pdf
{
    [Serializable]
    public class GenerateResponse
    {
        public GenerateResponse()
        {
            SignaturePositions = new List<SignaturePosition>(); ;
        }

        public bool Success { get; set; }

        public string Message { get; set; }

        public int TotalPages { get; set; }

        public List<SignaturePosition> SignaturePositions { get; set; }

        public byte[] Contents { get; set; }
    }
}
