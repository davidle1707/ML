using System;
using Android.Graphics;
using Android.Widget;
using System.ComponentModel;
using MLXamarin.Controls.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomIconViewRenderer), typeof(CustomIconViewRenderer))]
namespace MLXamarin.Controls.Android
{
    public class CustomIconViewRenderer : ViewRenderer<CustomIconView, ImageView>
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        private bool _isDisposed;

        public CustomIconViewRenderer()
        {
            base.AutoPackage = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomIconView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                SetNativeControl(new ImageView(Context));
            }
            UpdateBitmap(e.OldElement);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomIconView.SourceProperty.PropertyName)
            {
                UpdateBitmap(null);
            }
            else if (e.PropertyName == CustomIconView.ForegroundProperty.PropertyName)
            {
                UpdateBitmap(null);
            }
        }

        private void UpdateBitmap(CustomIconView previous = null)
        {
            if (!_isDisposed)
            {
                var d = Resources.GetDrawable(Element.Source).Mutate();
                //d.SetColorFilter(new LightingColorFilter(Element.Foreground.ToAndroid(), Element.Foreground.ToAndroid()));
                d.SetTint(Element.Foreground.ToAndroid());
                d.Alpha = Element.Foreground.ToAndroid().A;
                Control.SetImageDrawable(d);
                ((IVisualElementController)Element).NativeSizeChanged();
            }
        }
    }
}