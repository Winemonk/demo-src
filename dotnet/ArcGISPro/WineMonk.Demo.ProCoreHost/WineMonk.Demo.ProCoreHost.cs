using ArcGIS.Core.Hosting;
using System;

namespace WineMonk.Demo.ProCoreHost
{
    internal class Program
    {
        //[STAThread] must be present on the Application entry point
        //[STAThread] 必须出现在应用程序入口点上
        [STAThread]
        static void Main(string[] args)
        {
            //Call Host.Initialize before constructing any objects from ArcGIS.Core
            //在访问ArcGIS中的任何类型之前调用初始化
            Host.Initialize();
            //TODO: Add your business logic here.   
            //TODO: 在这里添加您的业务逻辑。
        }
    }
}
