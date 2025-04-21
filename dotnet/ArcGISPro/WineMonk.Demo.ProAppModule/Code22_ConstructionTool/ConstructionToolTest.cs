using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Mapping;
using System.Threading.Tasks;


namespace WineMonk.Demo.ProAppModule.Code22_ConstructionTool
{
    internal class ConstructionToolTest : MapTool
    {
        public ConstructionToolTest()
        {
            IsSketchTool = true;
            UseSnapping = true;
            // Select the type of construction tool you wish to implement.  
            // Make sure that the tool is correctly registered with the correct component category type in the daml 
            SketchType = SketchGeometryType.Point;
            // SketchType = SketchGeometryType.Line;
            // SketchType = SketchGeometryType.Polygon;
            //Gets or sets whether the sketch is for creating a feature and should use the CurrentTemplate.
            UsesCurrentTemplate = true;
            //Gets or sets whether the tool supports firing sketch events when the map sketch changes. 
            //Default value is false.
            FireSketchEvents = true;
        }

        /// <summary>
        /// Called when the sketch finishes. This is where we will create the sketch operation and then execute it.
        /// </summary>
        /// <param name="geometry">The geometry created by the sketch.</param>
        /// <returns>A Task returning a Boolean indicating if the sketch complete event was successfully handled.</returns>
        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            if (CurrentTemplate == null || geometry == null)
                return Task.FromResult(false);
            // Create an edit operation
            var createOperation = new EditOperation();
            createOperation.Name = string.Format("Create {0}", CurrentTemplate.Layer.Name);
            createOperation.SelectNewFeatures = true;
            // Queue feature creation
            createOperation.Create(CurrentTemplate, geometry);
            // Execute the operation
            Notification notification = new Notification();
            notification.Title = "绘制";
            notification.Message = $"绘制图层{CurrentTemplate.Layer.Name}点要素...";
            FrameworkApplication.AddNotification(notification);
            return createOperation.ExecuteAsync();
        }
    }
}
