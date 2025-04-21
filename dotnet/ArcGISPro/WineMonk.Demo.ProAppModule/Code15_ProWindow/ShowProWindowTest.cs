using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace WineMonk.Demo.ProAppModule.Code15_ProWindow
{
    internal class ShowProWindowTest : Button
    {

        private ProWindowTest _prowindowtest = null;

        protected override void OnClick()
        {
            //already open?
            if (_prowindowtest != null)
                return;
            _prowindowtest = new ProWindowTest();
            _prowindowtest.Owner = FrameworkApplication.Current.MainWindow;
            _prowindowtest.Closed += (o, e) => { _prowindowtest = null; };
            _prowindowtest.Show();
            //uncomment for modal
            //_prowindowtest.ShowDialog();
        }

    }
}
