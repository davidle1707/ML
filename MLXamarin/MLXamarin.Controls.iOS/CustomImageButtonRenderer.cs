﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CoreGraphics;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomImageButton), typeof(CustomImageButtonRenderer))]
namespace MLXamarin.Controls.iOS
{
    //https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/src/Forms/XLabs.Forms.iOS/Controls/ImageButton/ImageButtonRenderer.cs
    public class CustomImageButtonRenderer : ButtonRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        /// <summary>
        /// The padding to use in the control.
        /// </summary>
        private const int CONTROL_PADDING = 2;

        /// <summary>
        /// Identifies the iPad.
        /// </summary>
        private const string IPAD = "iPad";

        /// <summary>
        /// Gets the underlying element typed as an <see cref="ImageButton"/>.
        /// </summary>
        private CustomImageButton ImageButton
        {
            get { return (CustomImageButton)Element; }
        }

        /// <summary>
        /// Handles the initial drawing of the button.
        /// </summary>
        /// <param name="e">Information on the <see cref="ImageButton"/>.</param> 
        protected override async void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var imageButton = ImageButton;
            var targetButton = Control;
            if (imageButton != null && targetButton != null && imageButton.Source != null)
            {
                // Matches Android ImageButton behavior
                targetButton.LineBreakMode = UIKit.UILineBreakMode.WordWrap;

                var width = this.GetWidth(imageButton.ImageWidthRequest);
                var height = this.GetHeight(imageButton.ImageHeightRequest);



                await SetupImages(imageButton, targetButton, width, height);

                switch (imageButton.Orientation)
                {
                    case ImageOrientation.ImageToLeft:
                        AlignToLeft(targetButton);
                        break;
                    case ImageOrientation.ImageToRight:
                        AlignToRight(imageButton.ImageWidthRequest, targetButton);
                        break;
                    case ImageOrientation.ImageOnTop:
                        AlignToTop(imageButton.ImageHeightRequest, imageButton.ImageWidthRequest, targetButton, imageButton.WidthRequest);
                        break;
                    case ImageOrientation.ImageOnBottom:
                        AlignToBottom(imageButton.ImageHeightRequest, imageButton.ImageWidthRequest, targetButton);
                        break;
                }
            }
        }

        /// <summary>
        /// Called when the underlying model's properties are changed.
        /// </summary>
        /// <param name="sender">Model sending the change event.</param>
        /// <param name="e">Event arguments.</param>
        protected async override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomImageButton.SourceProperty.PropertyName ||
                e.PropertyName == CustomImageButton.DisabledSourceProperty.PropertyName ||
                e.PropertyName == CustomImageButton.ImageTintColorProperty.PropertyName ||
                e.PropertyName == CustomImageButton.DisabledImageTintColorProperty.PropertyName)
            {
                var sourceButton = Element as CustomImageButton;
                if (sourceButton != null && sourceButton.Source != null)
                {
                    var imageButton = ImageButton;
                    var targetButton = Control;
                    if (imageButton != null && targetButton != null && imageButton.Source != null)
                    {
                        await SetupImages(imageButton, targetButton, imageButton.ImageWidthRequest, imageButton.ImageHeightRequest);
                    }
                }
            }
        }

        async Task SetupImages(CustomImageButton imageButton, UIButton targetButton, int width, int height)
        {
            UIColor tintColor = imageButton.ImageTintColor == Color.Transparent ? null : imageButton.ImageTintColor.ToUIColor();
            UIColor disabledTintColor = imageButton.DisabledImageTintColor == Color.Transparent ? null : imageButton.DisabledImageTintColor.ToUIColor();

            await SetImageAsync(imageButton.Source, width, height, targetButton, UIControlState.Normal, tintColor);

            if (imageButton.DisabledSource != null || disabledTintColor != null)
            {
                await SetImageAsync(imageButton.DisabledSource ?? imageButton.Source, width, height, targetButton, UIControlState.Disabled, disabledTintColor);
            }
        }

        /// <summary>
        /// Properly aligns the title and image on a button to the left.
        /// </summary>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToLeft(UIButton targetButton)
        {
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Left;

            var titleInsets = new UIEdgeInsets(0, CONTROL_PADDING, 0, -1 * CONTROL_PADDING);
            targetButton.TitleEdgeInsets = titleInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button to the right.
        /// </summary>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToRight(int widthRequest, UIButton targetButton)
        {
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Right;

            var titleInsets = new UIEdgeInsets(0, 0, 0, widthRequest + CONTROL_PADDING);

            targetButton.TitleEdgeInsets = titleInsets;
            var imageInsets = new UIEdgeInsets(0, widthRequest, 0, -1 * widthRequest);
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button when the image is over the title.
        /// </summary>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        /// <param name="buttonWidthRequest">The button width request.</param>
        private static void AlignToTop(int heightRequest, int widthRequest, UIButton targetButton, double buttonWidthRequest)
        {
            targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Top;
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Center;
            targetButton.TitleLabel.LineBreakMode = UIKit.UILineBreakMode.WordWrap;

            targetButton.SizeToFit();

            var titleWidth = targetButton.TitleLabel.IntrinsicContentSize.Width;
            CGSize titleSize = targetButton.TitleLabel.Frame.Size;

            UIEdgeInsets titleInsets;
            UIEdgeInsets imageInsets;

            if (UIDevice.CurrentDevice.Model.Contains(IPAD))
            {
                titleInsets = new UIEdgeInsets(heightRequest, Convert.ToInt32(-1 * widthRequest / 2), -1 * heightRequest, Convert.ToInt32(widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, Convert.ToInt32(titleWidth / 2), 0, -1 * Convert.ToInt32(titleWidth / 2));
            }
            else
            {
                titleInsets = new UIEdgeInsets(heightRequest, Convert.ToInt32(-1 * widthRequest / 2), -1 * heightRequest, Convert.ToInt32(widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, Convert.ToInt32(targetButton.IntrinsicContentSize.Width / 2 - widthRequest / 2 - titleSize.Width / 2), 0, Convert.ToInt32(-1 * (targetButton.IntrinsicContentSize.Width / 2 - widthRequest / 2 + titleSize.Width / 2)));
            }

            targetButton.TitleEdgeInsets = titleInsets;
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Properly aligns the title and image on a button when the title is over the image.
        /// </summary>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="targetButton">The button to align.</param>
        private static void AlignToBottom(int heightRequest, int widthRequest, UIButton targetButton)
        {
            targetButton.VerticalAlignment = UIControlContentVerticalAlignment.Bottom;
            targetButton.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            targetButton.TitleLabel.TextAlignment = UITextAlignment.Center;
            targetButton.SizeToFit();
            var titleWidth = targetButton.TitleLabel.IntrinsicContentSize.Width;

            UIEdgeInsets titleInsets;
            UIEdgeInsets imageInsets;

            if (UIDevice.CurrentDevice.Model.Contains(IPAD))
            {
                titleInsets = new UIEdgeInsets(-1 * heightRequest, Convert.ToInt32(-1 * widthRequest / 2), heightRequest, Convert.ToInt32(widthRequest / 2));
                imageInsets = new UIEdgeInsets(0, titleWidth / 2, 0, -1 * titleWidth / 2);
            }
            else
            {
                titleInsets = new UIEdgeInsets(-1 * heightRequest, -1 * widthRequest, heightRequest, widthRequest);
                imageInsets = new UIEdgeInsets(0, 0, 0, 0);
            }

            targetButton.TitleEdgeInsets = titleInsets;
            targetButton.ImageEdgeInsets = imageInsets;
        }

        /// <summary>
        /// Loads an image from a bundle given the supplied image name, resizes it to the
        /// height and width request and sets it into a <see cref="UIButton" />.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource" /> to load the image from.</param>
        /// <param name="widthRequest">The requested image width.</param>
        /// <param name="heightRequest">The requested image height.</param>
        /// <param name="targetButton">A <see cref="UIButton" /> to set the image into.</param>
        /// <param name="state">The state.</param>
        /// <param name="tintColor">Color of the tint.</param>
        /// <returns>A <see cref="Task" /> for the awaited operation.</returns>
        private async static Task SetImageAsync(ImageSource source, int widthRequest, int heightRequest, UIButton targetButton, UIControlState state = UIControlState.Normal, UIColor tintColor = null)
        {
            var handler = GetHandler(source);
            using (UIImage image = await handler.LoadImageAsync(source))
            {
                UIImage scaled = image;
                if (heightRequest > 0 && widthRequest > 0 && (image.Size.Height != heightRequest || image.Size.Width != widthRequest))
                {
                    scaled = scaled.Scale(new CGSize(widthRequest, heightRequest));
                }

                if (tintColor != null)
                {
                    targetButton.TintColor = tintColor;
                    targetButton.SetImage(scaled.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate), state);
                }
                else
                    targetButton.SetImage(scaled.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal), state);
            }
        }

        /// <summary>
        /// Layouts the subviews.
        /// </summary>
        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            if (ImageButton.Orientation == ImageOrientation.ImageToRight)
            {
                var imageInsets = new UIEdgeInsets(0, Control.Frame.Size.Width - CONTROL_PADDING - ImageButton.ImageWidthRequest, 0, 0);
                Control.ImageEdgeInsets = imageInsets;
            }
        }

        /// <summary>
        /// Returns the proper <see cref="IImageSourceHandler"/> based on the type of <see cref="ImageSource"/> provided.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource"/> to get the handler for.</param>
        /// <returns>The needed handler.</returns>
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }

        /// <summary>
        /// Gets the width based on the requested width, if request less than 0, returns 50.
        /// </summary>
        /// <param name="requestedWidth">The requested width.</param>
        /// <returns>The width to use.</returns>
        private int GetWidth(int requestedWidth)
        {
            const int DefaultWidth = 50;
            return requestedWidth <= 0 ? DefaultWidth : requestedWidth;
        }

        /// <summary>
        /// Gets the height based on the requested height, if request less than 0, returns 50.
        /// </summary>
        /// <param name="requestedHeight">The requested height.</param>
        /// <returns>The height to use.</returns>
        private int GetHeight(int requestedHeight)
        {
            const int DefaultHeight = 50;
            return requestedHeight <= 0 ? DefaultHeight : requestedHeight;
        }
    }
}
