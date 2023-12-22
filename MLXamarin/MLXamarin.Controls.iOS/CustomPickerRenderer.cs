using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Foundation;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace MLXamarin.Controls.iOS
{
    public class CustomPickerRenderer : PickerRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            var view = e.NewElement as CustomPicker;

            if (view != null)
            {
                SetFont(view);

                SetTextColor(view);

                SetBorder(view);

                SetPlaceholderTextColor(view);

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

            var view = (CustomPicker)Element;

            if (e.PropertyName == CustomPicker.FontProperty.PropertyName)
                SetFont(view);

            if (e.PropertyName == CustomPicker.TextColorProperty.PropertyName)
                SetTextColor(view);

            if (e.PropertyName == CustomPicker.HasBorderProperty.PropertyName)
                SetBorder(view);

            if (e.PropertyName == CustomPicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

        }

        /// <summary>
        /// Sets the border.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetBorder(CustomPicker view)
        {
            Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetFont(CustomPicker view)
        {
            UIFont uiFont;
            if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
                Control.Font = uiFont;
            else if (view.Font == Font.Default)
                Control.Font = UIFont.SystemFontOfSize(17f);
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetPlaceholderTextColor(CustomPicker view)
        {
            if (string.IsNullOrEmpty(view.Title) == false && view.PlaceholderTextColor != Color.Default)
            {
                NSAttributedString placeholderString = new NSAttributedString(view.Title, new UIStringAttributes() { ForegroundColor = view.PlaceholderTextColor.ToUIColor() });
                Control.AttributedPlaceholder = placeholderString;
            }
        }

        /// <summary>
        /// Sets the color of the text.
        /// </summary>
        /// <param name="view">The view.</param>
        void SetTextColor(CustomPicker view)
        {
            Control.TextColor = view.TextColor.ToUIColor();
        }
    }
}
