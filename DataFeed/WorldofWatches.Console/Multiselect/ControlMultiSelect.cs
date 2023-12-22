using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace WorldofWatches.Console
{
    public partial class ControlMultiSelect : UserControl
    {
        private readonly List<SelectItem> _items;

        public ControlMultiSelect()
        {
            InitializeComponent();

            _items = new List<SelectItem>();
        }

        public void SetItems(List<SelectItem> items)
        {
            _items.Clear();
            _items.AddRange(items);

            LoadNames();
        }

        public List<string> Values => _items.Where(i => i.Selected).Select(i => i.Value).ToList();
        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            var form = new FormMultiSelect(_items);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadNames();
            }
        }

        private void LoadNames()
        {
            var names = _items.Where(i => i.Selected).Select(i => i.Name).Skip(0).Take(10);
            txtNames.Text = $"{string.Join(", ", names)} ...";
        }
    }
}
