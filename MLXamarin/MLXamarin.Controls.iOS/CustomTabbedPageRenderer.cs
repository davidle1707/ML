using System;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderer))]
namespace MLXamarin.Controls.iOS
{
    //https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Forms/XLabs.Forms.iOS/Controls/ExtendedTabbedPage/ExtendedTabbedPageRenderer.cs
    public class CustomTabbedPageRenderer : TabbedRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnPropertyChanged;
            }

            if (e.NewElement != null)
            {
                Element.PropertyChanged += OnPropertyChanged;
            }

            var page = (CustomTabbedPage)Element;

            TabBar.TintColor = page.TintColor.ToUIColor();
            TabBar.BarTintColor = page.BarTintColor.ToUIColor();
            TabBar.BackgroundColor = page.BackgroundColor.ToUIColor();
            
            if (!page.SwipeEnabled)
            {
                return;
            }

            var gestureRight = new UISwipeGestureRecognizer(sw =>
            {
                sw.ShouldReceiveTouch += (recognizer, touch) => !(touch.View is UITableView) && !(touch.View is UITableViewCell);

                if (sw.Direction == UISwipeGestureRecognizerDirection.Right)
                {
                    page.InvokeSwipeLeftEvent(null, null);
                }

            })
            { Direction = UISwipeGestureRecognizerDirection.Right };

            var gestureLeft = new UISwipeGestureRecognizer(sw =>
            {
                sw.ShouldReceiveTouch += (recognizer, touch) => !(touch.View is UITableView) && !(touch.View is UITableViewCell);

                if (sw.Direction == UISwipeGestureRecognizerDirection.Left)
                {
                    page.InvokeSwipeRightEvent(null, null);
                }

            })
            { Direction = UISwipeGestureRecognizerDirection.Left };

            View.AddGestureRecognizer(gestureRight);
            View.AddGestureRecognizer(gestureLeft);
        }

        /// <summary>
        /// Views the did load.
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        /// <summary>
        /// Views the will appear.
        /// </summary>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var page = (CustomTabbedPage)Element;

            if (!string.IsNullOrEmpty(page.TabBarSelectedImage))
            {
                TabBar.SelectionIndicatorImage = UIImage.FromFile(page.TabBarSelectedImage).CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0), UIImageResizingMode.Stretch);
            }

            if (!string.IsNullOrEmpty(page.TabBarBackgroundImage))
            {
                TabBar.BackgroundImage = UIImage.FromFile(page.TabBarBackgroundImage).CreateResizableImage(new UIEdgeInsets(0, 0, 0, 0), UIImageResizingMode.Stretch);
            }

            RenderBadges(page);
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Badges")
            {
                RenderBadges(Element as CustomTabbedPage);
            }
        }

        private void RenderBadges(CustomTabbedPage page)
        {
            if (TabBar.Items == null || TabBar.Items.Length == 0 || page.Badges == null || page.Badges.Count == 0)
            {
                return;
            }

            var lengthTabBarItems = TabBar.Items.Length;

            for (var i = 0; i < page.Badges.Count; i++)
            {
                if (i >= lengthTabBarItems)
                {
                    continue;
                }

                TabBar.Items[i].BadgeValue = page.Badges[i];
            }
        }
    }
}
