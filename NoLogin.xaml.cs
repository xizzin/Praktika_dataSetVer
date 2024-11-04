using Microsoft.SqlServer.Server;
using Praktika_5DataSetVer.AnimalShelterDataSetTableAdapters;
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

namespace Praktika_5DataSetVer
{
    /// <summary>
    /// Логика взаимодействия для NoLogin.xaml
    /// </summary>
    public partial class NoLogin : Page
    {
        public NoLogin()
        {
            InitializeComponent();
            PHAChoice.ItemsSource = Allergies.GetData();
            PHAChoice.DisplayMemberPath = "Allergy_Name";
            AnimalsGrid.ItemsSource = Animals.SelectBeauty();
        }
        PeopleTableAdapter People = new PeopleTableAdapter();
        AllergiesTableAdapter Allergies = new AllergiesTableAdapter();
        PeopleHaveAllergiesTableAdapter PHA = new PeopleHaveAllergiesTableAdapter();
        AnimalsHaveAllergiesTableAdapter AHA = new AnimalsHaveAllergiesTableAdapter();
        AnimalsTableAdapter Animals = new AnimalsTableAdapter();
        PeopleRequestTableAdapter PeopleRequest = new PeopleRequestTableAdapter();
        AnimalSubtypesTableAdapter Subtypes = new AnimalSubtypesTableAdapter();
        VaccinationJournalTableAdapter VAccJour = new VaccinationJournalTableAdapter();
        private void AnketaDone_Click(object sender, RoutedEventArgs e)
        {
            var inputsOne = new List<TextBox>
            {
                PeopleNameInput, PeopleSecondNameInput, PeoplePronouns, PeopleAge, PeopleSpace, PeopleTime, PeopleWhere, PeopleFamily, PeopleAgreement
            };

            if (inputsOne.Any(data => string.IsNullOrEmpty(data.Text)))
            {
                MessageBox.Show("Заполнены не все нужные поля! Заполните поля и попытайтесь еще раз.");
            }
            else
            {
                var StringBoxxesList = new List<TextBox> { PeopleNameInput, PeopleSecondNameInput, PeoplePronouns, PeopleThirdNameInput, PeopleWhere, PeopleFamily, PeopleAgreement };
                var StringBoxes = PeopleNameInput.Text + PeopleSecondNameInput.Text + PeoplePronouns.Text + PeopleThirdNameInput.Text + PeopleWhere.Text + PeopleFamily.Text + PeopleAgreement.Text;
                var IntBoxesList = new List<TextBox> { PeopleAge, PeopleSpace, PeopleTime };
                var IntBoxes = PeopleAge.Text + PeopleSpace.Text + PeopleTime.Text;
                foreach (char c in StringBoxes)
                {
                    checks.isNotPermittedInString(c);
                    if (checks.isNotPermittedInString(c) == true)
                    {
                        MessageBox.Show("Введены символы, которые не принимают следующие столбцы: имя, фамилия, отчество, местоимения, город/поселок, дети/без детей, согласие проживающих. Измените их и попытайтесь еще раз.");
                        break;
                    }
                    else
                    {
                        foreach (char k in IntBoxes)
                        {
                            if (!Char.IsDigit(c))
                            {
                                MessageBox.Show("Пожалуйста, введите в столбцы возраста, свободного пространства и времени только числа");
                                break;
                            }
                            else
                            {
                                if ((StringBoxxesList.Any(data => (data.Text.Length) > 100)) && (IntBoxesList.Any(data => (data.Text.Length > 300) && (PeopleExp.Text.Length > 5000))))
                                {
                                    MessageBox.Show("В каком-то из столбцов превышено количество символов. Измените данные и попытайтесь еще раз.");
                                }
                                else
                                {
                                    People.InsertQuery(PeopleNameInput.Text, PeopleSecondNameInput.Text, PeopleThirdNameInput.Text, PeoplePronouns.Text, Convert.ToInt32(PeopleAge.Text), Convert.ToInt32(PeopleSpace.Text), Convert.ToInt32(PeopleTime.Text), PeopleWhere.Text, PeopleFamily.Text, PeopleAgreement.Text, PeopleExp.Text);
                                    MessageBox.Show("Данные успешно сохранены!");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PHAAdd_Click(object sender, RoutedEventArgs e)
        {
            Ghost.ItemsSource = People.GetPeopleID(PeopleNameInput.Text, PeopleSecondNameInput.Text);
            Ghost.DisplayMemberPath = "ID_People";
            Ghost.SelectedIndex = 0;

            if ((PHAChoice.SelectedItem != null) && (Ghost.SelectedItem != null))
            {
                int People_ID = (int)(Ghost.SelectedItem as DataRowView).Row[0];
                int Allergy_ID = (int)(PHAChoice.SelectedItem as DataRowView).Row[0];
                PHA.InsertQuery(People_ID, Allergy_ID);
                MessageBox.Show("Данные успешно внесены!");
            }
            else 
            {
                MessageBox.Show("Анкета не была заполнена/данные введенной и отправленной анкеты были изменены/не была выбрана аллергия для добавления. Введите все данные заново и попытайтесь еще раз");
            }
        }

        private void AnimalsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AnimalsGrid.SelectedItem != null) 
            {
                int Animal_ID = (int)(AnimalsGrid.SelectedItem as DataRowView).Row[0];
                //search by id on gtype, subtype, worker
                AnimalTypeOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[1];
                AnimalSubypeOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[2];
                AnimalNameOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[3];
                HandlerCommentOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[4];
                AnimalWorkerOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[5];
                ElseOutput.Text = (string)(AnimalsGrid.SelectedItem as DataRowView).Row[6];
                VaccJourGrid.ItemsSource = VAccJour.GetDataByID(Animal_ID);
                AHAGrid.ItemsSource = AHA.GetDataByID(Animal_ID);
            }
        }

        private void CompleteRequest_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalChoiceCombo.SelectedItem != null)
            {
                Ghost.ItemsSource = People.GetPeopleID(PeopleNameInput.Text, PeopleSecondNameInput.Text);
                Ghost.DisplayMemberPath = "ID_People";
                Ghost.SelectedIndex = 0;
                if (Ghost.SelectedItem != null)
                {
                    int People_ID = (int)(Ghost.SelectedItem as DataRowView).Row[0];
                    int Animal_ID = (int)(AnimalChoiceCombo.SelectedItem as DataRowView).Row[0];
                    PeopleRequest.InsertQuery(People_ID, Animal_ID, 2);
                }
                else
                {
                    MessageBox.Show("Анкета не была заполнена/данные введенной и отправленной анкеты были изменены. Введите все данные заново и попытайтесь еще раз");
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage();
            this.Content = loginPage;
        }
    }
}
