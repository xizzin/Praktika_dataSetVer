using Praktika_5DataSetVer.AnimalShelterDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    
    public partial class AdminPage : Page
    {
        private string CreateSHA256(string input)
        {
            SHA256 hash = SHA256.Create();
            return Convert.ToString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }

        RequestStatusTableAdapter RequestsStatus = new RequestStatusTableAdapter();
        WorkersTableAdapter Workers = new WorkersTableAdapter();
        JobTitleTableAdapter JobTitle = new JobTitleTableAdapter();
        AccountsTableAdapter Accounts = new AccountsTableAdapter();
        public AdminPage()
        {
            InitializeComponent();
            RequestStatusGrid.ItemsSource = RequestsStatus.GetData();
            WorkersGrid.ItemsSource = Workers.GetData();

            JobID_Combo.ItemsSource = JobTitle.GetData();
            JobID_Combo.DisplayMemberPath = "Job_Name";

            JobtitleGrid.ItemsSource = JobTitle.GetData();
            AccountsGrid.ItemsSource = Accounts.GetData();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NoLogin nologinPage = new NoLogin();
            this.Content = nologinPage;
            checks.LoggedWorker = 0;
        }

        private void AddStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StatusInput.Text))
            {
                RequestsStatus.InsertQuery(StatusInput.Text);
            }
        }

        private void DeleteStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (RequestStatusGrid.SelectedItem != null)
            {
                int ReqID = (int)(RequestStatusGrid.SelectedItem as DataRowView).Row[0];
                RequestsStatus.DeleteQuery(ReqID);
            }
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if ((RequestStatusGrid.SelectedItem != null) && (string.IsNullOrEmpty(StatusInput.Text)))
            {
                int ReqID = (int)(RequestStatusGrid.SelectedItem as DataRowView).Row[0];
                RequestsStatus.UpdateQuery(StatusInput.Text, ReqID);
            }
        }

        private void WorkersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkersGrid.SelectedItem != null)
            {
                WorkerNameInput.Text = (string)(WorkersGrid.SelectedItem as DataRowView).Row[1];
                WorkerSecondNameInput.Text = (string)(WorkersGrid.SelectedItem as DataRowView).Row[2];
                WorkerThirdNameInput.Text = (string)(WorkersGrid.SelectedItem as DataRowView).Row[3];
                JobID_Combo.SelectedIndex = (int)(WorkersGrid.SelectedItem as DataRowView).Row[4] + 1;
            }
        }

        private void AddWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrEmpty(WorkerNameInput.Text) && (!string.IsNullOrEmpty(WorkerSecondNameInput.Text) && (!string.IsNullOrEmpty(WorkerThirdNameInput.Text) && (JobID_Combo.SelectedItem != null)))))
            {
                int job_ID = (int)(JobID_Combo.SelectedItem as DataRowView).Row[0];
                Workers.InsertQuery(WorkerNameInput.Text, WorkerSecondNameInput.Text, WorkerThirdNameInput.Text, job_ID);
            }
        }

        private void DeleteWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (WorkersGrid.SelectedItem != null)
            {
                int workerID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[0];
                Workers.DeleteQuery(workerID);
            }
        }

        private void ChangeWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            if ((WorkersGrid != null) && (!string.IsNullOrEmpty(WorkerNameInput.Text) && (!string.IsNullOrEmpty(WorkerSecondNameInput.Text) && (!string.IsNullOrEmpty(WorkerThirdNameInput.Text) && (JobID_Combo.SelectedItem != null)))))
            {
                int JobID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[4] + 1;
                int workerID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[0];
                Workers.UpdateQuery(WorkerNameInput.Text, WorkerSecondNameInput.Text, WorkerThirdNameInput.Text, JobID, workerID);
            }
        }

        private void JobtitleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JobtitleGrid.SelectedItem != null)
            {
                JobnameInput.Text = (string)(JobtitleGrid.SelectedItem as DataRowView).Row[1];
            }
        }

        private void AddJobButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(JobnameInput.Text))
            {
                JobTitle.InsertQuery(JobnameInput.Text);
            }
        }

        private void DeleteJobButton_Click(object sender, RoutedEventArgs e)
        {
            if (JobtitleGrid.SelectedItem != null)
            {
                int jobID = (int)(JobtitleGrid.SelectedItem as DataRowView).Row[0];
                JobTitle.DeleteQuery(jobID);
            }
        }

        private void ChangeJobButton_Click(object sender, RoutedEventArgs e)
        {
            if ((JobtitleGrid.SelectedItem != null) && (!string.IsNullOrEmpty(JobnameInput.Text)))
            {
                int jobID = (int)(JobtitleGrid.SelectedItem as DataRowView).Row[0];
                JobTitle.UpdateQuery(JobnameInput.Text, jobID);
            }
        }

        private void AccountsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountsGrid.SelectedItem != null)
            {
                LoginInput.Text = (string)(AccountsGrid.SelectedItem as DataRowView).Row[3];
                PasswordInput.Password = (string)(AccountsGrid.SelectedItem as DataRowView).Row[4];
            }
        }

        private void AddAccButton_Click(object sender, RoutedEventArgs e)
        {
            if ((WorkersGrid.SelectedItem != null) && (!string.IsNullOrEmpty(LoginInput.Text) && (!string.IsNullOrEmpty(PasswordInput.Password))))
            {
                string Passhash = CreateSHA256(PasswordInput.Password);
                int WorkerID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[0];
                int JobID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[4];
                Accounts.InsertQuery(JobID, WorkerID, LoginInput.Text, Passhash);
            }
        }

        private void DeleteAccButton_Click(object sender, RoutedEventArgs e)
        {
            if (AccountsGrid.SelectedItem != null)
            {
                int accID = (int)(AccountsGrid.SelectedItem as DataRowView).Row[0];
                Accounts.DeleteQuery(accID);
            }
        }

        private void ChangeAccButton_Click(object sender, RoutedEventArgs e)
        {
            if ((WorkersGrid.SelectedItem != null) && (!string.IsNullOrEmpty(LoginInput.Text) && (!string.IsNullOrEmpty(PasswordInput.Password))))
            {
                string Passhash = CreateSHA256(PasswordInput.Password);
                int WorkerID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[0];
                int JobID = (int)(WorkersGrid.SelectedItem as DataRowView).Row[4];
                Accounts.UpdateQuery(JobID, WorkerID, LoginInput.Text, Passhash, WorkerID);
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            RequestStatusGrid.ItemsSource = RequestsStatus.GetData();
            WorkersGrid.ItemsSource = Workers.GetData();

            JobID_Combo.ItemsSource = JobTitle.GetData();
            JobID_Combo.DisplayMemberPath = "Job_Name";

            JobtitleGrid.ItemsSource = JobTitle.GetData();
            AccountsGrid.ItemsSource = Accounts.GetData();
        }
    }
}
