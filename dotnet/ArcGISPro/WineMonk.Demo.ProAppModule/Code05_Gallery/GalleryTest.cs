using ArcGIS.Desktop.Framework.Contracts;
using System.Windows.Media;

namespace WineMonk.Demo.ProAppModule.Code05_Gallery
{
    internal class GalleryTest : Gallery
    {
        private bool _isInitialized;

        protected override void OnDropDownOpened()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;

            //Add 6 items to the gallery
            for (int i = 0; i < 6; i++)
            {
                string name = string.Format("Item {0}", i);
                Add(new GalleryItem(name, this.LargeImage != null ? ((ImageSource)this.LargeImage).Clone() : null, name));
            }
            _isInitialized = true;

        }

        protected override void OnClick(GalleryItem item)
        {
            //TODO - insert your code to manipulate the clicked gallery item here
            System.Diagnostics.Debug.WriteLine("Remove this line after adding your custom behavior.");
            base.OnClick(item);
        }
    }
}
