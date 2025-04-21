using ArcGIS.Desktop.Framework.Contracts;
using System.Windows.Media;

namespace WineMonk.Demo.ProAppModule.Code06_InlineGallery
{
    internal class InlineGalleryTest : Gallery
    {
        private bool _isInitialized;

        public InlineGalleryTest()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;

            //Add 6 items to the gallery
            for (int i = 0; i < 6; i++)
            {
                string name = string.Format("Item {0}", i);
                Add(new GalleryItem(name, this.LargeImage != null ? ((ImageSource)this.LargeImage).Clone() : null, name));
            }

        }

        protected override void OnClick(GalleryItem item)
        {
            //TODO - insert your code to manipulate the clicked gallery item here
            System.Diagnostics.Debug.WriteLine("Remove this line after adding your custom behavior.");
            base.OnClick(item);
        }
    }
}
