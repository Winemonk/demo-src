using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WineMonk.Demo.ProAppModule.Code21_DropHandler.Dockpanes
{
    /// <summary>
    /// Interaction logic for TestDropHandlerDockpaneView.xaml
    /// </summary>
    public partial class TestDropHandlerDockpaneView : UserControl
    {
        public TestDropHandlerDockpaneView()
        {
            InitializeComponent();
        }
        private Point _startPoint;
        private void TreeView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            _startPoint = e.GetPosition(null);
        }
        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = _startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged TreeViewItem
                TreeView treeView = sender as TreeView;
                TreeViewItem treeViewItem = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);
                if (treeViewItem == null || !treeViewItem.AllowDrop)
                {
                    return;
                }
                // Initialize the drag-and-drop operation
                DataObject dragData = new DataObject();
                dragData.SetData("dataName", treeViewItem.Header);
                dragData.SetData("dataPath", treeViewItem.Tag ?? string.Empty);
                DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
    }
}
