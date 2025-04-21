using ArcGIS.Core.CIM;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Mapping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.Code18_ImpersonateMapPane
{
    internal class ImpersonateMapPaneTestViewModel : TOCMapPaneProviderPane
    {
        private const string _viewPaneID = "WineMonk_Demo_ProAppModule_Code18_ImpersonateMapPane_ImpersonateMapPaneTest";

        /// <summary>
        /// Consume the passed in CIMView. Call the base constructor to wire up the CIMView.
        /// </summary>
        public ImpersonateMapPaneTestViewModel(CIMView view)
              : base(view)
        {
            //Set to true to change docking behavior
            _dockUnderMapView = false;
        }

        /// <summary>
        /// Create a new instance of the pane.
        /// </summary>
        internal static ImpersonateMapPaneTestViewModel Create(MapView mapView)
        {
            var view = new CIMGenericView();
            view.ViewType = _viewPaneID;
            view.ViewProperties = new Dictionary<string, object>();
            view.ViewProperties["MAPURI"] = mapView.Map.URI;
            var newPane = FrameworkApplication.Panes.Create(_viewPaneID, new object[] { view }) as ImpersonateMapPaneTestViewModel;
            newPane.Caption = $"Impersonate {mapView.Map.Name}";
            return newPane;
        }

        #region Pane Overrides

        /// <summary>
        /// Must be overridden in child classes used to persist the state of the view to the CIM.
        /// </summary>
        /// <remarks>View state is called on each project save</remarks>
        public override CIMView ViewState
        {
            get
            {
                _cimView.InstanceID = (int)InstanceID;
                //Cache content in _cimView.ViewProperties
                //((CIMGenericView)_cimView).ViewProperties["custom"] = "custom value";
                //((CIMGenericView)_cimView).ViewProperties["custom2"] = "custom value2";
                return _cimView;
            }
        }
        /// <summary>
        /// Called when the pane is initialized.
        /// </summary>
        protected async override Task InitializeAsync()
        {
            var uri = ((CIMGenericView)_cimView).ViewProperties["MAPURI"] as string;
            await this.SetMapURI(uri);
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
    internal class ImpersonateMapPaneTest_OpenButton : Button
    {
        protected override void OnClick()
        {
            ImpersonateMapPaneTestViewModel.Create(MapView.Active);
        }
    }
}
