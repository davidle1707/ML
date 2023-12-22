using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ML.Utils.DataFeed.DiscountWatchStore
{
    public partial class DiscountWatchStoreManager
    {
        public async Task<List<Product>> GetProductsAsync(List<DataFeed> feeds, GetOption option)
        {
            var products = new List<Product>();

            using (var client = new WebClient())
            {
                foreach (var feed in feeds)
                {
                    var endPage = false; var page = option.StartPage;

                    while (!endPage)
                    {
                        var productFeedsByPage = await GetProductFeedsByPageAsync(client, feed, page);

                        //get products
                        foreach (var productFeed in productFeedsByPage.ProductFeeds.Skip(option.StartRecordIndex))
                        {
                            var product = productFeed.Product;

                            if (option.ProductGetFullDetails)
                            {
                                product = await GetProductFullDetailsAsync(client, productFeed, option);

                                if (product == null)
                                {
                                    continue;
                                }
                            }

                            if (option.ProductCallBackAsync != null)
                            {
                                await option.ProductCallBackAsync(product);
                            }

                            products.Add(product);

                            if (option.DelayMillisecondsEachProduct != null && option.DelayMillisecondsEachProduct > 0)
                            {
                                await Task.Delay(option.DelayMillisecondsEachProduct.Value);
                            }
                        }

                        if (option.DelayMillisecondsEachPage != null && option.DelayMillisecondsEachPage > 0 && !productFeedsByPage.IsEndPage)
                        {
                            await Task.Delay(option.DelayMillisecondsEachPage.Value);
                        }

                        endPage = productFeedsByPage.IsEndPage; page++;
                    }
                }
            }

            return products;
        }

        public async Task<Product> GetProductAsync(DataFeed productFeed, GetOption option)
        {
            Product product;

            using (var client = new WebClient())
            {
                product = await GetProductFullDetailsAsync(client, productFeed, option);
            }

            return product;
        }

        private async Task<List<DataFeed>> GetProductFeedsAsync(WebClient client, DataFeed feed, GetOption option)
        {
            var productFeeds = new List<DataFeed>();

            var page = option.StartPage;
            var breakWhile = false;

            while (!breakWhile)
            {
                var htmlContent = await client.DownloadStringTaskAsync($"{RootUrl}/{feed.Url}?page={page++}");

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                var aProducts = htmlDoc.DocumentNode.SelectNodes("//li[@data-product]/a[@href]");

                if (aProducts?.Count > 0)
                {
                    productFeeds.AddRange(aProducts.Select(a => new DataFeed(feed.ProductType, a.Attributes["href"].Value, feed.Brand, feed.Category)));
                    breakWhile = htmlDoc.DocumentNode.SelectSingleNode("//ul[contains(@class, 'pagination')]") == null;
                }
                else
                {
                    breakWhile = true;
                }
            }

            return productFeeds;
        }

        private async Task<ProductFeedsByPage> GetProductFeedsByPageAsync(WebClient client, DataFeed feed, int page)
        {
            var productFeedsByPage = new ProductFeedsByPage { IsEndPage = true };

            var htmlContent = await client.DownloadStringTaskAsync($"{RootUrl}/{feed.Url}?page={page}");
            //var htmlContent = File.ReadAllText(@"C:\Users\mongngoc\Desktop\TestWatch\dws_list.txt");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            var nProducts = htmlDoc.DocumentNode.SelectNodes("//li[@data-product]");

            if (nProducts?.Count > 0)
            {
                foreach (var nProduct in nProducts)
                {
                    var nName = nProduct.SelectSingleNode(".//*[contains(@class, 'ProductName')]/a[@href]");

                    if (nName == null)
                    {
                        continue;
                    }

                    var productFeed = feed.Clone(nName.Attributes["href"].Value);
                    productFeedsByPage.ProductFeeds.Add(productFeed);

                    //info
                    productFeed.Product.Key = nProduct.Attributes["data-product"].Value;
                    productFeed.Product.Name = HtmlDecode(nName.InnerText);

                    var nPriceAmount = nProduct.SelectSingleNode(".//*[contains(@class, 'ProductPrice')]/text()");
                    productFeed.Product.Price.Amount = GetAmount(nPriceAmount?.InnerText) ?? 0;

                    var nPriceRetailAmount = nProduct.SelectSingleNode(".//*[contains(@class, 'RetailPriceValue')]");
                    productFeed.Product.Price.RetailAmount = GetAmount(nPriceRetailAmount?.InnerText);
                }

                productFeedsByPage.IsEndPage = htmlDoc.DocumentNode.SelectSingleNode("//ul[contains(@class, 'pagination')]") == null;
            }

            return productFeedsByPage;
        }

        private async Task<Product> GetProductFullDetailsAsync(WebClient client, DataFeed productFeed, GetOption option)
        {
            Product product = null;

            try
            {
                var htmlContent = await client.DownloadStringTaskAsync(productFeed.Url);

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                product = new Product(productFeed);

                var docNode = htmlDoc.DocumentNode;

                //id
                var nId = docNode.SelectSingleNode("//input[contains(@name, 'product_id') and @value]");
                product.Key = nId?.Attributes["value"].Value;

                if (string.IsNullOrWhiteSpace(product.Key))
                {
                    return null;
                }

                if (product.Type == ProductType.UnknownMisc || product.Type == ProductType.UnknownNewArrivals)
                {
                    PopulateProductType(product, docNode);
                }

                //sku
                var nSku = docNode.SelectSingleNode("//input[contains(@name, 'pro-sku') and @value]");
                product.Sku = nSku?.Attributes["value"].Value;

                //name
                var nName = docNode.SelectSingleNode("//input[contains(@name, 'pro-name') and @value]");
                product.Name = HttpUtility.HtmlDecode(nName?.Attributes["value"].Value);

                if (product.Brand == null)
                {
                    //Sku.StartWith or Name.Contains
                    product.Brand = Brands[n => product.Sku.StartsWith(n, StringComparison.OrdinalIgnoreCase)] ?? Brands[n => product.Name.Contains(n)];
                }

                //price.amount
                var nPriceAmount = docNode.SelectSingleNode("//meta[contains(@property, 'product:price:amount') and @content]");
                product.Price.Amount = GetAmount(nPriceAmount?.Attributes["content"].Value) ?? 0;

                //price.currency
                var nPriceCurrency = docNode.SelectSingleNode("//meta[contains(@property, 'product:price:currency') and @content]");
                product.Price.Currency = nPriceCurrency?.Attributes["content"].Value;

                //price.retailamount
                var nPriceRetailAmount = docNode.SelectSingleNode("//div[contains(@class, 'retailprice') and contains(@class, 'Value')]");
                product.Price.RetailAmount = GetAmount(nPriceRetailAmount?.InnerText);

                //description
                var divDescription = docNode.SelectSingleNode("//div[contains(@itemprop, 'description')]");
                product.Description = GetText(divDescription?.InnerHtml);

                //specification
                var nSpecifications = docNode.SelectNodes("//div[contains(@class, 'other-detail-pro-sub')]");
                //PopulateProductSpecification(product, nSpecifications);
                PopulateProductAttributes(product, nSpecifications);

                //product-thumbs
                var nImages = docNode.SelectNodes("//ul[contains(@class, 'product-thumbs')]//a[contains(@rel, 'fancybox-thumb') and @href]");
                await PopulateProductImagesAsync(client, product, nImages);
            }
            catch
            {
                // ignored
            }

            if (product != null && option.ProductCallBackAsync != null)
            {
                await option.ProductCallBackAsync(product);
            }

            return product;
        }

        private void PopulateProductType(Product product, HtmlNode docNode)
        {
            var liBreadcrumbs = docNode.SelectNodes("//ul[contains(@class, 'breadcrumbs')]/li");

            if (liBreadcrumbs == null || liBreadcrumbs.Count < 1)
            {
                return;
            }

            var typeAsString = HttpUtility.HtmlDecode(liBreadcrumbs[1].InnerText);

            if (typeAsString.StartsWith("Accessories", StringComparison.OrdinalIgnoreCase) && liBreadcrumbs.Count > 2)
            {
                typeAsString = HttpUtility.HtmlDecode(liBreadcrumbs[2].InnerText);
            }

            if (typeAsString.StartsWith("Watches", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.Watch;
            }
            else if (typeAsString.StartsWith("Handbags & Wallets", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.HandbagWallet;
            }
            else if (typeAsString.StartsWith("Sunglasses", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.Sunglass;
            }
            else if (typeAsString.StartsWith("Perfumes", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.Perfume;
            }
            else if (typeAsString.StartsWith("Clocks", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.Clock;
            }
            else if (typeAsString.StartsWith("Running Accessories", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.RunningAccessory;
            }
            else if (typeAsString.StartsWith("Swiss Army Knives", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.SwissAmryKnives;
            }
            else if (typeAsString.StartsWith("Watch Accessories", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.WatchAccessory;
            }
            else if (typeAsString.StartsWith("Watch Bands", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.WatchBand;
            }
            else if (typeAsString.StartsWith("Writing Instruments", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.WritingInstrument;
            }
            else if (typeAsString.StartsWith("Zippo Lighters", StringComparison.OrdinalIgnoreCase))
            {
                product.Type = ProductType.ZippoLighter;
            }
        }

        private async Task PopulateProductImagesAsync(WebClient client, Product product, HtmlNodeCollection nImages)
        {
            if (nImages == null || nImages.Count == 0)
            {
                return;
            }

            foreach (var nImage in nImages)
            {
                var productImage = await DownloadImageAsync(client, nImage);

                if (productImage != null)
                {
                    product.Images.Add(productImage);
                }
            }
        }

        private void PopulateProductAttributes(Product product, HtmlNodeCollection nSpecifications)
        {
            if (nSpecifications == null || nSpecifications.Count == 0)
            {
                return;
            }

            foreach (var nSpecification in nSpecifications)
            {
                var specifications = GetText(nSpecification.InnerText).Split(':');

                if (specifications.Length != 2)
                {
                    continue;
                }

                product.Attributes.Add(new ProductAttribute { Name = specifications[0].Trim(), Value = specifications[1].Trim() });
            }
        }

        private async Task<ProductImage> DownloadImageAsync(WebClient client, HtmlNode node, string attributeName = "href")
        {
            HtmlAttribute attr;

            if (node == null || (attr = node.Attributes[attributeName]) == null)
            {
                return null;
            }

            try
            {
                var image = new ProductImage { Url = attr.Value, FileName = Path.GetFileName(attr.Value) };

                //custom file: K29-8004__61679.1466788234.1280.1280.jpg?c=2 -> K29-8004__61679.1466788234.1280.1280.jpg
                var indexQuestionChar = image.FileName.IndexOf("?", StringComparison.OrdinalIgnoreCase);

                if (indexQuestionChar >= 0)
                {
                    image.FileName = image.FileName.Substring(0, indexQuestionChar);
                }

                //down
                image.Contents = await client.DownloadDataTaskAsync(attr.Value);

                return image;
            }
            catch
            {
                //ignore
            }

            return null;
        }

        private string HtmlDecode(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : HttpUtility.HtmlDecode(GetText(source));
        }

        private string GetText(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : source.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty).Trim();
        }

        private double? GetAmount(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            double value;

            return double.TryParse(Regex.Replace(source, @"[^0-9\.]", string.Empty), out value) ? value : (double?)null;
        }

        [Serializable]
        private class ProductFeedsByPage
        {
            public bool IsEndPage { get; set; }

            public List<DataFeed> ProductFeeds { get; set; } = new List<DataFeed>();
        }
    }
}
