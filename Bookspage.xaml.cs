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

            AuthorID_Combo.ItemsSource = Authors.GetData();
            AuthorID_Combo.DisplayMemberPath = "Author_Secondname";
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.SearchBooks(SearchBar.Text);
        }

        //done
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            BooksGrid.ItemsSource = Books.GetData();
        }

        //done
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            object ChangeID = (BooksGrid.SelectedItem as DataRowView).Row[0];
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
                        var Author_ID = (AuthorID_Combo.SelectedItem as DataRowView).Row[0];
                        var Genre_ID = (GenreID_Combo.SelectedItem as DataRowView).Row[0];
                        Books.InsertIntoBooks(BookInput.Text, Convert.ToInt32(Genre_ID), Convert.ToInt32(Genre_ID));
                        MessageBox.Show("Данные успешно сохранены!");
                        break;
                    }
                }

                BookInput.Clear();
                BooksGrid.ItemsSource = Books.GetData();
            }
        }
        
        //done
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            object ChangeID = (BooksGrid.SelectedItem as DataRowView).Row[0];
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
                        var Author_ID = (AuthorID_Combo.SelectedItem as DataRowView).Row[0];
                        var Genre_ID = (GenreID_Combo.SelectedItem as DataRowView).Row[0];
                        Books.UpdateBooksByID(BookInput.Text, Convert.ToInt32(Author_ID), Convert.ToInt32(Genre_ID), Convert.ToInt32(Genre_ID));
                        MessageBox.Show("Данные успешно изменены!");
                        break;
                    }
                }

                BookInput.Clear();
                BooksGrid.ItemsSource = Books.GetData();
            }
        }

        //done
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            object DeletionID = (BooksGrid.SelectedItem as DataRowView).Row[0];
            Books.DeleteFromBooksByID(Convert.ToInt32(DeletionID));
            MessageBox.Show("Данные успешно удалены!");
            BooksGrid.ItemsSource = Books.GetData();
        }
    }
}
