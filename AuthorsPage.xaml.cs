using System;
using System.Collections.Generic;
using System.Data;
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
//
using prOneDataSetVer.booksDataSetTableAdapters;
//
namespace prOneDataSetVer
{

    public partial class AuthorsPage : Page
    {
        AuthorsTableAdapter Authors = new AuthorsTableAdapter();

        public AuthorsPage()
        {
            InitializeComponent();
            AuthorsGrid.ItemsSource = Authors.GetData();
        }

        private void DoAfterLoad(object sender, RoutedEventArgs e)
        {
            AuthorsGrid.Columns[0].Visibility = Visibility.Collapsed;

        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorsGrid.ItemsSource = Authors.SearchAuthors(SearchBar.Text);
            AuthorsGrid.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
