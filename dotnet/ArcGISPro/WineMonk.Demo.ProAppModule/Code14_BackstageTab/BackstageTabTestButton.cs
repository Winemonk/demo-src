using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;


namespace WineMonk.Demo.ProAppModule.Code14_BackstageTab
{
    internal class BackstageTabTestButton : Button
    {
        protected override void OnClick()
        {
            MessageBox.Show("Sample action using C#.");
        }
    }
}