using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code17_Pane
{
    internal class PaneTestViewModel : ViewStatePane
    {
        private const string _viewPaneID = "WineMonk_Demo_ProAppModule_Code17_Pane_PaneTest";

        /// <summary>
        /// Consume the passed in CIMView. Call the base constructor to wire up the CIMView.
        /// </summary>
        public PaneTestViewModel(CIMView view)
          : base(view) { }

        /// <summary>
        /// Create a new instance of the pane.
        /// </summary>
        internal static PaneTestViewModel Create()
        {
            var view = new CIMGenericView();
            view.ViewType = _viewPaneID;
            return FrameworkApplication.Panes.Create(_viewPaneID, new object[] { view }) as PaneTestViewModel;
        }

        #region Pane Overrides

        /// <summary>
        /// Must be overridden in child classes used to persist the state of the view to the CIM.
        /// </summary>
        public override CIMView ViewState
        {
            get
            {
                _cimView.InstanceID = (int)InstanceID;
                return _cimView;
            }
        }

        /// <summary>
        /// Called when the pane is initialized.
        /// </summary>
        protected async override Task InitializeAsync()
        {
            await base.InitializeAsync();
        }

        /// <summary>
        /// Called when the pane is uninitialized.
        /// </summary>
        protected async override Task UninitializeAsync()
        {
            await base.UninitializeAsync();
        }

        #endregion Pane Overrides
    }

    /// <summary>
    /// Button implementation to create a new instance of the pane and activate it.
    /// </summary>
    internal class PaneTest_OpenButton : Button
    {
        protected override void OnClick()
        {
            PaneTestViewModel.Create();
        }
    }
}
