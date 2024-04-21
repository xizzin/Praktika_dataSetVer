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
        GenresTableAdapter Genres = new GenresTableAdapter();
        public Bookspage()
        {
            InitializeComponent();
            BooksGrid.ItemsSource = Books.GetEverything();

            GenreID_Combo.ItemsSource = Genres.GetData();
            GenreID_Combo.DisplayMemberPath = "Genre_name";
            GenreID_ComboMain.ItemsSource = Genres.GetData();
            GenreID_ComboMain.DisplayMemberPath = "Genre_name";

            AuthorID_Combo.ItemsSource = Authors.GetData();
            AuthorID_Combo.DisplayMemberPath = "Author_Secondname";
            AuthorID_ComboMain.ItemsSource = Authors.GetData();
            AuthorID_ComboMain.DisplayMemberPath = "Author_Secondname";
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.SearchBooks(SearchBar.Text);
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.GetEverything();
            SearchBar.Clear();
            BookInput.Clear();
            GenreID_Combo.SelectedItem = null;
            GenreID_Combo.SelectedItem = null;
            AuthorID_Combo.SelectedItem = null;
            AuthorID_ComboMain.SelectedItem = null;
        }

        //done
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ForCheck = BookInput.Text;
            if (ForCheck.Any(data => string.IsNullOrEmpty(ForCheck)) && !(GenreID_Combo.SelectedItem != null) && !(AuthorID_Combo.SelectedItem != null))
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
                        int Author_ID = (int)(AuthorID_Combo.SelectedItem as DataRowView).Row[0];
                        int Genre_ID = (int)(GenreID_Combo.SelectedItem as DataRowView).Row[0];
                        Books.InsertIntoBooks(BookInput.Text, Author_ID, Genre_ID);
                        MessageBox.Show("Данные успешно сохранены!");
                        break;
                    }
                }

                BookInput.Clear();
                BooksGrid.ItemsSource = Books.GetEverything();
            }
        }
        
        //done
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            int ChangeID = (int)(BooksGrid.SelectedItem as DataRowView).Row[0];
            string ForCheck = BookInput.Text;
            if (ForCheck.Any(data => string.IsNullOrEmpty(ForCheck)) && !(GenreID_Combo.SelectedItem != null) && !(AuthorID_Combo.SelectedItem != null))
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
                        int Author_ID = (int)(AuthorID_Combo.SelectedItem as DataRowView).Row[0];
                        int Genre_ID = (int)(GenreID_Combo.SelectedItem as DataRowView).Row[0];
                        Books.UpdateBooksByID(BookInput.Text, Author_ID, Genre_ID, ChangeID);
                        MessageBox.Show("Данные успешно изменены!");
                        break;
                    }
                }

                BookInput.Clear();
                BooksGrid.ItemsSource = Books.GetEverything();
            }
        }

        //done
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            object DeletionID = (BooksGrid.SelectedItem as DataRowView).Row[0];
            Books.DeleteFromBooksByID(Convert.ToInt32(DeletionID));
            MessageBox.Show("Данные успешно удалены!");
            BooksGrid.ItemsSource = Books.GetEverything();
        }

        private void BooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BooksGrid.SelectedItem != null)
            {
                string SelectedName = (string)(BooksGrid.SelectedItem as DataRowView).Row[1];
                BookInput.Text = SelectedName;
                int SelectedAuthorID = (int)(BooksGrid.SelectedItem as DataRowView).Row[2]-1;
                AuthorID_Combo.SelectedIndex = SelectedAuthorID;
                int SelectedGenreID = (int)(BooksGrid.SelectedItem as DataRowView).Row[3]-1;
                GenreID_Combo.SelectedIndex = SelectedGenreID;
            }
            else { }

        }

        private void GenreID_ComboMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenreID_ComboMain.SelectedItem != null)
            {
                int Genreid = (int)(GenreID_ComboMain.SelectedItem as DataRowView).Row[0];
                BooksGrid.ItemsSource = Books.SearchBooksByGenreID(Genreid);
            }
               
        }

        private void AuthorID_ComboMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorID_ComboMain.SelectedItem != null) 
            {
                int AuthorID = (int)(AuthorID_ComboMain.SelectedItem as DataRowView).Row[0];
                BooksGrid.ItemsSource = Books.SearchBooksByAuthorID(AuthorID);
                AuthorID_ComboMain.SelectedItem = null;
            }
        }
    }
}
