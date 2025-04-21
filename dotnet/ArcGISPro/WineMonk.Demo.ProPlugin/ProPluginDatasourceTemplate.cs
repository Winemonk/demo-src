using ArcGIS.Core.Data;
using ArcGIS.Core.Data.PluginDatastore;
using ArcGIS.Core.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineMonk.Demo.ProPlugin
{
    public class ProPluginDatasourceTemplate : PluginDatasourceTemplate
    {

        public override void Open(Uri connectionPath)
        {
            //TODO Initialize your plugin instance. Individual instances
            //of your plugin may be initialized on different threads
            throw new NotImplementedException();
        }

        public override void Close()
        {
            //TODO Cleanup required to close the plugin 
            //data source instance
            throw new NotImplementedException();
        }

        public override PluginTableTemplate OpenTable(string name)
        {
            //TODO Open the given table/object in the plugin
            //data source
            throw new NotImplementedException();
        }

        public override IReadOnlyList<string> GetTableNames()
        {
            var tableNames = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                tableNames.Add(Guid.NewGuid().ToString());
            }
            //TODO Return the names of all tables in the plugin
            //data source
            return tableNames;
        }

        public override bool IsQueryLanguageSupported()
        {
            //default is false
            return base.IsQueryLanguageSupported();
        }
    }
}
