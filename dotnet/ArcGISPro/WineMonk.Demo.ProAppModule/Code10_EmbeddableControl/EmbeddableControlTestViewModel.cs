using ArcGIS.Desktop.Framework.Controls;
using System.Xml.Linq;


namespace WineMonk.Demo.ProAppModule.Code10_EmbeddableControl
{
    internal class EmbeddableControlTestViewModel : EmbeddableControl
    {
        public EmbeddableControlTestViewModel(XElement options, bool canChangeOptions) : base(options, canChangeOptions) { }

        /// <summary>
        /// Text shown in the control.
        /// </summary>
        private string _text = "Embeddable Control";
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
