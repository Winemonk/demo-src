using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;

namespace WineMonk.Demo.ProAppModule.Code01_MenuButton
{
    internal class ButtonTest : Button
    {
        protected override void OnClick()
        {
            // 点击事件...
            MessageBox.Show($"点击了按钮 - ID:{this.ID} Caption:{this.Caption}");
        }
    }
}
