﻿using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppZBC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuItemZHCN_Click(object sender, RoutedEventArgs e)
        {
            LanguageManager.Instance.ChangeLanguage(new CultureInfo("zh-CN"));
        }

        private void menuItemZHHANT_Click(object sender, RoutedEventArgs e)
        {
            LanguageManager.Instance.ChangeLanguage(new CultureInfo("zh-Hant"));
        }

        private void menuItemEN_Click(object sender, RoutedEventArgs e)
        {
            LanguageManager.Instance.ChangeLanguage(new CultureInfo("en"));
        }
    }
}