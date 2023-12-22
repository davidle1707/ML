using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorldofWatches.Console
{
    public partial class FormMultiSelect : Form
    {
        private readonly List<SelectItem> _items;

        public FormMultiSelect(List<SelectItem> items)
        {
            InitializeComponent();

            _items = items ?? new List<SelectItem>();
        }

        private void FormMultiSelect_Load(object sender, EventArgs e)
        {
            BindItems();
        }

        private void BindItems()
        {
            if (_items == null || _items.Count == 0)
            {
                return;
            }

            foreach (var item in _items)
            {
                clbSelects.Items.Add(item, item.Selected);
            }

            clbSelects.DisplayMember = "Name";
        }
        
        private void FormMultiSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_items == null || _items.Count == 0)
            {
                return;
            }

            for (var i = 0; i < clbSelects.Items.Count; i++)
            {
                _items[i].Selected = clbSelects.GetItemChecked(i);
            }

            DialogResult = DialogResult.OK;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (var i = 0; i < clbSelects.Items.Count; i++)
            {
                clbSelects.SetItemChecked(i, true);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (var i = 0; i < clbSelects.Items.Count; i++)
            {
                clbSelects.SetItemChecked(i, false);
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
