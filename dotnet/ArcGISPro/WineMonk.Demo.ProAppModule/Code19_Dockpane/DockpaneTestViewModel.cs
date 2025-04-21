using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;


namespace WineMonk.Demo.ProAppModule.Code19_Dockpane
{
    internal class DockpaneTestViewModel : DockPane
    {
        private const string _dockPaneID = "WineMonk_Demo_ProAppModule_Code19_Dockpane_DockpaneTest";

        protected DockpaneTestViewModel() { }

        /// <summary>
        /// Show the DockPane.
        /// </summary>
        internal static void Show()
        {
            DockPane pane = FrameworkApplication.DockPaneManager.Find(_dockPaneID);
            if (pane == null)
                return;

            pane.Activate();
        }

        /// <summary>
        /// Text shown near the top of the DockPane.
        /// </summary>
        private string _heading = "My DockPane";
        public string Heading
        {
            get { return _heading; }
            set
            {
                SetProperty(ref _heading, value, () => Heading);
            }
        }
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class DockpaneTest_ShowButton : Button
    {
        protected override void OnClick()
        {
            DockpaneTestViewModel.Show();
        }
    }
}
