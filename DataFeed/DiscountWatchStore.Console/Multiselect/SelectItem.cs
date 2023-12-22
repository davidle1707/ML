using System;

namespace DiscountWatchStore.Console
{
    [Serializable]
    public class SelectItem
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public bool Selected { get; set; }
    }
}
