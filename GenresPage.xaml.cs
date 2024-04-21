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

        private void DoAfterLoad(object sender, RoutedEventArgs e)
        {
            GenresGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            GenresGrid.ItemsSource = Genres.SearchGenres(SearchBar.Text);
        }

        //done
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            GenresGrid.ItemsSource = Genres.GetData();
            GenreInput.Clear();
            SearchBar.Clear();
        }

        //done
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            object ChangeID = (GenresGrid.SelectedItem as DataRowView).Row[0];
            string ForCheck = GenreInput.Text;
            if (ForCheck.Any(data => string.IsNullOrEmpty(ForCheck)))
            {
                MessageBox.Show("Заполнены не все нужные поля! Заполните оба поля и попытайтесь изменить данные автора еще раз.");
            }
            else
            {
                foreach (char c in ForCheck)
                {
                    argh.isNotPermittedIn(c);
                    if (argh.isNotPermittedIn(c) == true)
                    {
                        MessageBox.Show("Введены символы, не поддерживаемые данным столбцом таблицы");
                        break;
                    }
                    else
                    {
                        Genres.UpdateGenres(GenreInput.Text, Convert.ToInt32(ChangeID));
                        MessageBox.Show("Данные успешно измеенены!");
                        break;
                    }
                }

                GenreInput.Clear();
                GenresGrid.ItemsSource = Genres.GetData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ForCheck = GenreInput.Text;
            if (ForCheck.Any(data => string.IsNullOrEmpty(ForCheck)))
            {
                MessageBox.Show("Заполнены не все нужные поля! Заполните оба поля и попытайтесь изменить данные автора еще раз.");
            }
            else
            {
                foreach (char c in ForCheck)
                {
                    argh.isNotPermittedIn(c);
                    if (argh.isNotPermittedIn(c) == true)
                    {
                        MessageBox.Show("Введены символы, не поддерживаемые данным столбцом таблицы");
                        break;
                    }
                    else
                    {
                        Genres.InsertIntoGenres(GenreInput.Text);
                        MessageBox.Show("Данные успешно внесены!");
                        break;
                    }
                }

                GenreInput.Clear();
                GenresGrid.ItemsSource = Genres.GetData();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            object DeletionID = (GenresGrid.SelectedItem as DataRowView).Row[0];
            Genres.DeleteFromGenresByID(Convert.ToInt32(DeletionID));
            MessageBox.Show("Данные успешно удалены!");
        }

        private void GenresGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenresGrid.SelectedItem != null) 
            {
                string Selectedgenre = (string)(GenresGrid.SelectedItem as DataRowView).Row[1];
                GenreInput.Text = Selectedgenre;
            }
            else
            {

            }
        }
    }
}
