using ML.Utils.DataFeed.WorldofWatches;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WorldofWatches.Console
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            BindProductTypes();
            BindBrands();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!grbInfo.Enabled)
            {
                e.Cancel = true;
                MessageBox.Show("Feed is running. Please wait...");
            }
        }

        private async void btnGetProducts_Click(object sender, EventArgs e)
        {
            //await ProductManager.Instance.Test();
            //return;

            var feeds = new List<DataFeed>();

            if (!GetDataFeeds(ref feeds))
            {
                return;
            }

            EnableControls(false);

            await InternalManager.Instance.GetProducts(feeds, (int)nudStartPage.Value, (int)nudStartRecordIndex.Value - 1, (int)nudDelaySecondsEachPage.Value * 1000, (int)nudDelaySecondsEachProduct.Value * 1000, rtbReport);

            EnableControls(true);
            rtbReport.AppendText(Environment.NewLine);
        }

        private async void btnUpdatePriceProducts_Click(object sender, EventArgs e)
        {
            var feeds = new List<DataFeed>();

            if (!GetDataFeeds(ref feeds))
            {
                return;
            }

            EnableControls(false);

            await InternalManager.Instance.UpdatePriceProducts(feeds, (int)nudStartPage.Value, (int)nudStartRecordIndex.Value - 1, (int)nudDelaySecondsEachPage.Value * 1000, (int)nudDelaySecondsEachProduct.Value * 1000, rtbReport);

            EnableControls(true);
            rtbReport.AppendText(Environment.NewLine);
        }

        private void lbnClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rtbReport.Clear();
        }

        private bool GetDataFeeds(ref List<DataFeed> feeds)
        {
            var selectedProductTypes = cmsProductTypes.Values;

            if (selectedProductTypes.Count == 0)
            {
                MessageBox.Show("Please select product(s)");
                return false;
            }

            rtbReport.AppendText($"--- Date: {DateTime.Now} -------------");

            var productTypes = selectedProductTypes.Select(p => (ProductType)int.Parse(p)).ToList();
            rtbReport.AppendText(Environment.NewLine + $"--- Products: {string.Join(", ", productTypes.Select(p => p.ToString()))}");

            var selectedBrandKeys = cmsBrands.Values;

            var brands = WorldofWatchesManager.Brands.Where(b => selectedBrandKeys.Contains(b.Key)).ToList();
            rtbReport.AppendText(Environment.NewLine + $"--- Brands: {string.Join(", ", brands.Select(p => p.Name))}");

            feeds = InternalManager.Instance.GetDataFeeds(productTypes, selectedBrandKeys.ToArray());

            rtbReport.AppendText(Environment.NewLine);

            return feeds.Count > 0;
        }

        private void BindProductTypes()
        {
            cmsProductTypes.SetItems(Enum.GetValues(typeof(ProductType)).Cast<ProductType>().Select(b => new SelectItem { Name = b.ToString(), Value = ((int)b).ToString(), Selected = true }).ToList());
        }

        private void BindBrands()
        {
            cmsBrands.SetItems(WorldofWatchesManager.Brands.OrderBy(b => b.Name).Select(b => new SelectItem { Name = b.Name, Value = b.Key, Selected = true }).ToList());
        }

        private void EnableControls(bool enabled)
        {
            grbInfo.Enabled = enabled;
            btnGetProducts.Enabled = enabled;
            btnUpdatePriceProducts.Enabled = enabled;
        }
    }
}
