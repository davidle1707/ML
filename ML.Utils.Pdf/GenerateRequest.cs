using System;
using System.Collections.Generic;

namespace ML.Utils.Pdf
{
    [Serializable]
    public class GenerateRequest
    {
        public GenerateRequest()
        {
            SignatureFieldKeys = new List<string>();
        }

        public bool ShowPageNumber { get; set; }

        public bool ShowPrintDialog { get; set; }

        public bool LockMappingField { get; set; }

        public Func<MapRequest, MapResponse> Mapper { get; set; }

        public bool RetrieveSignaturePositions { get; set; }

        public bool RetrieveTotalPages { get; set; }

        public List<string> SignatureFieldKeys { get; set; }
    }
}
