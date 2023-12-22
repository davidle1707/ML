using ML.Utils.MongoDb;
using System.Configuration;
using MongoDB.Driver;

namespace DiscountWatchStore.DataContext
{
    public class DWSDataContext : MongoApiContext
    {
        public DWSDataContext()
            : base(ConfigurationManager.AppSettings["DWSConnectionString"], ConfigurationManager.AppSettings["DWSDbName"])
        {
        }

        public IMongoCollection<Product> Products => Get<Product>();
    }
}
