using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
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
        //done
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            object ChangeID = (AuthorsGrid.SelectedItem as DataRowView).Row[0];
            string ForCheck = NameInput.Text + SecondNameInput.Text;
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
                        Authors.UpdateAuthors(NameInput.Text, SecondNameInput.Text, Convert.ToInt32(ChangeID));
                        MessageBox.Show("Данные успешно измеенены!");
                        break;
                    }
                }
                
                NameInput.Clear();
                SecondNameInput.Clear();
                AuthorsGrid.ItemsSource = Authors.GetData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            // создаем стринг для комбинирования текстов обоих вводов для прогона символов через бул запрещенных символов.
            string ForCheck = NameInput.Text + SecondNameInput.Text;
            if (ForCheck.Any(data => string.IsNullOrEmpty(ForCheck)))
            {
                MessageBox.Show("Заполнены не все нужные поля! Заполните оба поля и попытайтесь добавить данные автора еще раз.");
                
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
                        
                        Authors.InsertIntoAuthors(NameInput.Text, SecondNameInput.Text);
                        MessageBox.Show("Данные успешно внесены!");
                        break;
                    }

                }

                //очищаем текстбоксы
                NameInput.Clear();
                SecondNameInput.Clear();
                AuthorsGrid.ItemsSource = Authors.GetData();
            }          
        }
        //done
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            object DeletionID = (AuthorsGrid.SelectedItem as DataRowView).Row[0];
            Authors.DeleteFromAuthorsByID(Convert.ToInt32(DeletionID));
            MessageBox.Show("Данные успешно изменены!");
            AuthorsGrid.ItemsSource = Authors.GetData();
        }
        //done
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            AuthorsGrid.ItemsSource = Authors.GetData();
            NameInput.Clear();
            SecondNameInput.Clear();
            SearchBar.Clear();
        }

        private void AuthorsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorsGrid.SelectedItem != null) 
            {
                string SelectedName = (string)(AuthorsGrid.SelectedItem as DataRowView).Row[1];
                NameInput.Text = SelectedName;
                string SelectedSEcondName = (string)(AuthorsGrid.SelectedItem as DataRowView).Row[2];
                SecondNameInput.Text = SelectedSEcondName;
            }
        }
    }
}
