using ArcGIS.Desktop.Framework.Contracts;



namespace WineMonk.Demo.ProAppModule.Code08_CustomControl
{
    internal class CustomControlTestViewModel : CustomControl
    {
        /// <summary>
        /// Text shown in the control.
        /// </summary>
        private string _text = "Custom Control";
        public string Text
        {
            get { return _text; }
            set
            {
                SetProperty(ref _text, value, () => Text);
            }
        }
    }
}
