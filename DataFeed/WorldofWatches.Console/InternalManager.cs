using ML.Utils.DataFeed.WorldofWatches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldofWatches.Console
{
    internal class InternalManager
    {
        #region Singleton

        private static readonly Lazy<InternalManager> LazyInstance = new Lazy<InternalManager>(() => new InternalManager());

        public static InternalManager Instance => LazyInstance.Value;

        #endregion

        private readonly ProductManager _product;
        private readonly WorldofWatchesManager _manager;

        public InternalManager()
        {
            _product = new ProductManager();
            _manager = new WorldofWatchesManager();
        }

        public List<DataFeed> GetDataFeeds(List<ProductType> types, params string[] brandKeys)
        {
            var feeds = WorldofWatchesManager.DataFeeds.Where(a => types.Contains(a.ProductType)).ToList();

            if (brandKeys.Length > 0 && brandKeys.Length != WorldofWatchesManager.Brands.Count)
            {
                var brands = WorldofWatchesManager.Brands.Where(b => brandKeys.Contains(b.Key)).ToList();
                feeds.ForEach(f => f.Brands.AddRange(brands));
            }

            return feeds;
        }

        public async Task  GetProducts(List<DataFeed> feeds, int? startPage = null, int? startRecordIndex = null, int? delayMillisecondsEachPage = null, int? delayMillisecondsEachProduct = null, RichTextBox rtbReport = null)
        {
            var index = 1;

            var option = new GetOption
            {
                ProductGetFullDetails = true,
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"{index++}. Key: {product.Key} - Name: {product.Name}");
                    }

                    await _product.SaveAsync(product);
                }
            };

            option.StartPage = startPage ?? option.StartPage;
            option.StartRecordIndex = startRecordIndex ?? option.StartRecordIndex;
            option.DelayMillisecondsEachPage = delayMillisecondsEachPage ?? option.DelayMillisecondsEachPage;
            option.DelayMillisecondsEachProduct = delayMillisecondsEachProduct ?? option.DelayMillisecondsEachProduct;


            await _manager.GetProductsAsync(feeds, option);
        }

        public Task UpdatePriceProductsInConsole(List<ProductType> types, params string[] brandKeys)
        {
            var feeds = GetDataFeeds(types, brandKeys);

            return UpdatePriceProducts(feeds);
        }

        public Task UpdatePriceProducts(List<DataFeed> feeds, int? startPage = null, int? startRecordIndex = null, int? delayMillisecondsEachPage = null, int? delayMillisecondsEachProduct = null, RichTextBox rtbReport = null)
        {
            var index = 1;

            var option = new GetOption
            {
                ProductGetFullDetails = false,
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"{index++}. Key: {product.Key} - Name: {product.Name}");
                    }
                    
                    var productId = await _product.UpdatePriceAsync(product);

                    //not exists -> call get product from data feed
                    if (productId == null)
                    {
                        await GetProductFromFeed(new DataFeed(product.Type, product.Url, product.Category));
                    }
                    else
                    {
                        rtbReport?.AppendText($" - ProductId: {productId}");
                    }
                }
            };

            option.StartPage = startPage ?? option.StartPage;
            option.StartRecordIndex = startRecordIndex ?? option.StartRecordIndex;
            option.DelayMillisecondsEachPage = delayMillisecondsEachPage ?? option.DelayMillisecondsEachPage;
            option.DelayMillisecondsEachProduct = delayMillisecondsEachProduct ?? option.DelayMillisecondsEachProduct;


            return _manager.GetProductsAsync(feeds, option);
        }

        private Task GetProductFromFeed(DataFeed feed, RichTextBox rtbReport = null)
        {
            return _manager.GetProductAsync(feed, new GetOption
            {
                ProductCallBackAsync = async product =>
                {
                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"\t- Get feed {feed.Url}");
                    }
                    
                    var productId = await _product.SaveAsync(product);

                    if (rtbReport != null)
                    {
                        rtbReport.AppendText(Environment.NewLine);
                        rtbReport.AppendText($"\t- ProductId: {productId}");
                    }
                },
            });
        }
    }
}
