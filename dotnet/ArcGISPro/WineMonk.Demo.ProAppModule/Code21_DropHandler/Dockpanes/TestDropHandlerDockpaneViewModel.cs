using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;


namespace WineMonk.Demo.ProAppModule.Code21_DropHandler.Dockpanes
{
    internal class TestDropHandlerDockpaneViewModel : DockPane
    {
        private const string _dockPaneID = "WineMonk_Demo_ProAppModule_Code21_DropHandler_Dockpanes_TestDropHandlerDockpane";

        protected TestDropHandlerDockpaneViewModel() { }

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
    internal class TestDropHandlerDockpane_ShowButton : Button
    {
        protected override void OnClick()
        {
            TestDropHandlerDockpaneViewModel.Show();
        }
    }
}
