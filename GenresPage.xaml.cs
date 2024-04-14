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
    /// Логика взаимодействия для GenresPage.xaml
    /// </summary>
    public partial class GenresPage : Page
    {
        GenresTableAdapter Genres = new GenresTableAdapter();
        public GenresPage()
        {
            InitializeComponent();
            GenresGrid.ItemsSource = Genres.GetData();
        }
    }
}
