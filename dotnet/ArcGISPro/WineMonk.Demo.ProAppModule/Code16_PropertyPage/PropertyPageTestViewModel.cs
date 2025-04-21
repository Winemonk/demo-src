using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code16_PropertyPage
{
    internal class PropertyPageTestViewModel : Page
    {
        /// <summary>
        /// Invoked when the OK or apply button on the property sheet has been clicked.
        /// </summary>
        /// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
        /// <remarks>This function is only called if the page has set its IsModified flag to true.</remarks>
        protected override Task CommitAsync()
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Called when the page loads because it has become visible.
        /// </summary>
        /// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
        protected override Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Called when the page is destroyed.
        /// </summary>
        protected override void Uninitialize()
        {
        }

        /// <summary>
        /// Text shown inside the page hosted by the property sheet
        /// </summary>
        public string DataUIContent
        {
            get
            {
                return base.Data[0] as string;
            }
            set
            {
                SetProperty(ref base.Data[0], value, () => DataUIContent);
            }
        }
    }

    /// <summary>
    /// Button implementation to show the property sheet.
    /// </summary>
    internal class PropertyPageTest_ShowButton : Button
    {
        protected override void OnClick()
        {
            // collect data to be passed to the page(s) of the property sheet
            Object[] data = new object[] { "Page UI content" };

            if (!PropertySheet.IsVisible)
                PropertySheet.ShowDialog("WineMonk_Demo_ProAppModule_Code16_PropertySheet_PropertySheetTest", "WineMonk_Demo_ProAppModule_Code16_PropertyPage_PropertyPageTest", data);

        }
    }
}
