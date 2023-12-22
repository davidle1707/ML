using Xamarin.Forms;

namespace MLXamarin.Controls
{
    /// <summary>
    /// https://github.com/andreinitescu/IconApp
    /// </summary>
    public class CustomIconView : View
    {
        #region ForegroundProperty

        public static readonly BindableProperty ForegroundProperty = BindableProperty.Create("Foreground", typeof(Color), typeof(CustomIconView), default(Color));

        public Color Foreground
        {
            get { return (Color)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        #endregion

        #region SourceProperty

        public static readonly BindableProperty SourceProperty = BindableProperty.Create("Source", typeof(string), typeof(CustomIconView), default(string));

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        #endregion
    }
}
