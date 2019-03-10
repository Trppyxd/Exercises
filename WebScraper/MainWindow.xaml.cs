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

namespace WebScraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Scraper _scraper;
        public MainWindow()
        {
            InitializeComponent();
            _scraper = new Scraper();
            DataContext = _scraper;
        }

        private void BtnScraper_OnClick(object sender, RoutedEventArgs e)
        {
            _scraper.ScrapeData(TbPage.Text);
        }

        private void EntriesExport_OnClick(object sender, RoutedEventArgs e)
        {
            _scraper.Export();
        }

        private void EntriesImport_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void TbPage_OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (TbPage.Foreground == Brushes.Silver)
            {
                TbPage.Text = "";
                TbPage.Foreground = Brushes.Black;
            }
        }

        private void TbPage_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (TbPage.Text == "")
            {
                TbPage.Text = "Webpage";
                TbPage.Foreground = Brushes.Silver;
            }
        }
    }
}
