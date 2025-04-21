using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System.Windows;

namespace WineMonk.Demo.ProAppModule.License
{
    internal class Module1 : Module, IExtensionConfig
    {
        private static Module1 _this = null;

        public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("WineMonk_Demo_ProAppModule_License_Module");

        private ExtensionState _state = ExtensionState.Disabled;

        public string Message { get => "未授权"; set { } }
        public string ProductName { get => "WineMonk Demo ProAppModule"; set { } }
        public ExtensionState State
        {
            get => _state;
            set
            {
                MessageBoxResult mbr = MessageBox.Show("是否激活权限？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    _state = ExtensionState.Enabled;
                }
                else
                {
                    _state = ExtensionState.Disabled;
                }
            }
        }

        #region Overrides


        protected override bool CanUnload()
        {
            return true;
        }
        #endregion Overrides

    }
}
