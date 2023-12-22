using System;
using Xamarin.Forms;

namespace MLXamarin.Controls
{
    public class CustomTimePicker : TimePicker
    {
        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(CustomTimePicker), true);
     
        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        /// <value><c>true</c> if this instance has border; otherwise, <c>false</c>.</value>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

    }
}
