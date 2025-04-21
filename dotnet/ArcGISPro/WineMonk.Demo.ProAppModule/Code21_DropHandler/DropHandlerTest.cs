using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.DragDrop;
using System;
using System.Windows;


namespace WineMonk.Demo.ProAppModule.Code21_DropHandler
{
    internal class DropHandlerTest : DropHandlerBase
    {
        public override void OnDragOver(DropInfo dropInfo)
        {
            //default is to accept our data types
            dropInfo.Effects = DragDropEffects.All;
        }

        public override void OnDrop(DropInfo dropInfo)
        {
            //eg, if you are accessing a dropped file
            //string filePath = dropInfo.Items[0].Data.ToString();
            if (dropInfo.Items == null || dropInfo.Items.Count < 1)
            {
                return;
            }
            DropDataItem dropDataItem = dropInfo.Items[0];
            object data = dropDataItem.Data;
            if (data == null)
            {
                return;
            }
            if (data is DataObject dataObject)
            {
                string[] formats = dataObject.GetFormats();
                if (formats == null || formats.Length < 1)
                {
                    return;
                }
                string msg = string.Empty;
                foreach (string f in formats)
                {
                    object subData = dataObject.GetData(f);
                    msg += $"{Environment.NewLine}{f}: {subData}";
                }
                ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show($"接收到数据：{msg}");
                dropInfo.Handled = true;
            }
            //set to true if you handled the drop
            //dropInfo.Handled = true;
        }
    }
}
