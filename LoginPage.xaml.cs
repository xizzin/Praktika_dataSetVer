using Praktika_5DataSetVer.AnimalShelterDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        AccountsTableAdapter Accounts = new AccountsTableAdapter();
        private string CreateSHA256(string input)
        {
            SHA256 hash = SHA256.Create();
            return Convert.ToString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }
        public LoginPage()
        {
            InitializeComponent();
            
        }
        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            var AllLogins = Accounts.GetData().Rows;
            string Passhash = CreateSHA256(PasswordInput.Password);
            for (int i = 0; i < AllLogins.Count+1; i++) 
            {
                if ((AllLogins[i][3].ToString() == LoginInput.Text) && (AllLogins[i][4].ToString() == Passhash))
                {
                    int roleID = (int)AllLogins[i][1];
                    checks.LoggedWorker = (int)AllLogins[i][2];

                    switch (roleID)
                    {
                        case 1:
                            AdminPage adminPage = new AdminPage();
                            AuthChoice.Content = adminPage;
                            break;
                        case 2:
                            ControlPagexaml controlPagexaml = new ControlPagexaml();
                            AuthChoice.Content = controlPagexaml;
                            break;
                        case 3:
                            HandlersPagexaml handlers = new HandlersPagexaml();
                            AuthChoice.Content = handlers;
                            break;
                        case 4:
                            VetPage vetPage = new VetPage();
                            AuthChoice.Content = vetPage;
                            break;
                    }                   
                }
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            NoLogin nologinPage = new NoLogin();
            AuthChoice.Content = nologinPage;
        }
    }
}
