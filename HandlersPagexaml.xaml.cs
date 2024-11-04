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
    /// Логика взаимодействия для HandlersPagexaml.xaml
    /// </summary>
    public partial class HandlersPagexaml : Page
    {
        PeopleHaveAllergiesTableAdapter PHA = new PeopleHaveAllergiesTableAdapter();
        PeopleTableAdapter People = new PeopleTableAdapter();
        PeopleRequestTableAdapter peopleRequests = new PeopleRequestTableAdapter();
        RequestStatusTableAdapter Status = new RequestStatusTableAdapter();
        AnimalsHaveAllergiesTableAdapter AHA = new AnimalsHaveAllergiesTableAdapter();
        AnimalsSpecialDifferenciesTableAdapter ASD = new AnimalsSpecialDifferenciesTableAdapter();
        AnimalsSpecialNeedsTableAdapter ASN = new AnimalsSpecialNeedsTableAdapter();
        AnimalsTableAdapter Animals = new AnimalsTableAdapter();
        AnimalTypesTableAdapter AnimalTypes = new AnimalTypesTableAdapter();
        AnimalSubtypesTableAdapter AnimalSubtypes = new AnimalSubtypesTableAdapter();
        SpecialNeedsTableAdapter SpecialNeeds = new SpecialNeedsTableAdapter();
        SpecialDifferenciesTableAdapter SpecialDifferencies = new SpecialDifferenciesTableAdapter();
        
        public HandlersPagexaml()
        {
            InitializeComponent();
            PeopleRequestsGrid.ItemsSource = peopleRequests.GetData();
            PHAGrid.ItemsSource = PHA.GetBeauty();

            SpecialNeedsGrid.ItemsSource = SpecialNeeds.GetData();
            DifferenciesGrid.ItemsSource = SpecialDifferencies.GetData();

            StatusBox.ItemsSource = Status.GetData();
            StatusBox.DisplayMemberPath = "RequestStatus_Name";

            AHAGrid.ItemsSource = AHA.getBeauty();
            AnimalDiffenreciesGrid.ItemsSource = ASN.GetBeauty();

            AnimalBox.ItemsSource=Animals.GetData();
            AnimalBox.DisplayMemberPath = "Animal_Name";

            AnimalNameInput.ItemsSource = Animals.GetData();
            AnimalNameInput.DisplayMemberPath = "Animal_Name";

            AnimalTypeInput.ItemsSource= AnimalTypes.GetData();
            AnimalTypeInput.DisplayMemberPath = "AnimalType_Name";

            AnimalSubtypeInput.ItemsSource = AnimalSubtypes.GetData();
            AnimalTypeInput.DisplayMemberPath = "AnimalSubtype_Name";

            NeedsChoiceCombo.ItemsSource = SpecialNeeds.GetData();
            NeedsChoiceCombo.DisplayMemberPath = "SpecialNeeds_Name";

            AnimalNeedsCombo.ItemsSource = Animals.GetData();
            AnimalNeedsCombo.DisplayMemberPath = "Animal_Name";

            DiffChoiceCombo.ItemsSource = SpecialDifferencies.GetData();
            DiffChoiceCombo.DisplayMemberPath = "SpecialNeeds_Name";

            AnimalNameDiffCombo.ItemsSource = Animals.GetData();
            AnimalNameInput.DisplayMemberPath = "Animal_Name";

            AnimalsGrid.ItemsSource = Animals.GetData();
        }

        private void PeopleRequestsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PHAGrid.SelectedItem != null)
            {
                int PeopleID = (int)(PeopleRequestsGrid.SelectedItem as DataRowView).Row[0];
                var PeopleInfo = People.GetDataByID(PeopleID).Rows;
                PeopleNameInput.Text = PeopleInfo[1][0].ToString();
                PeoplePronouns.Text = PeopleInfo[1][1].ToString();
                PeopleAge.Text = PeopleInfo[1][2].ToString();
                PeopleSpace.Text = PeopleInfo[1][3].ToString();
                PeopleTime.Text = PeopleInfo[1][4].ToString();
                PeopleWhere.Text = PeopleInfo[1][5].ToString();
                PeopleFamily.Text = PeopleInfo[1][6].ToString();
                PeopleAgreement.Text = PeopleInfo[1][7].ToString();
                PeopleExp.Text = PeopleInfo[1][8].ToString();
                PHAGrid.ItemsSource = PHA.GetBeautyByID(PeopleID);

                int AnimalID = (int)(PeopleRequestsGrid.SelectedItem as DataRowView).Row[1];
                AHAGrid.ItemsSource = AHA.GetDataByID(AnimalID);
            }
            
        }

        private void DecisionButton_Click(object sender, RoutedEventArgs e)
        {
            if ((StatusBox.SelectedItem != null) && (AnimalBox.SelectedItem != null))
            {
                int Req_ID = (int)(StatusBox.SelectedItem as DataRowView).Row[2];
                int PeopleID = (int)(PeopleRequestsGrid.SelectedItem as DataRowView).Row[0];
                int Animal_ID = (int)(AnimalBox.SelectedItem as DataRowView).Row[0];
                peopleRequests.InsertQuery(PeopleID, Animal_ID, Req_ID);
            }
        }

        private void ChangeAnimalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(nameInput.Text) && (AnimalTypeInput.SelectedItem != null) && (AnimalSubtypeInput.SelectedItem != null))
            {
                int animalType = (int)(AnimalTypeInput.SelectedItem as DataRowView).Row[0];
                int animalSub = (int)(AnimalSubtypeInput.SelectedItem as DataRowView).Row[0];
                Animals.InsertQuery(animalType, animalSub, nameInput.Text, checks.LoggedWorker, "none", "none");
            }
        }

        private void DeleteAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalNameInput.SelectedItem != null)
            {
                int AnimalID = AnimalNameInput.SelectedIndex + 1;
                Animals.DeleteQuery(AnimalID);
            }
        }

        private void SN_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SpecialNeedsInput.Text))
            {
                SpecialNeeds.InsertQuery(SpecialNeedsInput.Text);
            }
        }

        private void SN_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialNeedsGrid.SelectedItem != null)
            {
                int Diff_ID = (int)(SpecialNeedsGrid.SelectedItem as DataRowView).Row[0];
                SpecialNeeds.DeleteQuery(Diff_ID);

            }
        }

        private void SN_Change_Click(object sender, RoutedEventArgs e)
        {
            if ((SpecialNeedsGrid.SelectedItem != null) && (!string.IsNullOrEmpty(SpecialNeedsInput.Text)))
            {
                int SN_ID = (int)(SpecialNeedsGrid.SelectedItem as DataRowView).Row[0];
                SpecialNeeds.UpdateQuery(SpecialNeedsInput.Text, SN_ID);
            }
        }

        private void Diff_Add_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SpecialNeedsInput.Text))
            {
                SpecialDifferencies.InsertQuery(SpecialNeedsInput.Text);
            }
        }

        private void Diff_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SpecialNeedsGrid.SelectedItem != null)
            {
                int Sn_ID = (int)(SpecialNeedsGrid.SelectedItems as DataRowView).Row[0];    
                SpecialDifferencies.DeleteQuery(Sn_ID);
            }
        }

        private void Diff_Change_Click(object sender, RoutedEventArgs e)
        {
            if ((SpecialNeedsGrid.SelectedItem != null) && (string.IsNullOrEmpty(DifferenciesInput.Text)))
            {
                int Sn_ID = (int)(SpecialNeedsGrid.SelectedItems as DataRowView).Row[0];
                SpecialNeeds.UpdateQuery(DifferenciesInput.Text, Sn_ID);
            }
        }

        private void AHA_Add_Click(object sender, RoutedEventArgs e)
        {
            if ((AnimalNeedsCombo.SelectedItem != null) && (NeedsChoiceCombo != null))
            {
                int AnimalID = (int)(AnimalNeedsCombo.SelectedItem as DataRowView).Row[0];
                int AllergyID = (int)(NeedsChoiceCombo.SelectedItem as DataRowView).Row[0];
                AHA.InsertQuery(AnimalID, AllergyID);
            }
        }

        private void AD_Add_Click(object sender, RoutedEventArgs e)
        {
            if ((AnimalNameDiffCombo.SelectedItem != null) && (DiffChoiceCombo.SelectedItem != null))
            {
                int AnimalID = (int)(AnimalNameDiffCombo.SelectedItem as DataRowView).Row[0];
                int AllergyID = (int)(DiffChoiceCombo.SelectedItem as DataRowView).Row[0];
                ASD.InsertQuery(AnimalID, AllergyID);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NoLogin nologinPage = new NoLogin();
            this.Content = nologinPage;
            checks.LoggedWorker = 0;
        }

        private void AHA_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AHAGrid.SelectedItem != null)
            {
                int animal_id = (int)(AHAGrid.SelectedItem as DataRowView).Row[0];
                int allergy_id = (int)(AHAGrid.SelectedItem as DataRowView).Row[1];
                AHA.DeleteQuery(animal_id, allergy_id);
            }
        }

        private void AD_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalDiffenreciesGrid.SelectedItem != null)
            {
                int animal_id = (int)(AnimalDiffenreciesGrid.SelectedItem as DataRowView).Row[0];
                int allergy_id = (int)(AnimalDiffenreciesGrid.SelectedItem as DataRowView).Row[1];
                ASD.DeleteQuery(animal_id, allergy_id);
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            PeopleRequestsGrid.ItemsSource = peopleRequests.GetData();
            PHAGrid.ItemsSource = PHA.GetBeauty();
            SpecialNeedsGrid.ItemsSource = SpecialNeeds.GetData();
            DifferenciesGrid.ItemsSource = SpecialDifferencies.GetData();
            StatusBox.ItemsSource = Status.GetData();
            StatusBox.DisplayMemberPath = "RequestStatus_Name";
            AHAGrid.ItemsSource = AHA.getBeauty();
            AnimalDiffenreciesGrid.ItemsSource = ASN.GetBeauty();
            AnimalBox.ItemsSource = Animals.GetData();
            AnimalBox.DisplayMemberPath = "Animal_Name";
            AnimalNameInput.ItemsSource = Animals.GetData();
            AnimalNameInput.DisplayMemberPath = "Animal_Name";
            AnimalTypeInput.ItemsSource = AnimalTypes.GetData();
            AnimalTypeInput.DisplayMemberPath = "AnimalType_Name";
            AnimalSubtypeInput.ItemsSource = AnimalSubtypes.GetData();
            AnimalTypeInput.DisplayMemberPath = "AnimalSubtype_Name";
            NeedsChoiceCombo.ItemsSource = SpecialNeeds.GetData();
            NeedsChoiceCombo.DisplayMemberPath = "SpecialNeeds_Name";
            AnimalNeedsCombo.ItemsSource = Animals.GetData();
            AnimalNeedsCombo.DisplayMemberPath = "Animal_Name";
            DiffChoiceCombo.ItemsSource = SpecialDifferencies.GetData();
            DiffChoiceCombo.DisplayMemberPath = "SpecialNeeds_Name";
            AnimalNameDiffCombo.ItemsSource = Animals.GetData();
            AnimalNameInput.DisplayMemberPath = "Animal_Name";
            AnimalsGrid.ItemsSource = Animals.GetData();
        }
    }
}
