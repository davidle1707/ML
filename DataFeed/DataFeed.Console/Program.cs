using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;
using MLWorldofWatches = ML.Utils.DataFeed.WorldofWatches;
using MLDiscountWatchStore = ML.Utils.DataFeed.DiscountWatchStore;

namespace DataFeed.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //var manager = new MLWorldofWatches.WorldofWatchesManager();
            //var product = manager.GetProductAsync(new MLWorldofWatches.DataFeed(MLWorldofWatches.ProductType.JewelryBracelet), MLWorldofWatches.GetOption.Default).Result;

            //var productFeeds = manager.GetProductFeedsAsync(new WorldofWatches.DataFeed(WorldofWatches.ProductType.Watch)
            //{
            //    Url = "jewelry/rings",
            //}, new WorldofWatches.GetOption { }).Result;

            //var productFeedsJson = JsonConvert.SerializeObject(productFeeds);
            //File.WriteAllText(@"c:\temp\productfeeds.txt", productFeedsJson);

            var manager = new MLDiscountWatchStore.DiscountWatchStoreManager();
            //var product = manager.GetProductAsync(new DiscountWatchStore.DataFeed(DiscountWatchStore.ProductType.UnknownNewArrivals, "http://www.discountwatchstore.com/products/kate-spade-wlru2169-977-womens-a-la-vita-ostrich-neda-sidewalk-zip-around-taupe-leather-wallet.html", null), DiscountWatchStore.GetOption.Default).Result;

            //Test();

            TestProxy().Wait();
        }

        static void Test()
        {
            var htmlContent = File.ReadAllText(@"C:\Users\mongngoc\Desktop\TestWatch\wow_list.txt");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);


            //var nProducts = htmlDoc.DocumentNode.SelectNodes("//div[starts-with(@id, 'entitledItem_')]/div[@data-catentryid-partnumber and @data-catentryidentifier]");

            //foreach (var nProduct in nProducts)
            //{
            //    var key = nProduct.Attributes["data-catentryidentifier"].Value;
            //    var sku = nProduct.Attributes["data-catentryid-partnumber"].Value;
            //    var nLink = nProduct.SelectSingleNode(".//a[starts-with(@id, 'catalogEntry_') and @href]");
            //    var nBrand = nProduct.SelectSingleNode(".//div[contains(@class, 'product_brand')]/a");
            //    var nName = nProduct.SelectSingleNode(".//div[contains(@class, 'product_name')]/a");

            //    var nAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'offerPrice_') and contains(@class, 'sale_price')]/span[contains(@class, 'price')]");
            //    var nListAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'listPrice_') and contains(@class, 'list_price')]/span[contains(@class, 'price')]");
            //    var nRegAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'yourprice_') and contains(@class, 'reg_price')]/span[contains(@class, 'price')]");


            //}
        }

        static async Task TestProxy()
        {
            var handler = new HttpClientHandler
            {
                Proxy = new WebProxy("http://14.161.38.118:80"),
                UseProxy = true,
            };

            using (var client = new HttpClient(handler))
            {
                var data = new Dictionary<string, string>
                {
                    ["cc"] = "1",
                    ["phonenum"] = "7147252456"
                };

                var httpResponse = await client.PostAsync("http://freecarrierlookup.com/getcarrier.php", new FormUrlEncodedContent(data));
                var httpContent = await httpResponse.Content.ReadAsStringAsync();
            }
            
            //// ... Use HttpClient.            
            //HttpClient client = new HttpClient(handler);

            //var byteArray = Encoding.ASCII.GetBytes("username:password1234");
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            //HttpResponseMessage response = await client.GetAsync(TARGETURL);
            //HttpContent content = response.Content;

        }

        static string GetHtml(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            return HttpUtility.HtmlDecode(GetText(source));
        }

        static string GetText(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            return source.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty).Trim();
        }
    }
}
