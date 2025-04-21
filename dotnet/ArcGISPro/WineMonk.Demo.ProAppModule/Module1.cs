using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace WineMonk.Demo.ProAppModule
{
    internal class Module1 : Module
    {
        private static Module1 _this = null;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// 在这里检索这个模块的单例实例
        /// </summary>
        public static Module1 Current => _this ??= (Module1)FrameworkApplication.FindModule("WineMonk.Demo.ProAppModule_Module");

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// 在ArcGIS Pro关闭时被框架调用
        /// </summary>
        /// <returns>
        /// False to prevent Pro from closing, otherwise True
        /// False防止Pro关闭，否则为True
        /// </returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //TODO - 添加业务逻辑
            //return false to ~cancel~ Application close
            //返回false取消应用程序关闭
            return true;
        }

        #endregion Overrides

    }
}
