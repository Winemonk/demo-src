using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Mapping;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code23_TableConstructionTool
{
    internal class TableConstructionToolTest : MapTool
    {
        public TableConstructionToolTest()
        {
            IsSketchTool = false;
            // set the SketchType to None
            SketchType = SketchGeometryType.None;
            //Gets or sets whether the sketch is for creating a feature and should use the CurrentTemplate.
            UsesCurrentTemplate = true;
        }

        /// <summary>
        /// Called when the "Create" button is clicked. This is where we will create the edit operation and then execute it.
        /// </summary>
        /// <param name="geometry">The geometry created by the sketch - will be null because SketchType = SketchGeometryType.None</param>
        /// <returns>A Task returning a Boolean indicating if the sketch complete event was successfully handled.</returns>
        protected override Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            if (CurrentTemplate == null)
                return Task.FromResult(false);
            // geometry will be null
            // Create an edit operation
            var createOperation = new EditOperation();
            createOperation.Name = string.Format("Create {0}", CurrentTemplate.StandaloneTable.Name);
            createOperation.SelectNewFeatures = false;
            // determine the number of rows to add
            var numRows = this.CurrentTemplateRows;
            for (int idx = 0; idx < numRows; idx++)
                // Queue feature creation
                createOperation.Create(CurrentTemplate, null);
            // Execute the operation
            Notification notification = new Notification();
            notification.Title = "添加行";
            notification.Message = $"添加表{CurrentTemplate.MapMember.Name}行...";
            FrameworkApplication.AddNotification(notification);
            return createOperation.ExecuteAsync();
        }
    }
}
