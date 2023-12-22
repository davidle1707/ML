using System;

namespace ML.Utils.Phone
{
    [Serializable]
    public class DeletePhoneResponse
    {
        public bool Success { get; set; }

        public string Description { get; set; }
    }
}
