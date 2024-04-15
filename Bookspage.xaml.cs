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
    /// <summary>
    /// Логика взаимодействия для Bookspage.xaml
    /// </summary>
    public partial class Bookspage : Page
    {
        private void DoAfterLoad(object sender, RoutedEventArgs e)
        {
            BooksGrid.Columns[0].Visibility = Visibility.Collapsed;
            BooksGrid.Columns[2].Visibility = Visibility.Collapsed;
            BooksGrid.Columns[3].Visibility = Visibility.Collapsed;

        }

        BooksTableAdapter Books = new BooksTableAdapter();
        AuthorsTableAdapter Authors = new AuthorsTableAdapter();
        public Bookspage()
        {
            InitializeComponent();
            BooksGrid.ItemsSource = Books.GetEverything();
            Combo.ItemsSource = Authors.GetData();
            Combo.DisplayMemberPath = "Author_Secondname";
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.SearchBooks(SearchBar.Text);
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Combo.SelectedItem != null)
            {

                var Author_ID = (int)(Combo.SelectedItem as DataRowView).Row[0];
                BooksGrid.ItemsSource = Books.SearchBooksByAuthorID(Author_ID);
            }
        }

        private void Очистить_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.GetData();
        }
    }
}
