using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class NewSaleWindow : Window
    {
         static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();


        public NewSaleWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            label_WindowType.Content = "New Sale";
            Grid_Sale.Visibility = Visibility.Visible;
            Grid_sales.Visibility = Visibility.Hidden;

            // Populate combobox with medicine pulled from the database.            
            using (SqlConnection conn = new SqlConnection(connection))
            {
                try
                {
                    string query = "SELECT MedID, MedName FROM Medication";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    conn.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Medication");
                 //   comboBox_select_Item. = "MedName";
                 //   comboBox_select_Item.ValueMember = "MedID";
                 //   comboBox_select_Item.DataSource = ds.Tables["Medication"];
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
            }         
        }



        void messageTimer_Tick(object sender, EventArgs e)
        {
            label_Time.Content = DateTime.Now.ToString();

        }
        
        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_Sale.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            Grid_saleTypeSelect.Visibility = Visibility.Hidden;
            Grid_AddInstruction.Visibility = Visibility.Hidden;
            
        }

        private void button_newCashSale_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_Sale_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_sales.Visibility = Visibility.Visible;
        }

        private void button_Patient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_patient.Visibility = Visibility.Visible;
        }

        private void button_Medication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_medication.Visibility = Visibility.Visible;
        }

        private void button_Instruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_instruction.Visibility = Visibility.Visible;
        }

        private void button_Employee_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Employee.Visibility = Visibility.Visible;
        }

        private void button_Reports_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Report.Visibility = Visibility.Visible;
        }

        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            Grid_patientSelect.Visibility = Visibility.Hidden;
            Grid_Sale.Visibility = Visibility.Visible;
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }

       

        private void button_cashSaleSelect_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;
            SaleFinalWindow saleFinalWindow = new SaleFinalWindow();
            this.Close();
            saleFinalWindow.ShowDialog();
        }

        private void button_medicalAidSaleSelect_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;
            SaleFinalWindow saleFinalWindow = new SaleFinalWindow();
            this.Close();
            saleFinalWindow.ShowDialog();
        }

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_saleTypeSelect.Visibility = Visibility.Visible;
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_AddInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_AddInstruction.Visibility = Visibility.Visible;
        }

        private void button_ProceedInstructions_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_Sale.Visibility = Visibility.Visible;
            
        }

        private void button_addItem_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox_select_Item.SelectedItem == null)
            {
                MessageBox.Show("You have not selected medication for this patient.", "Error.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
    }




