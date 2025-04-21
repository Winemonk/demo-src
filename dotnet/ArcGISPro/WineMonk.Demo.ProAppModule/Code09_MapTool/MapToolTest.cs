using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Mapping;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code09_MapTool
{
    internal class MapToolTest : MapTool
    {
        public MapToolTest()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Rectangle;
            SketchOutputMode = SketchOutputMode.Map;
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
