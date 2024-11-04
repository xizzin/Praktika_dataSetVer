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
    /// Логика взаимодействия для VetPage.xaml
    /// </summary>
    public partial class VetPage : Page
    {
        VaccineTypesTableAdapter Vaccines = new VaccineTypesTableAdapter();
        VaccinationJournalTableAdapter VaccJour = new VaccinationJournalTableAdapter();
        AnimalsHaveAllergiesTableAdapter AHA = new AnimalsHaveAllergiesTableAdapter();
        AnimalsTableAdapter Animals = new AnimalsTableAdapter();
        public VetPage()
        {
            InitializeComponent();
            VaccineComboOne.ItemsSource = Vaccines.GetData();
            VaccineComboOne.DisplayMemberPath = "Vaccine_Name";
            VaccineCombotwo.ItemsSource = Vaccines.GetData();
            VaccineCombotwo.DisplayMemberPath = "Vaccine_Name";
            AnimalNameComboOne.ItemsSource = Animals.GetData();
            AnimalNameComboOne.DisplayMemberPath = "Animal_Name";
            AnimalNameComboTwo.ItemsSource = Animals.GetData();
            AnimalNameComboTwo.DisplayMemberPath = "Animal_Name";
            AllergyCombo.ItemsSource = Vaccines.GetData();
            AllergyCombo.DisplayMemberPath = "Vaccine_Name";
            AHAGrid.ItemsSource = AHA.getBeauty();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NoLogin nologinPage = new NoLogin();
            this.Content = nologinPage;
            checks.LoggedWorker = 0;
        }

        private void VaccineComboOne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VaccineComboOne.SelectedItem != null) 
            {
                int Vaccine_ID = VaccineComboOne.SelectedIndex + 1;
                VaccineGrid.ItemsSource = Vaccines.GetBeautyByID(Vaccine_ID);
            }            
        }

        private void AddVaccJournal_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnimal = (AnimalNameComboOne.SelectedItem as DataRowView);
            var selectedvaccine = (VaccineCombotwo.SelectedItem as DataRowView);
            var everything = new List<DataRowView> { selectedAnimal, selectedvaccine };
            if (everything.Any(data => string.IsNullOrEmpty((string)(data.Row[1]))))
            {
                MessageBox.Show("Заполнены не все нужные поля! Заполните поля и попытайтесь еще раз.");
            }
            else
            {
                int Animal_ID = (int)(selectedAnimal.Row[0]);
                int Vaccine_ID = (int)(selectedvaccine).Row[0];
                VaccJour.InsertQuery(checks.LoggedWorker, Animal_ID, Vaccine_ID);
            }
        }

        private void SearchAHA_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalNameComboTwo.SelectedItem != null) 
            {
                var selectedAnimal = (AnimalNameComboTwo.SelectedItem as DataRowView);
                AHAGrid.ItemsSource = AHA.GetDataByID((int)(selectedAnimal.Row[0]));
            }
        }

        private void AddAHA_Click(object sender, RoutedEventArgs e)
        {
            if ((AnimalNameComboTwo.SelectedItem != null) && (AllergyCombo.SelectedItem != null))
            {
                var AllergiesList = AHA.GetData().Rows;
                string animalID = (string)(AnimalNameComboTwo.SelectedItem as DataRowView).Row[0];
                string allergyID = (string)(AllergyCombo.SelectedItem as DataRowView).Row[1];
                for (int  i = 0; i < AllergiesList.Count+1;  i++) 
                {
                    if ((AllergiesList[i][0].ToString() == animalID) && (AllergiesList[i][1].ToString() == allergyID))
                    {
                        MessageBox.Show("Данная запись уже существует!");
                        break;
                    }
                    else
                    {
                        AHA.InsertQuery(Convert.ToInt32(animalID), Convert.ToInt32(allergyID));
                        AHAGrid.ItemsSource = AHA.getBeauty();                       
                    }
                }
            }
        }

        private void ChangeVaccJournal_Click(object sender, RoutedEventArgs e)
        {
            if ((AnimalNameComboOne.SelectedItem != null) && (VaccineCombotwo.SelectedItem != null) && (VaccJournalGrid.SelectedItem != null))
            {
                int VaccID = VaccJournalGrid.SelectedIndex + 1;
                int AnimalID = (int)(AnimalNameComboOne.SelectedItem as DataRowView).Row[0];
                int VaccineID = (int)(VaccineCombotwo.SelectedItem as DataRowView).Row[0];
                VaccJour.UpdateQuery(checks.LoggedWorker, AnimalID, VaccineID, VaccID);
            }
        }

        private void DeleteVaccJournal_Click(object sender, RoutedEventArgs e)
        {
            if (VaccineGrid.SelectedItem != null)
            {
                int JournalID = (int)(VaccineGrid.SelectedItem as DataRowView).Row[0];
                VaccJour.DeleteQuery(JournalID);
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            VaccineComboOne.ItemsSource = Vaccines.GetData();
            VaccineComboOne.DisplayMemberPath = "Vaccine_Name";
            VaccineCombotwo.ItemsSource = Vaccines.GetData();
            VaccineCombotwo.DisplayMemberPath = "Vaccine_Name";
            AnimalNameComboOne.ItemsSource = Animals.GetData();
            AnimalNameComboOne.DisplayMemberPath = "Animal_Name";
            AnimalNameComboTwo.ItemsSource = Animals.GetData();
            AnimalNameComboTwo.DisplayMemberPath = "Animal_Name";
            AllergyCombo.ItemsSource = Vaccines.GetData();
            AllergyCombo.DisplayMemberPath = "Vaccine_Name";
            AHAGrid.ItemsSource = AHA.getBeauty();
        }

        private void SearchVaccJournal_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalNameComboOne.SelectedItem != null)
            {
                int Animal_ID = AnimalNameComboOne.SelectedIndex + 1;
                VaccJour.GetDataByID(Animal_ID);
            }
        }

        private void DeleteAHA_Click(object sender, RoutedEventArgs e)
        {
            if (AHAGrid.SelectedItem != null)
            {
                int Animal_ID = (int)(AHAGrid.SelectedItem as DataRowView).Row[0];
                int Vaccine_ID = (int)(AHAGrid.SelectedItem as DataRowView).Row[1];
                AHA.DeleteQuery(Animal_ID, Vaccine_ID);
            }
        }
    }
}
