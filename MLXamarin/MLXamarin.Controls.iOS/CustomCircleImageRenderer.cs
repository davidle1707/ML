using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Diagnostics;
using Foundation;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;


[assembly: ExportRenderer(typeof(CustomCircleImage), typeof(CustomCircleImageRenderer))]
namespace MLXamarin.Controls.iOS
{
    [Preserve(AllMembers = true)]
    public class CustomCircleImageRenderer : ImageRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
                return;
            CreateCircle();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
              e.PropertyName == CustomCircleImage.BorderColorProperty.PropertyName ||
              e.PropertyName == CustomCircleImage.BorderThicknessProperty.PropertyName ||
              e.PropertyName == CustomCircleImage.FillColorProperty.PropertyName)
            {
                CreateCircle();
            }
        }

        private void CreateCircle()
        {
            try
            {
                double min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = ((CustomCircleImage)Element).BorderColor.ToCGColor();
                Control.Layer.BorderWidth = ((CustomCircleImage)Element).BorderThickness;
                Control.BackgroundColor = ((CustomCircleImage)Element).FillColor.ToUIColor();
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }
    }
}
