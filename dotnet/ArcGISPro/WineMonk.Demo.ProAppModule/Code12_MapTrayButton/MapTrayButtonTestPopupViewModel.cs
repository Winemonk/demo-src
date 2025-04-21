using ArcGIS.Desktop.Framework.Contracts;

namespace WineMonk.Demo.ProAppModule.Code12_MapTrayButton
{
    internal class MapTrayButtonTestPopupViewModel : PropertyChangedBase
    {
        /// <summary>
        /// Text shown near the top Map Tray UI.
        /// </summary>
        private string _heading = "MapTray";
        public string Heading
        {
            get => _heading;
            set => SetProperty(ref _heading, value);
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
    }
}
