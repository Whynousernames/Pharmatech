using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MedicineMainWindow : Window
    {
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();


        public MedicineMainWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            labelMedicationIsActive.Visibility = Visibility.Hidden;
            textBox_IsActive.Visibility = Visibility.Hidden;

            for (int i = 1; i <= 7; i++)
            {
                comboBox_Schedule.Items.Add(i);
            }


            // Fill medicine combobox with medicine entries in database.
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT MedID, MedName FROM Medication", con);
                    con.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_selectMedication.Items.Add(sqlReader["MedName"].ToString());
                    }
                    sqlReader.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not populate medication combobox from database.", ex.ToString());
                }
            }

            // Set combobox's initial display to the first item.
           // comboBox_selectMedication.SelectedIndex = 0;
        }


        void messageTimer_Tick(object sender, EventArgs e)
        {
            label_Time.Content = DateTime.Now.ToString();

        }
        
        void gridHidden_True()
        {
            Grid_Employee.Visibility = Visibility.Hidden;
            Grid_sales.Visibility = Visibility.Hidden;
            Grid_patient.Visibility = Visibility.Hidden;
            Grid_medication.Visibility = Visibility.Hidden;
            Grid_Instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            Grid_instruction.Visibility = Visibility.Hidden;        
        }

        private void button_newCashSale_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            NewSaleWindow newCashSaleWindow = new NewSaleWindow();
            newCashSaleWindow.label_SaleWindowType.Content = "New Sale";
            newCashSaleWindow.ShowDialog();
            this.Close();
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

        private void button_next_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            // git test
        }

        private void button_next_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            string medID = textBox_MedicationID.Text;
            string medName = textBox_MedicationName.Text;
            string schedLevel = comboBox_Schedule.SelectedItem.ToString();
            int costPrice = Convert.ToInt32(textBox_CostPrice.Text);
           // int markUp = Convert.ToInt32(textBox_Markup.Text);
            int salePrice = Convert.ToInt32(textBox_SalePrice.Text);
            int quantityStock = Convert.ToInt32(textBox_QuantityInStock.Text);
            string medDescription = textBox_QuantityInStock_Copy.Text;



            if (label_MedicationWindowType.Content.ToString() == "Add Medication")
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to add this medicine to the system?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Add medicine item to system.
                    DataAccess.MedicationDA.AddMedication(medName, schedLevel, medDescription, costPrice, salePrice, quantityStock);
                   // DataAccess.AllergiesDA.AddAllergy(allergyID, patientID);
                    System.Windows.MessageBox.Show("Successfully added a new medicine.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    Grid_MedicationMainWindow.Visibility = Visibility.Visible;
                }
            }

            if (label_MedicationWindowType.Content.ToString() == "Update Medication")
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to update this medicine on the system?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Add medicine item to system.
                    DataAccess.MedicationDA.UpdateMedication(medID, medName, schedLevel, medDescription, costPrice, salePrice, quantityStock);
                    // DataAccess.AllergiesDA.AddAllergy(allergyID, patientID);
                    System.Windows.MessageBox.Show("Successfully updated medicine.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    Grid_MedicationMainWindow.Visibility = Visibility.Visible; 
                                   
                }
            
            }

        }

        private void button_AddPatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.label_PatientWindowType.Content = "Add Patient";
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Hidden;
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_updatePatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Update Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_ViewPatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "View Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removePatient_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Remove Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_addMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Add Medication";

            
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Hidden;            
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_updateMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_viewMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "View Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removeMedication_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            MedicineMainWindow medicineMainWindow = new MedicineMainWindow();
            medicineMainWindow.label_MedicationWindowType.Content = "Remove Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_nextSelectMedication_Click(object sender, RoutedEventArgs e)
        {
            comboBox_Schedule.Items.Clear();
            gridHidden_True();           

            string medName = comboBox_selectMedication.SelectedItem.ToString();

            if (label_MedicationWindowType.Content.ToString() == "View Medication")
            {
                button_Next.Visibility = Visibility.Hidden;
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT * FROM Medication WHERE medName = @medName";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@medName", medName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_MedicationID.Text = reader["medID"].ToString();
                        textBox_MedicationName.Text = reader["medName"].ToString();
                        comboBox_Schedule.Items.Add(reader["scheduleLevel"].ToString());
                        textBox_CostPrice.Text = "R " + reader["costPrice"].ToString();
                        textBox_SalePrice.Text = "R " + reader["salePrice"].ToString();
                        textBox_QuantityInStock.Text = reader["quantityInStock"].ToString();
                        textBox_QuantityInStock_Copy.Text = reader["Description"].ToString();
                        textBox_IsActive.Text = reader["isActive"].ToString();
                    }
                    reader.Close();
                    con.Close();
                }
                 comboBox_Schedule.SelectedIndex = 0;

                if (textBox_IsActive.Text == "n")
                {
                    labelMedicationIsActive.Content = "* Note: This item is currently removed and not in the pharmacy.";
                    labelMedicationIsActive.Visibility = Visibility.Visible;
                }


                Grid_SelectMedication.Visibility = Visibility.Hidden;
                Grid_MedicationMainWindow.Visibility = Visibility.Visible;
                disableTextboxes();
            }


            if (label_MedicationWindowType.Content.ToString() == "Update Medication")
            {
                button_Next.Content = "Update";
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT * FROM Medication WHERE medName = @medName";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@medName", medName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_MedicationID.Text = reader["medID"].ToString();
                        textBox_MedicationName.Text = reader["medName"].ToString();
                        comboBox_Schedule.Items.Add(reader["scheduleLevel"].ToString());
                        textBox_CostPrice.Text = reader["costPrice"].ToString();
                        textBox_SalePrice.Text = reader["salePrice"].ToString();
                        textBox_QuantityInStock.Text = reader["quantityInStock"].ToString();
                        textBox_QuantityInStock_Copy.Text = reader["Description"].ToString();                     
                    }
                    reader.Close();
                    con.Close();
                }

                Grid_SelectMedication.Visibility = Visibility.Hidden;
                Grid_MedicationMainWindow.Visibility = Visibility.Visible;
                //    comboBox_Schedule.SelectedIndex = 0;
            }
      

             //Soft delete medication item from database.
            if (label_MedicationWindowType.Content.ToString() == "Remove Medication")
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to delete medication item [" + comboBox_selectMedication.SelectedItem.ToString() + "]" +  "on the system?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Soft delete/flag in-active medicine item in database.
                    DataAccess.MedicationDA.DeleteMedication(medName);
                    // DataAccess.AllergiesDA.AddAllergy(allergyID, patientID);
                    System.Windows.MessageBox.Show("Successfully deleted medicine item.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    Grid_SelectMedication.Visibility = Visibility.Visible;
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    Grid_SelectMedication.Visibility = Visibility.Visible;
                }                           
            }

        }

        private void button_nextInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_MedicationMainWindow.Visibility = Visibility.Visible;
        }

        private void button_cancelInstruction_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_MedicationMainWindow.Visibility = Visibility.Visible;
        }

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();
            this.Hide();
            reportsMainWindow.ShowDialog();
        }

        void disableTextboxes()
        {
            textBox_MedicationID.IsEnabled = false;
            textBox_MedicationName.IsEnabled = false;
            comboBox_Schedule.IsEnabled = false;
            textBox_CostPrice.IsEnabled = false;
            textBox_SalePrice.IsEnabled = false;
            textBox_QuantityInStock.IsEnabled = false;
            textBox_QuantityInStock_Copy.IsEnabled = false;

        }
    }

}

