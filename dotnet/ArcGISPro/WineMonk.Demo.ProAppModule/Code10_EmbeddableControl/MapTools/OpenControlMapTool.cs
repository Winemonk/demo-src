using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;
using System.Text;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code10_EmbeddableControl.MapTools
{
    internal class OpenControlMapTool : MapTool
    {
        public OpenControlMapTool()
        {
            //Set the tools OverlayControlID to the DAML id of the embeddable control
            OverlayControlID = "WineMonk_Demo_ProAppModule_Code10_EmbeddableControl_EmbeddableControlTest";
            //Embeddable control can be resized
            OverlayControlCanResize = true;
            //Specify ratio of 0 to 1 to place the control
            OverlayControlPositionRatio = new System.Windows.Point(0, 0); //top left
        }

        protected override void OnToolMouseDown(MapViewMouseButtonEventArgs args)
        {
            // On mouse down check if the mouse button pressed is the left mouse button. 
            // If it is handle the event.
            if (args.ChangedButton == System.Windows.Input.MouseButton.Left)
                args.Handled = true;
        }

        protected override Task HandleMouseDownAsync(MapViewMouseButtonEventArgs args)
        {
            //Get the instance of the ViewModel
            var vm = OverlayEmbeddableControl as EmbeddableControlTestViewModel;
            if (vm == null)
                return Task.FromResult(0);

            //Get the map coordinates from the click point and set the property on the ViewModel.
            return QueuedTask.Run(() =>
            {
                var mapPoint = ActiveMapView.ClientToMap(args.ClientPoint);
                var coords = GeometryEngine.Instance.Project(mapPoint, SpatialReferences.WGS84) as MapPoint;
                if (coords == null) return;
                var sb = new StringBuilder();
                sb.AppendLine($"X: {coords.X:0.000}");
                sb.Append($"Y: {coords.Y:0.000}");
                if (coords.HasZ)
                {
                    sb.AppendLine();
                    sb.Append($"Z: {coords.Z:0.000}");
                }
                vm.Text = sb.ToString();
            });
        }

        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }

        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            return base.OnSketchCompleteAsync(geometry);
        }
    }
}
