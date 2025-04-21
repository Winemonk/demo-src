using ArcGIS.Desktop.Core.Events;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using System;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProAppModule.CustomSettings
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;

        public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("WineMonk_Demo_ProAppModule_CustomSettings_Module");

        protected Module1()
        {
            ProjectOpenedEvent.Subscribe(OnProjectOpen);
            ProjectClosedEvent.Subscribe(OnProjectClosed);
        }

        private void OnActivePaneInitialized(ActivePaneInitializedEventArgs args)
        {
            
        }

        private void OnProjectClosed(ProjectEventArgs args)
        {
            _hasSettings = false;
        }

        private void OnProjectOpen(ProjectEventArgs args)
        {
            if (!_hasSettings)
            {
                Setting1 = false;
                Setting2 = "Default Value";
            }
        }

        public bool Setting1 { get; set; } = true;
        public string Setting2 { get; set; } = string.Empty;
        private bool _hasSettings = false;
        #region Overrides
        protected override Task OnReadSettingsAsync(ModuleSettingsReader settings)
        {
            if (settings == null) return Task.FromResult(0);
            _hasSettings = true;
            object value = settings.Get(nameof(Setting1));
            if (value != null)
            {
                Setting1 = Convert.ToBoolean(value);
            }
            value = settings.Get(nameof(Setting2));
            if (value != null)
            {
                Setting2 = value.ToString();
            }
            return Task.FromResult(0);
        }

        protected override Task OnWriteSettingsAsync(ModuleSettingsWriter settings)
        {
            settings.Add(nameof(Setting1), Setting1.ToString());
            settings.Add(nameof(Setting2), Setting2);
            return Task.FromResult(0);
        }
        protected override bool CanUnload()
        {
            return true;
        }

        #endregion Overrides
    }
}
