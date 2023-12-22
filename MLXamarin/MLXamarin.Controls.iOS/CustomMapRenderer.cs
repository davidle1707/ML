using System;
using CoreLocation;
using MapKit;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MLXamarin.Controls;
using MLXamarin.Controls.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]

namespace MLXamarin.Controls.iOS
{
    public class CustomMapRenderer : MapRenderer, ICustomMapFunctionsRenderer
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        private MKMapView NativeMap => Control as MKMapView;

        private CustomMap FormsMap => Element as CustomMap;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnMapPropertyChanged;

                NativeMap.OverlayRenderer = null;
            }

            if (e.NewElement != null)
            {
                FormsMap.SetRenderer(this);

                Element.PropertyChanged += OnMapPropertyChanged;

                RenderRouteCoordinatesOverlay(e.NewElement as CustomMap);
            }
        }

        MKPolyline _currentRouteOverlay;

        void RenderRouteCoordinatesOverlay(CustomMap formsMap)
        {
            if (formsMap == null)
            {
                return;
            }

            var coords = formsMap.GetRoutes().Select(c => new CLLocationCoordinate2D(c.Latitude, c.Longitude));
            _currentRouteOverlay = MKPolyline.FromCoordinates(coords.ToArray());

            var objNativeMap = NativeMap;
            objNativeMap.OverlayRenderer = GetOverlayRenderer;
            objNativeMap.AddOverlay(_currentRouteOverlay);
        }

        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlay)
        {
            return new MKPolylineRenderer(overlay as MKPolyline)
            {
                FillColor = UIColor.Blue,
                StrokeColor = UIColor.Red,
                LineWidth = 3,
                Alpha = 0.4f
            };
        }

        private void OnMapPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RouteCoordinatesChanged")
            {
                if (_currentRouteOverlay != null)
                {
                    NativeMap.RemoveOverlay(_currentRouteOverlay);
                }

                RenderRouteCoordinatesOverlay(FormsMap);
            }
        }

        /// <summary>
        /// https://github.com/TorbenK/TK.CustomMap/blob/master/TK.CustomMap/TK.CustomMap.iOSUnified/TKCustomMapRenderer.cs
        /// </summary>
        public Task<byte[]> GetSnapshotAsync()
        {
            UIGraphics.BeginImageContextWithOptions(Frame.Size, false, 0.0f);

            Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var img = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return Task.FromResult(img.AsJPEG().ToArray());
        }

    }
}
