using ArcGIS.Desktop.Framework.Contracts;
using System.Threading.Tasks;


namespace WineMonk.Demo.ProAppModule.Code14_BackstageTab
{
    internal class BackstageTabTestViewModel : BackstageTab
    {
        /// <summary>
        /// Called when the backstage tab is selected.
        /// </summary>
        protected override Task InitializeAsync()
        {
            return base.InitializeAsync();
        }

        /// <summary>
        /// Called when the backstage tab is unselected.
        /// </summary>
        protected override Task UninitializeAsync()
        {
            return base.UninitializeAsync();
        }

        private string _tabHeading = "Tab Title";
        public string TabHeading
        {
            get
            {
                return _tabHeading;
            }
            set
            {
                SetProperty(ref _tabHeading, value, () => TabHeading);
            }
        }
    }
}
