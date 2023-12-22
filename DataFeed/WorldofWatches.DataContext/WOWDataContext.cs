using ML.Utils.MongoDb;
using System.Configuration;
using MongoDB.Driver;

namespace WorldofWatches.DataContext
{
    public class WOWDataContext : MongoApiContext
    {
        public WOWDataContext()
            : base(ConfigurationManager.AppSettings["WOWConnectionString"], ConfigurationManager.AppSettings["WOWDbName"])
        {
        }

        public IMongoCollection<Product> Products => Get<Product>();
    }
}
