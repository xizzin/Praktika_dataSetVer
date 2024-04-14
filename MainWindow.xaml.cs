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
//
using prOneDataSetVer.booksDataSetTableAdapters;
//
namespace prOneDataSetVer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorsPage authorsPage = new AuthorsPage();
            AuthorsFrame.Content = authorsPage;
        }

        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            GenresPage genresPage = new GenresPage();
            GenresFrame.Content = genresPage;
        }

        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            Bookspage booksPage = new Bookspage();  
            BooksFrame.Content = booksPage;
        }

    }
}
