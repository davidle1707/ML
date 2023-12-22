using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MLXamarin.Controls
{
    /// <summary>
    /// https://developer.xamarin.com/recipes/cross-platform/xamarin-forms/maps/map-overlay/polyline/
    /// </summary>
    public class CustomMap : Map, ICustomMapFunctions, ICustomMapFunctionsRenderer
    {
        private readonly List<Position> _routeCoordinates;

        public CustomMap()
        {
            _routeCoordinates = new List<Position>();
        }

        public void AddRoutes(params Position[] positions)
        {
            if (positions.Length == 0)
            {
                return;
            }

            _routeCoordinates.AddRange(positions);
            OnPropertyChanged("RouteCoordinatesChanged");

        }

        public List<Position> GetRoutes()
        {
            return _routeCoordinates;
        }

        private ICustomMapFunctionsRenderer _functionsRenderer;

        public void SetRenderer(ICustomMapFunctionsRenderer render)
        {
            _functionsRenderer = render;
        }

        public Task<byte[]> GetSnapshotAsync()
        {
            return _functionsRenderer.GetSnapshotAsync();
        }
    }

    public interface ICustomMapFunctions
    {
        void SetRenderer(ICustomMapFunctionsRenderer renderer);
    }

    public interface ICustomMapFunctionsRenderer
    {
        Task<byte[]> GetSnapshotAsync();
    }

}
