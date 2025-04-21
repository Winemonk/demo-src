using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;
using System.Windows.Controls;

namespace WineMonk.Demo.ProAppModule.Code13_LayoutTrayButton
{
    internal class LayoutTrayButtonTest : LayoutTrayButton
    {
        /// <summary>
        /// Invoked after construction, and after all DAML settings have been loaded. 
        /// Use this to perform initialization such as setting ButtonType.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // set the button type
            //  change for different button types
            ButtonType = TrayButtonType.PopupToggleButton;

            // ClickCommand is used for TrayButtonType.Button only
            // ClickCommand = new RelayCommand(DoClick);
        }

        /// <summary>
        /// Override to perform some button initialization.  This is called the first time the botton is loaded.
        /// </summary>
        protected override void OnButtonLoaded()
        {
            base.OnButtonLoaded();
        }


        #region TrayButtonType.Button
        private void DoClick()
        {
            // do something when the tray button is clicked
        }
        #endregion


        #region TrayButtonType.ToggleButton / TrayButtonType.PopupToggleButton
        // 
        // this method fires when ButtonType = TrayButtonType.ToggleButton or PopupToggleButton
        // 

        /// <summary>
        /// Called when the toggle button check state changes
        /// </summary>
        protected override void OnButtonChecked()
        {
            // get the checked state
            var isChecked = this.IsChecked;

            // do something with the checked state
            // refresh the popup VM checked state
            if ((_popupVM != null) && (_popupVM.IsChecked != this.IsChecked))
                _popupVM.IsChecked = this.IsChecked;
        }
        #endregion

        #region TrayButtonType.PopupToggleButton
        // 
        // These methods fire when ButtonType = TrayButtonType.PopupToggleButton
        // 

        private LayoutTrayButtonTestPopupViewModel _popupVM = null;

        /// <summary>
        /// Construct the popup view and return it. 
        /// </summary>
        /// <returns></returns>
        protected override ContentControl ConstructPopupContent()
        {
            // set up the tray button VM
            _popupVM = new LayoutTrayButtonTestPopupViewModel()
            {
                Heading = this.Name,
                IsChecked = this.IsChecked
            };

            // return the UI with the datacontext set
            return new LayoutTrayButtonTestPopupView() { DataContext = _popupVM };
        }

        private bool _subscribed = false;

        /// <summary>
        /// Called when the popup is shown. 
        /// </summary>
        protected override void OnShowPopup()
        {
            base.OnShowPopup();
            // track property changes
            if (!_subscribed)
            {
                _popupVM.PropertyChanged += LayoutTrayButtonTestPopupViewModel_PropertyChanged;
                _subscribed = true;
            }
        }

        /// <summary>
        /// Called when the popup is hidden.
        /// </summary>
        protected override void OnHidePopup()
        {
            // cleanup
            if (_subscribed)
            {
                _popupVM.PropertyChanged -= LayoutTrayButtonTestPopupViewModel_PropertyChanged;
                _subscribed = false;
            }
            base.OnHidePopup();
        }

        private void LayoutTrayButtonTestPopupViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_popupVM == null)
                return;
            // make sure MapTrayButton class has correct checked state when it changes on the VM
            if (e.PropertyName == nameof(LayoutTrayButtonTestPopupViewModel.IsChecked))
            {
                // Since we are changing IsChecked in OnButtonChecked
                //We don't want property notification to trigger (another) callback to OnButtonChecked
                this.SetCheckedNoCallback(_popupVM.IsChecked);
            }
        }

        // Provided to show you how to manually close the popup via code. 
        private void ManuallyClosePopup()
        {
            this.ClosePopup();
        }
        #endregion
    }
}
