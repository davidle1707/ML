using System;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using MLXamarin.Controls;
using MLXamarin.Controls.Android;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MLXamarin.Controls.Android
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback, ICustomMapFunctionsRenderer, GoogleMap.ISnapshotReadyCallback
    {
        public static void Register()
        {
            var time = DateTime.UtcNow;
        }

        GoogleMap _gmap;

        List<Position> _routeCoordinates;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                _routeCoordinates = formsMap.GetRoutes();

                ((MapView)Control).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _gmap = googleMap;

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in _routeCoordinates)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));
            }

            _gmap.AddPolyline(polylineOptions);
        }

        private byte[] _snapShot;

        /// <summary>
        /// https://github.com/TorbenK/TK.CustomMap/blob/master/TK.CustomMap/TK.CustomMap.Android/TKCustomMapRenderer.cs
        /// </summary>
        public async Task<byte[]> GetSnapshotAsync()
        {
            if (_gmap == null)
            {
                return null;
            }

            _snapShot = null;
            _gmap.Snapshot(this);

            while (_snapShot == null)
            {
                await Task.Delay(10);
            }

            return _snapShot;
        }

        public void OnSnapshotReady(Bitmap snapshot)
        {
            using (var strm = new MemoryStream())
            {
                snapshot.Compress(Bitmap.CompressFormat.Png, 100, strm);
                _snapShot = strm.ToArray();
            }
        }
    }
}