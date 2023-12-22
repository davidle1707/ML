using System;
using System.ComponentModel;
using CoreGraphics;
using Foundation;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomTimePicker), typeof(CustomTimePickerRenderer))]
namespace MLXamarin.Controls.iOS
{
    public class CustomTimePickerRenderer : TimePickerRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// The on element changed callback.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as CustomTimePicker;

            if (view != null)
            {
               SetBorder(view);
            }

        }

        /// <summary>
        /// The on element property changed callback
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (CustomTimePicker)Element;

            
            if (e.PropertyName == CustomTimePicker.HasBorderProperty.PropertyName)
                SetBorder(view);
            
        }
        
        /// <summary>
        /// Sets the border.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetBorder(CustomTimePicker view)
        {
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }
        
    }
}
