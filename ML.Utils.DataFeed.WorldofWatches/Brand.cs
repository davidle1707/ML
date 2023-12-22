using System;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.DataFeed.WorldofWatches
{
    [Serializable]
    public class Brands : List<Brand>
    {
        public Brand this[string name] => !string.IsNullOrEmpty(name) ? this[n => n.Equals(name, StringComparison.OrdinalIgnoreCase)] : null;

        public Brand this[Func<string, bool> matchName] => this.FirstOrDefault(b => b.Match(matchName));
    }

    [Serializable]
    public class Brand
    {
        public Brand(string name, string key)
        {
            Name = name;
            Key = key;
        }

        public string Name { get; set; }

        public string Key { get; set; }

        public bool Match(Func<string, bool> matchName)
        {
            return matchName(Name) || matchName(Key);
        }
    }
}
