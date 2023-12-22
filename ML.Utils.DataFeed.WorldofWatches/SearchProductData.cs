using System.Collections.Generic;
using System.Collections.Specialized;

namespace ML.Utils.DataFeed.WorldofWatches
{
    internal class SearchProductData
    {
        public static readonly string SearchUrl = $"{WorldofWatchesManager.RootUrl}/ProductListingView";

        public NameValueCollection QueryString { get; set; } = new NameValueCollection();

        public NameValueCollection FormData { get; set; } = new NameValueCollection
            {
                { "contentBeginIndex", "0"},
                { "productBeginIndex", "0"},
                { "beginIndex", "0"},
                { "orderBy", ""},
                { "facetId", ""},
                { "pageView", "grid"},
                { "resultType", "products"},
                { "orderByContent", ""},
                { "searchTerm", ""},
                { "facet", ""},
                { "facetLimit", ""},
                { "minPrice", ""},
                { "maxPrice", ""},
                { "pageSize", "30"},
                { "storeId", ""}, // from UI
                { "catalogId", ""}, //from UI
                { "langId", "-1"}, //from UI
                { "requesttype", "ajax"},
            };

        public int TotalPages { get; set; } = 1;

        public List<DataFeed> ProductFeedsOfFirstPage { get; set; } = new List<DataFeed>();

        public void SetPage(int page)
        {
            var beginIndex = page < TotalPages ? (page - 1) * 30 : 0;

            FormData["productBeginIndex"] = beginIndex.ToString();
            FormData["beginIndex"] = beginIndex.ToString();
        }

        public void SendBrands(List<Brand> brands)
        {
            if (brands.Count == 0)
            {
                return;
            }

            FormData["facetId"] = brands[0].Key;

            for (var i = 1; i < brands.Count; i++)
            {
                FormData.Add("facetId", brands[i].Key);
            }
        }
    }
}
