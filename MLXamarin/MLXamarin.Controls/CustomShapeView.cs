using System;
using Xamarin.Forms;

namespace MLXamarin.Controls
{
    //https://github.com/chrispellett/Xamarin-Forms-Shape/blob/master/DrawShape/ShapeView.cs
    public class CustomShapeView : BoxView
    {
        #region ShapeTypeProperty

        public static readonly BindableProperty ShapeTypeProperty = BindableProperty.Create("ShapeType", typeof(ShapeType), typeof(CustomShapeView), ShapeType.Box);

        public ShapeType ShapeType
        {
            get { return (ShapeType)GetValue(ShapeTypeProperty); }
            set { SetValue(ShapeTypeProperty, value); }
        }

        #endregion

        #region StrokeColorProperty

        public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create("StrokeColor", typeof(Color), typeof(CustomShapeView), Color.Default);

        public Color StrokeColor
        {
            get { return (Color)GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        #endregion

        #region StrokeWidthProperty

        public static readonly BindableProperty StrokeWidthProperty = BindableProperty.Create("StrokeWidth", typeof(float), typeof(CustomShapeView), 1f);

        public float StrokeWidth
        {
            get { return (float)GetValue(StrokeWidthProperty); }
            set { SetValue(StrokeWidthProperty, value); }
        }

        #endregion

        #region IndicatorPercentageProperty

        public static readonly BindableProperty IndicatorPercentageProperty = BindableProperty.Create("IndicatorPercentage", typeof(float), typeof(CustomShapeView), 0f);

        public float IndicatorPercentage
        {
            get { return (float)GetValue(IndicatorPercentageProperty); }
            set
            {
                if (ShapeType != ShapeType.CircleIndicator)
                    throw new ArgumentException("Can only specify this property with CircleIndicator");
                SetValue(IndicatorPercentageProperty, value);
            }
        }

        #endregion

        #region CornerRadiusProperty

        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(float), typeof(CustomShapeView), 0f);

        public float CornerRadius
        {
            get { return (float)GetValue(CornerRadiusProperty); }
            set
            {
                if (ShapeType != ShapeType.Box)
                    throw new ArgumentException("Can only specify this property with Box");
                SetValue(CornerRadiusProperty, value);
            }
        }

        #endregion

        #region PaddingProperty

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create("Padding", typeof(Thickness), typeof(CustomShapeView), default(Thickness));

        public Thickness Padding
        {
            get { return (Thickness)GetValue(PaddingProperty); }
            set { SetValue(PaddingProperty, value); }
        }

        #endregion
    }

    public enum ShapeType
    {
        Box,
        Circle,
        CircleIndicator
    }
}
