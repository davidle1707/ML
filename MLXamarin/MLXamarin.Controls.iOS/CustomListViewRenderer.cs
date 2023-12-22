using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace MLXamarin.Controls.iOS
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as CustomListView;

            if (view != null)
            {
                SetHasScroll(view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (CustomListView)Element;

            if (e.PropertyName == CustomListView.HasScrollProperty.PropertyName)
            {
                SetHasScroll(view);
            }
        }

        private void SetHasScroll(CustomListView view)
        {
            Control.ScrollEnabled = view.HasScroll;
        }

    }
}
