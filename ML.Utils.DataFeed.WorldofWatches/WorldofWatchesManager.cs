using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ML.Utils.DataFeed.WorldofWatches
{
    public partial class WorldofWatchesManager
    {
        public async Task<List<Product>> GetProductsAsync(List<DataFeed> feeds, GetOption option)
        {
            var products = new List<Product>();

            using (var client = new WebClient())
            {
                foreach (var feed in feeds)
                {
                    //get search params to post and get product feeds by ajax
                    var dataSearch = await GetSearchProductDataAsync(client, feed);

                    if (dataSearch == null)
                    {
                        continue;
                    }

                    //set fitler by brands
                    dataSearch.SendBrands(feed.Brands);

                    var page = option.StartPage;

                    while (page <= dataSearch.TotalPages)
                    {
                        //in case: current page = 1 and not filter by brands -> use product feeds (of first page) which get from GetSearchProductDataAsync function
                        var productFeeds = page == 1 && feed.Brands.Count == 0 
                            ? dataSearch.ProductFeedsOfFirstPage
                            : await GetProductFeedsByPageAsync(client, feed, dataSearch, page);

                        //break while if product feeds in current page are empty
                        if (productFeeds.Count == 0)
                        {
                            break;
                        }

                        //get products
                        foreach (var productFeed in productFeeds.Skip(option.StartRecordIndex))
                        {
                            var product = productFeed.Product;

                            if (option.ProductGetFullDetails)
                            {
                                product = await GetProductFullDetailsAsync(client, productFeed);

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

                        if (option.DelayMillisecondsEachPage != null && option.DelayMillisecondsEachPage > 0 && page < dataSearch.TotalPages)
                        {
                            await Task.Delay(option.DelayMillisecondsEachPage.Value);
                        }

                        page++;
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
                product = await GetProductFullDetailsAsync(client, productFeed);
            }

            if (product != null && option.ProductCallBackAsync != null)
            {
                await option.ProductCallBackAsync(product);
            }

            return product;
        }

        private async Task<List<DataFeed>> GetProductFeedsByPageAsync(WebClient client, DataFeed feed, SearchProductData dataSearch, int page)
        {
            var productFeeds = new List<DataFeed>();

            dataSearch.SetPage(page);

            client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            client.Headers["X-Requested-With"] = "XMLHttpRequest";

            var htmlBytes = await client.UploadValuesTaskAsync($"{SearchProductData.SearchUrl}?{dataSearch.QueryString}", dataSearch.FormData);
            var htmlContent = Encoding.UTF8.GetString(htmlBytes);
            //var htmlContent = File.ReadAllText(@"C:\Users\Administrator\Desktop\products.txt");

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            //product feeds
            productFeeds.AddRange(GetProductFeedsInListPage(feed, htmlDoc.DocumentNode));

            //reset total pages when using filter by brands
            if (feed.Brands.Count > 0)
            {
                var nTotalPages = htmlDoc.DocumentNode.SelectSingleNode("(//div[contains(@class, 'pageControl number')]/a)[last()]");
                dataSearch.TotalPages = int.Parse(GetText(nTotalPages?.InnerText ?? "1"));
            }

            return productFeeds;
        }

        private async Task<SearchProductData> GetSearchProductDataAsync(WebClient client, DataFeed feed)
        {
            var htmlContent = await client.DownloadStringTaskAsync($"{RootUrl}/{feed.Url}");
            //var htmlContent = File.ReadAllText(@"C:\Users\mongngoc\Desktop\TestWatch\wow_list.txt");

            var matchSearchUrl = Regex.Match(htmlContent, SearchProductData.SearchUrl + @"(.+?)'\)");

            if (!matchSearchUrl.Success)
            {
                return null;
            }

            var data = new SearchProductData();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            //query string
            var uri = new Uri(matchSearchUrl.Value.TrimEnd(')', '\''), UriKind.RelativeOrAbsolute);
            data.QueryString = HttpUtility.ParseQueryString(uri.Query);
            data.QueryString["searchtype"] = "1000";
            data.QueryString["metaData"] = "";

            //form
            var nStoreId = htmlDoc.DocumentNode.SelectSingleNode("//input[contains(@name, 'storeId') and @value]");
            data.FormData["storeId"] = nStoreId?.Attributes["value"].Value ?? string.Empty;

            var nCatalogId = htmlDoc.DocumentNode.SelectSingleNode("//input[contains(@name, 'catalogId') and @value]");
            data.FormData["catalogId"] = nCatalogId?.Attributes["value"].Value ?? string.Empty;

            var nLangId = htmlDoc.DocumentNode.SelectSingleNode("//input[contains(@name, 'langId') and @value]");
            data.FormData["langId"] = nLangId?.Attributes["value"].Value ?? string.Empty;

            //total pages
            var nTotalPages = htmlDoc.DocumentNode.SelectSingleNode("(//div[contains(@class, 'pageControl number')]/a)[last()]");
            data.TotalPages = int.Parse(GetText(nTotalPages?.InnerText ?? "1"));

            //product feeds at first page
            data.ProductFeedsOfFirstPage = GetProductFeedsInListPage(feed, htmlDoc.DocumentNode);

            return data;
        }

        private List<DataFeed> GetProductFeedsInListPage(DataFeed feed, HtmlNode root)
        {
            var nProducts = root.SelectNodes("//div[starts-with(@id, 'entitledItem_')]/div[@data-catentryid-partnumber and @data-catentryidentifier]");

            if (nProducts == null || nProducts.Count == 0)
            {
                return new List<DataFeed>();
            }

            var productFeeds = new List<DataFeed>();

            foreach (var nProduct in nProducts)
            {
                var nLink = nProduct.SelectSingleNode(".//a[starts-with(@id, 'catalogEntry_') and @href]");

                if (nLink == null)
                {
                    continue;
                }

                var productFeed = feed.Clone(nLink.Attributes["href"].Value);
                productFeeds.Add(productFeed);

                //info
                productFeed.Product.Key = nProduct.Attributes["data-catentryidentifier"].Value;
                productFeed.Product.Sku = nProduct.Attributes["data-catentryid-partnumber"].Value;

                var nName = nProduct.SelectSingleNode(".//div[contains(@class, 'product_name')]/a");
                productFeed.Product.Name = HtmlDecode(nName?.InnerText);

                var nBrand = nProduct.SelectSingleNode(".//div[contains(@class, 'product_brand')]/a");
                productFeed.Product.Brand = Brands[HtmlDecode(nBrand?.InnerText)];

                var nPriceAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'offerPrice_') and contains(@class, 'sale_price')]/span[contains(@class, 'price')]");
                productFeed.Product.Price.Amount = GetAmount(nPriceAmount?.InnerText) ?? 0;

                var nPriceListAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'listPrice_') and contains(@class, 'list_price')]/span[contains(@class, 'price')]");
                productFeed.Product.Price.ListAmount = GetAmount(nPriceListAmount?.InnerText);

                var nPriceRegAmount = nProduct.SelectSingleNode(".//div[starts-with(@id, 'yourprice_') and contains(@class, 'reg_price')]/span[contains(@class, 'price')]");
                productFeed.Product.Price.RegAmount = GetAmount(nPriceRegAmount?.InnerText);
            }

            return productFeeds;
        }

        private async Task<Product> GetProductFullDetailsAsync(WebClient client, DataFeed feed)
        {
            Product product = null;

            try
            {
                var htmlContent = await client.DownloadStringTaskAsync(feed.Url);
                //var htmlContent = File.ReadAllText(@"C:\Users\Administrator\Desktop\productdetail.txt");

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                product = feed.Product;

                var docNode = htmlDoc.DocumentNode;

                //id
                var nId = docNode.SelectSingleNode("//*[starts-with(@id, 'entitledItem_')]");
                product.Key = nId?.Attributes["id"].Value;

                if (string.IsNullOrWhiteSpace(product.Key) || (product.Key = Regex.Replace(product.Key, "[^0-9]", string.Empty)) == string.Empty)
                {
                    return null;
                }

                //sku
                var nSku = docNode.SelectSingleNode($"//*[contains(@id, 'product_SKU_{product.Key}')]");
                product.Sku = nSku?.InnerText;

                //name
                var nName = docNode.SelectSingleNode($"//*[contains(@id, 'product_Name_{product.Key}')]");
                product.Name = HtmlDecode(nName?.InnerText);

                //price.amount
                var nPriceAmount = docNode.SelectSingleNode($"//*[contains(@id, 'offerPrice_{product.Key}')]/span[contains(@class, 'price')]");
                product.Price.Amount = GetAmount(nPriceAmount?.InnerText) ?? 0;

                //price.listamount
                var nPriceListAmount = docNode.SelectSingleNode($"//*[contains(@id, 'listPrice_{product.Key}')]/span[contains(@class, 'price')]");
                product.Price.ListAmount = GetAmount(nPriceListAmount?.InnerText);

                //price.regamount
                var nPriceRegAmount = docNode.SelectSingleNode($"//*[contains(@id, 'yourprice_{product.Key}')]/span[contains(@class, 'price')]");
                product.Price.RegAmount = GetAmount(nPriceRegAmount?.InnerText);

                //description
                var nDescription = docNode.SelectSingleNode($"//*[contains(@id, 'product_longdescription_{product.Key}')]");
                product.Description = HtmlDecode(nDescription?.InnerHtml);

                //attributes
                var nAttributes = docNode.SelectNodes("//table[contains(@class, 'descriptive_attributes')]//tr");
                PopulateProductAttributes(product, nAttributes);

                //brand
                ProductAttribute attrBrand;
                if (product.Brand == null && (attrBrand = product.Attributes.FirstOrDefault(n => n.Name == "Brand")) != null)
                {
                    product.Brand = Brands[attrBrand.Value];
                }

                //category breadcrumbs
                var nBreadcrumbs = docNode.SelectNodes("//div[contains(@id, 'widget_breadcrumb')]//ul/li/a[@href]");
                PopulateProductCategoryBreadcrumbs(product, nBreadcrumbs);

                //images
                var nImages = docNode.SelectNodes("//*[contains(@id, 'ProductAngleImagesAreaList')]//a[@data-large]");
                await PopulateProductImagesAsync(client, product, nImages);
            }
            catch (Exception ex)
            {
                // ignored
            }
         
            return product;
        }

        private void PopulateProductAttributes(Product product, HtmlNodeCollection nAttributes)
        {
            if (nAttributes == null || nAttributes.Count == 0)
            {
                return;
            }

            foreach (var nAttribute in nAttributes)
            {
                var nLable = nAttribute.SelectSingleNode("*[contains(@class, 'attr_label')]");
                var nValue = nAttribute.SelectSingleNode("*[contains(@class, 'attr_val')]");

                if (nLable != null && nValue != null)
                {
                    product.Attributes.Add(new ProductAttribute { Name = GetText(nLable.InnerText).TrimEnd(':'), Value = GetText(nValue.InnerText) });
                }
            }
        }

        private void PopulateProductCategoryBreadcrumbs(Product product, HtmlNodeCollection nBreadcrumbs)
        {
            if (nBreadcrumbs == null || nBreadcrumbs.Count == 0)
            {
                return;
            }

            foreach (var nBreadcrumb in nBreadcrumbs)
            {
                var category = GetText(nBreadcrumb.InnerText);

                if (category != "Home")
                {
                    product.CategoryBreadcrumbs.Add(category);
                }
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
                var productImage = await DownloadImageAsync(client, nImage, "data-large");

                if (productImage != null)
                {
                    product.Images.Add(productImage);
                }
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

        private double? GetAmount(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            double value;

            return double.TryParse(Regex.Replace(source, @"[^0-9\.]", string.Empty), out value) ? value : (double?)null;
        }

        private string HtmlDecode(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : HttpUtility.HtmlDecode(GetText(source));
        }

        private string GetText(string source)
        {
            return string.IsNullOrWhiteSpace(source) ? string.Empty : source.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty).Trim();
        }
    }

}
