using ArcGIS.Desktop.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WineMonk.Demo.ProAppModule.Code25_ProCustomProjectItem
{
    internal class ProCustomProjectItemTestContainer : CustomProjectItemContainer<ProCustomProjectItemTest>
    {
        //This should be an arbitrary unique string. It must match your <content type="..." 
        //in the Config.daml for the container
        public static readonly string ContainerName = "ProCustomProjectItemTestContainer";
        public ProCustomProjectItemTestContainer() : base(ContainerName)
        {

        }

        /// <summary>
        /// Create item is called whenever a custom item, registered with the container,
        /// is browsed or fetched (eg the user is navigating through different folders viewing
        /// content in the catalog pane).
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="containerType"></param>
        /// <param name="data"></param>
        /// <returns>A custom item created from the input parameters</returns>
        public override Item CreateItem(string name, string path, string containerType, string data)
        {
            var item = ItemFactory.Instance.Create(path);


            if (item is ProCustomProjectItemTestContainer)
            {
                this.Add(item as ProCustomProjectItemTest);

            }
            return item;
        }

        public override ImageSource LargeImage
        {
            get
            {
                var largeImg = new BitmapImage(new Uri(@"pack://application:,,,/WineMonk.Demo.ProAppModule;component/Images/Folder32.png"));
                return largeImg;
            }
        }

        public override Task<System.Windows.Media.ImageSource> SmallImage
        {
            get
            {
                var smallImage = new BitmapImage(new Uri(@"pack://application:,,,/WineMonk.Demo.ProAppModule;component/Images/Folder16.png"));
                if (smallImage == null) throw new ArgumentException("SmallImage for CustomProjectContainer doesn't exist");
                return Task.FromResult(smallImage as ImageSource);
            }
        }

    }
}
