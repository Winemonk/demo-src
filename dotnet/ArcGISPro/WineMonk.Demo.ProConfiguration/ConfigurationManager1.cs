using ArcGIS.Desktop.Framework.Contracts;
using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WineMonk.Demo.ProConfiguration.UI;

namespace WineMonk.Demo.ProConfiguration
{
    internal class ConfigurationManager1 : ConfigurationManager
    {

        /// <summary>
        /// Replaces the default ArcGIS Pro title bar text
        /// 替换默认的ArcGIS Pro标题栏文本
        /// </summary>
        protected override string TitleBarText
        {
            get { return "WineMonk.Demo.ProConfiguration"; }
        }

        /// <summary>
        /// Replaces the ArcGIS Pro Main window icon.
        /// 替换ArcGIS Pro主窗口图标。
        /// </summary>
        protected override ImageSource Icon
        {
            get
            {
                return new BitmapImage(new Uri(@"pack://application:,,,/WineMonk.Demo.ProConfiguration;component/Images/favicon.ico"));
            }
        }

        #region Override Startup Page

        private StartPageViewModel _vm;
        /// <summary>
        /// Called before ArcGIS Pro starts up. Replaces the default Pro start-up page (Optional)
        /// 在ArcGIS Pro启动之前调用。替换默认的Pro启动页面(可选)
        /// </summary>
        /// <returns> 
        /// Implemented UserControl with start-up page functionality. 
        /// Return null if a custom start-up page is not needed. Default ArcGIS Pro start-up page will be displayed.
        /// 实现了具有启动页面功能的UserControl。
        /// 如果不需要自定义启动页，则返回null。将显示默认的ArcGIS Pro启动页面。
        /// </returns>
        protected override System.Windows.FrameworkElement OnShowStartPage()
        {
            if (_vm == null)
            {
                _vm = new StartPageViewModel();
            }
            var page = new StartPage();
            page.DataContext = _vm;
            return page;
        }

        ///<summary>
        ///During the start up this method is called after it is safe to access Portal and use ArcGIS.Desktop.Core. 
        ///ArcGIS Pro Theme has already been set. 
        ///在启动过程中，在安全访问Portal并使用ArcGIS.Desktop.Core之后，这个方法会被调用。
        ///ArcGIS Pro主题已经设置好。
        ///</summary>
        ///<param name="cancelEventArgs">
        ///To cancel initialization, set the cancelEventArgs.Cancel property to true.
        ///要取消初始化，设置cancelEventArgs。取消属性为true。
        ///</param>
        protected override void OnApplicationInitializing(CancelEventArgs cancelEventArgs)
        {

        }

        ///<summary>
        ///During the start up this method is called after the Application Window Start page is ready. From here on calls to ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask are safe.
        ///ArcGIS Pro Extension modules can now be accessed. 
        ///在启动过程中，当应用程序窗口启动页准备就绪后，将调用此方法。从这里开始，对ArcGIS.Desktop.Framework.Threading.Tasks.QueuedTask的调用都是安全的。
        ///现在可以访问ArcGIS Pro扩展模块。
        ///</summary>
        protected override void OnApplicationReady()
        {

        }

        #endregion

        #region Override Splash screen
        /// <summary>
        /// Called while ArcGIS Pro starts up. Replaces the default Pro splash screen. (Optional)
        /// 在ArcGIS Pro启动时调用。替换默认的Pro启动画面。(可选)
        /// </summary>
        /// <returns>
        /// Implemented Window with splash screen functionality. 
        /// Return null if a custom splash screen is not needed. Default ArcGIS Pro splash screen will be displayed.
        /// 实现了带有启动画面功能的窗口。
        /// 如果不需要自定义启动画面，则返回null。将显示默认的ArcGIS Pro启动画面。
        /// </returns>
        protected override System.Windows.Window OnShowSplashScreen()
        {
            return new SplashScreen();
        }
        #endregion

        #region Override About page
        /// <summary>
        /// Customized UserControl is displayed in ArcGIS Pro About property page. Allows to add information about this specific managed configuration.
        /// 自定义UserControl显示在ArcGIS Pro的About属性页中。允许添加有关此特定管理配置的信息。
        /// </summary>
        /// <returns>
        /// Implemented UserControl with about box information. 
        /// Return null if a custom about box is not needed. Default ArcGIS Pro About box will be displayed.
        /// 实现了关于框信息的UserControl。
        /// 如果不需要自定义about框，则返回null。将显示默认的ArcGIS Pro关于框。
        /// </returns>
        protected override System.Windows.FrameworkElement OnShowAboutPage()
        {
            return new AboutPage();
        }
        #endregion
    }
}
