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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {

        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        public string allergyID;
        


        public PatientMainWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            arrowHidden_True();

            

            if(label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                button_next.Content = "Remove this Patient";
            }
            if (label_PatientWindowType.Content.ToString() == "View Patient")
            {
                button_next.Visibility = Visibility.Hidden;
            }



            using (SqlConnection con = new SqlConnection(conn))
                {
                    try
                    {
                        SqlCommand sqlCmd = new SqlCommand("SELECT allergyName FROM Allergies", con);
                        con.Open();
                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                        while (sqlReader.Read())
                        {
                            comboBox_selectAllergy.Items.Add(sqlReader["allergyName"].ToString());
                        }
                        sqlReader.Close();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Could not populate allergies combobox from database.", ex.ToString());
                    }
                }


        }


        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //The following is a method to display the "help function" on F1 keypress.
            //It displays a messagebox and arrows pointing to the relavent elements.
            //When the user clicks "OK" on the messagebox the messagebox and the arrows are closed.
            if (e.Key == Key.F1)
            {

                image_arrow.Visibility = Visibility.Visible;
                image_arrow_Copy.Visibility = Visibility.Visible;
                image_arrow_Copy1.Visibility = Visibility.Visible;
                image_arrow_Copy2.Visibility = Visibility.Visible;

                if(label_PatientWindowType.Content.ToString() == "Add Patient")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Input patient details." + Environment.NewLine + "Add allergies to patient."
                 + Environment.NewLine + "Allergies are displayed in the grid" + Environment.NewLine + "Click 'Add Patient Button' to add the patient to the system.", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
                }
                if (label_PatientWindowType.Content.ToString() == "View Patient")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Patient Details are displayed." + Environment.NewLine + "Allergies are displayed in the grid."
                 + Environment.NewLine + "Click 'Cancel button' once complete.", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
                }
                if (label_PatientWindowType.Content.ToString() == "Update Patient")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Patient Details are displayed." + Environment.NewLine + "Allergies are displayed in the grid."
                 + Environment.NewLine + "Make changes to existing patient details.", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
                }
                if (label_PatientWindowType.Content.ToString() == "Remove Patient")
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("Patient Details are displayed." + Environment.NewLine + "Allergies are displayed in the grid."
                 + Environment.NewLine + "Click 'Remove Patient Button' to set patient as inactive", "Help!", MessageBoxButton.OK, MessageBoxImage.Question);
                    if (result == MessageBoxResult.OK)
                    {
                        arrowHidden_True();
                    }
                }






            }
        }

        void arrowHidden_True()
        {
            image_arrow.Visibility = Visibility.Hidden;
            image_arrow_Copy.Visibility = Visibility.Hidden;
            image_arrow_Copy1.Visibility = Visibility.Hidden;
            image_arrow_Copy2.Visibility = Visibility.Hidden;
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
            Grid_instruction.Visibility = Visibility.Hidden;
            Grid_Report.Visibility = Visibility.Hidden;
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            Grid_MedicalAidSelect.Visibility = Visibility.Hidden;
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            
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

            string id = textBox_IDNumber.Text;
            string firstName = textBox_FirstName.Text;
            string surname = textBox_Surname.Text;
            string contactNo = textBox_ContactNumber.Text;
            string email = textBox_Email.Text;
            string address1 = textBox_AddressLine1.Text;
            string address2 = textBox_AddressLine2.Text;          
            string patientID = textBox_IDNumber.Text;
                               

            if (label_PatientWindowType.Content.ToString() == "Add Patient")
            {
                Grid_PatientMain.Visibility = Visibility.Hidden;

                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(contactNo) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address1))
                {
                    System.Windows.MessageBox.Show("Not all fields are completed.", "Alert!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    Grid_PatientMain.Visibility = Visibility.Visible;
                }
                else
                { 
                    MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to add this patient to the system?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        // Add Patient to system.
                        DataAccess.PatientDA.AddPatient(id, firstName, surname, contactNo, email, address1, address2);
                        DataAccess.AllergiesDA.AddAllergy(allergyID, patientID);
                        System.Windows.MessageBox.Show("Successfully added a new patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {
                        Grid_PatientMain.Visibility = Visibility.Visible;
                    }

                }


            }


            if (label_PatientWindowType.Content.ToString() == "Remove Patient")
            {

                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to delete this patient from the system?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Delete Patient from system.
                        DataAccess.PatientDA.DeletePatient(id);
                        System.Windows.MessageBox.Show("Successfully deleted patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);                   
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    Grid_PatientMain.Visibility = Visibility.Visible;
                }
                
            }

            if (label_PatientWindowType.Content.ToString() == "Update Patient")
            {
                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to update this patient on the system?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Update patient on system.
                    DataAccess.PatientDA.UpdatePatient(id, firstName, surname, contactNo, email, address1, address2);
                    System.Windows.MessageBox.Show("Successfully updated patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                }
                else if (dialogResult == MessageBoxResult.No)
                {
                    Grid_PatientMain.Visibility = Visibility.Visible;
                }

            }




        }

        private void button_cancel_Click(object sender, RoutedEventArgs e)
        {
            
            MainMenuWindow mainMenuWindow = new MainMenuWindow();
            mainMenuWindow.ShowDialog();
            this.Close();

        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            if (label_PatientWindowType.Content.ToString() == "Update Patient"|| label_PatientWindowType.Content.ToString() == "Update Patient"|| label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                Grid_PatientMain.Visibility = Visibility.Hidden;
                Grid_SelectPatient.Visibility = Visibility.Visible;
            }
           
            else
            {
                MainMenuWindow mainMenuWindow = new MainMenuWindow();
                mainMenuWindow.ShowDialog();
                this.Close();
            }
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT allergyID, allergyName, allergyDescription FROM Allergies WHERE allergyName = @allergyName";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.AddWithValue("@allergyName", comboBox_selectAllergy.Text);
                SqlDataReader reader = cmd.ExecuteReader();

                dt.Columns.Add("AllergyID");
                dt.Columns.Add("AllergyName");
                dt.Columns.Add("AllergyDescription");

                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["AllergyID"] = reader["allergyID"].ToString();
                    row["AllergyDescription"] = reader["allergyDescription"].ToString();
                    row["AllergyName"] = reader["allergyName"].ToString();
                    dt.Rows.Add(row);


                    allergyID = dt.Rows[0]["AllergyID"].ToString();

                    //dataGrid_Allergies.Items.Add(reader["allergyID"].ToString());
                    //dataGrid_Allergies.Items.Add(reader["allergyName"].ToString());
                    //dataGrid_Allergies.Items.Add(reader["allergyDescription"].ToString());

                }
                dataGrid_Allergies.ItemsSource = dt.DefaultView;
                con.Close();


            }
           



        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            
        }

       
        private void button_YesSelect_Click(object sender, RoutedEventArgs e)
        {
            Grid_YesNoSelect.Visibility = Visibility.Hidden;
            Grid_MedicalAidSelect.Visibility = Visibility.Visible;
        }

        private void button_NoSelect_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_nextSelectPatient_Click(object sender, RoutedEventArgs e)
        {
            string idNumber = textBox_IDNumberSelect.Text;

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT * FROM Patient WHERE PatientIDNumber = @id";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.AddWithValue("@id", idNumber);
                SqlDataReader reader = cmd.ExecuteReader();

                // Check to see if the input Patient ID is present in the database.
                if (reader.HasRows)
                {
                    Grid_SelectPatient.Visibility = Visibility.Hidden;
                    Grid_PatientMain.Visibility = Visibility.Visible;
                }
                else
                {
                    System.Windows.MessageBox.Show("The patient ID number entered is not currently on the system.", "Alert!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                con.Close();

            }

            if(label_PatientWindowType.Content.ToString() == "View Patient")
            {
                button_next.Visibility = Visibility.Hidden;
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT * FROM Patient WHERE PatientIDNumber = @id";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@id", idNumber);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_IDNumber.Text = reader["PatientIDNumber"].ToString();
                        textBox_FirstName.Text = reader["firstName"].ToString();
                        textBox_Surname.Text = reader["lastName"].ToString();
                        textBox_Email.Text = reader["email"].ToString();
                        textBox_ContactNumber.Text = reader["contactNumber"].ToString();
                        textBox_AddressLine1.Text = reader["physicalAddress1"].ToString();
                        textBox_AddressLine2.Text = reader["physicalAddress2"].ToString();

                    }
                    reader.Close();
                    con.Close();
                }
                disableTextBoxes();
            }


            if(label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                button_next.Width = 190;
                button_next.Content = "Remove Patient";

                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT * FROM Patient WHERE PatientIDNumber = @id";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@id", idNumber);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_IDNumber.Text = reader["PatientIDNumber"].ToString();
                        textBox_FirstName.Text = reader["firstName"].ToString();
                        textBox_Surname.Text = reader["lastName"].ToString();
                        textBox_Email.Text = reader["email"].ToString();
                        textBox_ContactNumber.Text = reader["contactNumber"].ToString();
                        textBox_AddressLine1.Text = reader["physicalAddress1"].ToString();
                        textBox_AddressLine2.Text = reader["physicalAddress2"].ToString();

                    }
                    reader.Close();
                    con.Close();
                }

                disableTextBoxes();
            }

            if(label_PatientWindowType.Content.ToString() == "Update Patient")
            {
                button_next.Width = 175;
                button_next.Content = "Update Patient";

                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT * FROM Patient WHERE PatientIDNumber = @id";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@id", idNumber);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_IDNumber.Text = reader["PatientIDNumber"].ToString();
                        textBox_FirstName.Text = reader["firstName"].ToString();
                        textBox_Surname.Text = reader["lastName"].ToString();
                        textBox_Email.Text = reader["email"].ToString();
                        textBox_ContactNumber.Text = reader["contactNumber"].ToString();
                        textBox_AddressLine1.Text = reader["physicalAddress1"].ToString();
                        textBox_AddressLine2.Text = reader["physicalAddress2"].ToString();

                    }
                    reader.Close();
                    con.Close();
                }


            }
        }

       

        

        private void button_AddPatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();            
            patientMainWindow.label_PatientWindowType.Content = "Add Patient";
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Hidden;
            patientMainWindow.ShowDialog();
            this.Close();




        }

        private void button_updatePatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "Update Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_ViewPatient_Click_1(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            PatientMainWindow patientMainWindow = new PatientMainWindow();
            patientMainWindow.Grid_SelectPatient.Visibility = Visibility.Visible;
            patientMainWindow.Grid_PatientMain.Visibility = Visibility.Hidden;
            patientMainWindow.label_PatientWindowType.Content = "View Patient";
            patientMainWindow.ShowDialog();
            this.Close();
        }

        private void button_removePatient_Click_1(object sender, RoutedEventArgs e)
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
            medicineMainWindow.label_MedicationWindowType.Content = "Update Medication";
            medicineMainWindow.Grid_SelectMedication.Visibility = Visibility.Visible;
            medicineMainWindow.Grid_MedicationMainWindow.Visibility = Visibility.Hidden;
            medicineMainWindow.ShowDialog();
            this.Close();
        }

        private void button_generateReport_Click(object sender, RoutedEventArgs e)
        {
            gridHidden_True();
            ReportsMainWindow reportsMainWindow = new ReportsMainWindow();
            this.Hide();
            reportsMainWindow.ShowDialog();
        }

        private void comboBox_selectAllergy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    string cmdString = "SELECT allergyDescription FROM Allergies WHERE allergyName = @allergyName";
                    SqlCommand cmd = new SqlCommand(cmdString, con);
                    cmd.Parameters.AddWithValue("@allergyName", comboBox_selectAllergy.Text);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        textBox_AllergyDescription.Text = reader["allergyDescription"].ToString();

                    }

                    con.Close();

                }
            }
        }

        // Simple function to disable textboxes, for viewing purposes only.
        public void disableTextBoxes()
        {
            textBox_IDNumber.IsEnabled = false;
            textBox_FirstName.IsEnabled = false;
            textBox_Surname.IsEnabled = false;
            textBox_Email.IsEnabled = false;
            textBox_ContactNumber.IsEnabled = false;
            textBox_AddressLine1.IsEnabled = false;
            textBox_AddressLine2.IsEnabled = false;
            textBox_City.IsEnabled = false;
            textBox_Suburb.IsEnabled = false;
            comboBox_selectAllergy.IsEnabled = false;
            textBox_AllergyDescription.IsEnabled = false;
            button_Remove.IsEnabled = false;
            button_Add.IsEnabled = false;

        }
    }

}

