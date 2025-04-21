using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.CustomSettings
{
    public class CustomSettingsViewModel : Page
    {
        private bool _setting1;
        public bool Setting1
        {
            get { return _setting1; }
            set
            {
                SetProperty(ref _setting1, value, () => Setting1);
                base.IsModified = true;
            }
        }
        private string _setting2;
        public string Setting2
        {
            get { return _setting2; }
            set
            {
                SetProperty(ref _setting2, value, () => Setting2);
                base.IsModified = true;
            }
        }

        private bool _origSetting1;
        private string _origSetting2;

        protected override Task InitializeAsync()
        {
            Setting1 = Module1.Current.Setting1;
            Setting2 = Module1.Current.Setting2;
            _origSetting1 = Setting1;
            _origSetting2 = Setting2;
            return Task.FromResult(0);
        }

        private bool IsDirty()
        {
            if (_origSetting1 != Setting1)
                return true;
            if (_origSetting2 != Setting2)
                return true;
            return false;
        }

        protected override Task CommitAsync()
        {
            if (IsDirty())
            {
                Module1.Current.Setting1 = Setting1;
                Module1.Current.Setting2 = Setting2;
                Project.Current.SetDirty(true);
            }
            return Task.FromResult(0);
        }
    }
}
