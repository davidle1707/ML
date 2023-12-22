using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    [Serializable]
    public class Brands : List<Brand>
    {
        public Brand this[string name] => this[n => n.Equals(name, StringComparison.OrdinalIgnoreCase)];

        public Brand this[Func<string, bool> matchName] => this.FirstOrDefault(b => b.Match(matchName));
    }

    [Serializable]
    public class Brand
    {
        public Brand(string name, params string[] extends)
        {
            Name = name;
            Extends = new List<string>(extends);
        }

        public string Name { get; set; }

        public List<string> Extends { get; set; }

        public bool Match(Func<string, bool> matchName)
        {
            return matchName(Name) || Extends.Any(matchName);
        }
    }
}
