using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;


namespace WineMonk.Demo.ProAppModule.Code20_ButtonDockpane
{
    internal class ButtonDockpaneTestViewModel : DockPane
    {
        private const string _dockPaneID = "WineMonk_Demo_ProAppModule_Code20_ButtonDockpane_ButtonDockpaneTest";
        private const string _menuID = "WineMonk_Demo_ProAppModule_Code20_ButtonDockpane_ButtonDockpaneTest_Menu";

        protected ButtonDockpaneTestViewModel() { }

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

        #region Burger Button

        /// <summary>
        /// Tooltip shown when hovering over the burger button.
        /// </summary>
        public string BurgerButtonTooltip
        {
            get { return "Options"; }
        }

        /// <summary>
        /// Menu shown when burger button is clicked.
        /// </summary>
        public System.Windows.Controls.ContextMenu BurgerButtonMenu
        {
            get { return FrameworkApplication.CreateContextMenu(_menuID); }
        }
        #endregion
    }

    /// <summary>
    /// Button implementation to show the DockPane.
    /// </summary>
    internal class ButtonDockpaneTest_ShowButton : Button
    {
        protected override void OnClick()
        {
            ButtonDockpaneTestViewModel.Show();
        }
    }

    /// <summary>
    /// Button implementation for the button on the menu of the burger button.
    /// </summary>
    internal class ButtonDockpaneTest_MenuButton : Button
    {
        protected override void OnClick()
        {
        }
    }
}
