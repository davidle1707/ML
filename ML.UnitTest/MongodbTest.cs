using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using NUnit.Framework;

namespace ML.UnitTest
{
    [TestFixture]
    public class MongodbTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test()
        {
            //var t = Builders<User>.Update.p()
        }

        class User
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public List<Contact> Contacts { get; set; } = new List<Contact>();
        }

        class Contact
        {
            
        }
    }
}
