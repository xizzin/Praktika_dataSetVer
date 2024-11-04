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

namespace Praktika_5DataSetVer
{
    /// <summary>
    /// Логика взаимодействия для ControlPagexaml.xaml
    /// </summary>
    public partial class ControlPagexaml : Page
    {
        public ControlPagexaml()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            NoLogin nologinPage = new NoLogin();
            this.Content = nologinPage;
            checks.LoggedWorker = 0;
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
