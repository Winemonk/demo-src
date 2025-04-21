using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code11_LayoutTool
{
    internal class LayoutToolTest : LayoutTool
    {
        public LayoutToolTest()
        {
            SketchType = SketchGeometryType.Rectangle;
        }
        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }
        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            //TODO: Use geometry. Add graphic, select elements, etc.
            //QueuedTask.Run( () => {
            //  ActiveElementContainer.SelectElements(geometry, SelectionCombinationMethod.New, false);
            //});
            return base.OnSketchCompleteAsync(geometry);
        }
    }
}
