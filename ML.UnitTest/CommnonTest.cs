using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML.Common;

namespace ML.UnitTest
{
    [TestFixture]
    public class CommnonTest
    {
        [Test]
        public void Test()
        {
            var full = new FullClass
            {
                f_list = new List<ItemClass>
                {
                    new ItemClass()
                }
            };

            Type t = null;

            var test = full.GetValue("f_int", ref t);
            test = full.GetValue("f_list", ref t);
            test = full.GetValue("f_list[0]", ref t);
            test = full.GetValue("f_list[0].f_string", ref t);


            test = full.GetValue("f_dictionary", ref t);
            test = full.GetValue("f_dictionary[0]", ref t);
            test = full.GetValue("f_dictionary[0].f_string", ref t);
        }
    }

    public class FullClass
    {
        public int f_int { get; set; }

        public List<ItemClass> f_list { get; set; }

        public Dictionary<string, ItemClass> f_dictionary { get; set;  }
    }

    public class ItemClass
    {
        public string f_string { get; set; }
    }
}
