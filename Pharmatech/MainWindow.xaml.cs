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
using System.Data;
using DataAccess;
using BusinessLogic;
using System.Configuration;


namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            // Log user (employee) into system. Throw error if username/password incorrect, proceed to main system window if correct.

            var username = textBox_Username.Text;
            var password = textBox_Password.Text;
            
            int empID = EmployeeDA.AuthenticateLogin(username, password);
            if (empID > 0)
            {
                
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.ShowDialog();
                this.Close();
            }
            else
            {                
                MessageBox.Show("Invalid username and/or password. Please try again.", "Incorrect login!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                textBox_Username.Focus();
            }               
        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
