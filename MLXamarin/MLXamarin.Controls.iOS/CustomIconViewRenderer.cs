﻿using System;
using System.ComponentModel;

using Xamarin.Forms;
using UIKit;
using Xamarin.Forms.Platform.iOS;

using CoreGraphics;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;

[assembly: ExportRenderer(typeof(CustomIconView), typeof(CustomIconViewRenderer))]
namespace MLXamarin.Controls.iOS
{
    public class CustomIconViewRenderer : ViewRenderer<CustomIconView, UIImageView>
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        private bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing && base.Control != null)
            {
                UIImage image = base.Control.Image;
                UIImage uIImage = image;
                if (image != null)
                {
                    uIImage.Dispose();
                    uIImage = null;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomIconView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                UIImageView uIImageView = new UIImageView(CGRect.Empty)
                {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    ClipsToBounds = true
                };
                SetNativeControl(uIImageView);
            }
            if (e.NewElement != null)
            {
                SetImage(e.OldElement);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomIconView.SourceProperty.PropertyName)
            {
                SetImage(null);
            }
            else if (e.PropertyName == CustomIconView.ForegroundProperty.PropertyName)
            {
                SetImage(null);
            }
        }

        private void SetImage(CustomIconView previous = null)
        {
            if (previous == null)
            {
                var uiImage = new UIImage(Element.Source);
                uiImage = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                Control.TintColor = Element.Foreground.ToUIColor();
                Control.Image = uiImage;
                if (!_isDisposed)
                {
                    ((IVisualElementController)Element).NativeSizeChanged();
                }
            }
        }
    }
}
