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
using BusinessObject;
using System.IO;

namespace Pharmatech
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class PatientMainWindow : Window
    {
        static string connection = System.Configuration.ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        string conn = ConfigurationManager.ConnectionStrings["connstring"].ConnectionString.ToString();
        public string allergyID;
        public List<Allergies> allergiesList = new List<Allergies>();
        int _count = 0;
        DataTable dt = new DataTable();

        int medAidSelected = 0;

        string empIDNumber;
        string empFName;
        string empLName;
        string empType;
        string[] employeeDetails = new string[5];

        string medAidID;
        string medAidName;
        string medAidDescription;
        string medAidAmountRemaining;

        string medAidIDPatient;
        int amountRemaining;

        public PatientMainWindow()
        {
            InitializeComponent();
            DispatcherTimer messageTimer = new DispatcherTimer();
            messageTimer.Tick += new EventHandler(messageTimer_Tick);
            messageTimer.Interval = new TimeSpan(0, 0, 1);
            messageTimer.Start();
            gridHidden_True();
            arrowHidden_True();
            button_ViewPatient.Visibility = Visibility.Hidden;

            textBox_PatientMedicalAid.Text = "No Medial Aid Selected";

            

            if(label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                button_next.Content = "Remove this Patient";
            }
            if (label_PatientWindowType.Content.ToString() == "View Patient")
            {
                button_next.Visibility = Visibility.Hidden;
            }

            using (StreamReader reader = new StreamReader("emp.txt"))
            {
                while (!reader.EndOfStream)
                {

                    for (var i = 0; i < 5; i++)
                    {
                        var currLine = reader.ReadLine();
                        employeeDetails[i] = currLine;
                    }
                }
            }

            empIDNumber = employeeDetails[0];
            empFName = employeeDetails[1];
            empLName = employeeDetails[2];
            empType = employeeDetails[3];

            if (empType == "P")
            {
                button_Medication.IsEnabled = false;
                button_Instruction.IsEnabled = false;
                button_Reports.IsEnabled = false;
                button_Employee.IsEnabled = false;
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

                        SqlCommand sqlCmd2 = new SqlCommand("SELECT cityName FROM City", con);
                        SqlDataReader sqlReader2 = sqlCmd2.ExecuteReader();
                        while (sqlReader2.Read())
                        {
                        comboBox_selectCity.Items.Add(sqlReader2["cityName"].ToString());
                        }
                    sqlReader2.Close();
                    con.Close();

                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.ToString(), ex.ToString());
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
            Grid_AddMedicalAid.Visibility = Visibility.Hidden;
            
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

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {


            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string cmdString = "SELECT allergyID, allergyName, allergyDescription FROM Allergies WHERE allergyName = @allergyName";
                SqlCommand cmd = new SqlCommand(cmdString, con);
                cmd.Parameters.AddWithValue("@allergyName", comboBox_selectAllergy.Text);
                SqlDataReader reader = cmd.ExecuteReader();



                Allergies allergies = new Allergies();

                while (reader.Read())
                {

                    allergies.allergyID = reader["allergyID"].ToString();
                    allergies.allergyDescription = reader["allergyDescription"].ToString();
                    allergies.allergyName = reader["allergyName"].ToString();
                    //var row = dt.NewRow();
                    //row["AllergyID"] = reader["allergyID"].ToString();
                    //row["AllergyDescription"] = reader["allergyDescription"].ToString();
                    //row["AllergyName"] = reader["allergyName"].ToString();
                    //dt.Rows.Add(row);

                    allergiesList.Add(allergies);

                    //allergyID = dt.Rows[0]["AllergyID"].ToString();

                    //dataGrid_Allergies.Items.Add(reader["allergyID"].ToString());
                    //dataGrid_Allergies.Items.Add(reader["allergyName"].ToString());
                    //dataGrid_Allergies.Items.Add(reader["allergyDescription"].ToString());

                }


                if (_count == 0)
                {

                    dt.Columns.Add("Name");
                    dt.Columns.Add("Description");
                }

                foreach (var item in allergiesList)
                {
                    var row = dt.NewRow();
                    row["Name"] = item.allergyName;
                    row["Description"] = item.allergyDescription;
                    dt.Rows.Add(row);
                }
                dataGrid_Allergies.AutoGenerateColumns = true;
                reader.Close();
                con.Close();
                _count++;
                dataGrid_Allergies.ItemsSource = dt.AsDataView();
                allergiesList.Clear();
                con.Close();


            }

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
            string suburb = comboBox_selectSuburb.Text;
            string city = comboBox_selectCity.Text;
                               

            if (label_PatientWindowType.Content.ToString() == "Add Patient")
            {
               // Grid_PatientMain.Visibility = Visibility.Hidden;

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
                       DataAccess.PatientDA.AddPatient(id, firstName, surname, contactNo, email, address1, address2, city, suburb);
                        // DataAccess.AllergiesDA.AddAllergy(allergyID, patientID);
                        //  System.Windows.MessageBox.Show("Successfully added a new patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                        foreach (DataRow row in dt.Rows)
                        {

                            int allergyID = 0;



                            using (SqlConnection conn = new SqlConnection(connection))
                            {
                                try
                                {
                                    
                                    SqlConnection con = new SqlConnection(connection);
                                    using (SqlCommand cmd = con.CreateCommand())
                                    {
                                        cmd.CommandText = "INSERT INTO PatientAllergies(allergyID, patientIDNumber) VALUES(@aID, @idNumber)";                                        
                                        
                                        SqlCommand sqlCmd = new SqlCommand("SELECT allergyID FROM Allergies WHERE allergyName = @aName", conn);
                                        sqlCmd.Parameters.AddWithValue("@aName", row["Name"]);
                                        conn.Open();
                                        SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                                        while (sqlReader.Read())
                                        {
                                            allergyID = Convert.ToInt32(sqlReader["allergyID"]);

                                        }

                                        cmd.Parameters.AddWithValue("@aID", allergyID.ToString());
                                        cmd.Parameters.AddWithValue("@idNumber", textBox_IDNumber.Text);


                                        
                                        con.Open();
                                        cmd.ExecuteNonQuery();                                        
                                        sqlReader.Close();
                                        con.Close();

                                    }

                                    using (SqlCommand cmd2 = con.CreateCommand())
                                    {
                                        if(medAidSelected == 1)
                                        {
                                            cmd2.CommandText = "INSERT INTO Patient_MedicalAid_Account (medAidID, patientIDNumber, amountRemaining) VALUES (@medAidID, @patientIDNumber, @amountRemaining)";

                                            cmd2.Parameters.AddWithValue("@medAidID", medAidID);
                                            cmd2.Parameters.AddWithValue("@patientIDNumber", id);
                                            cmd2.Parameters.AddWithValue("@amountRemaining", medAidAmountRemaining);

                                            con.Open();
                                            cmd2.ExecuteNonQuery();
                                            con.Close();

                                        }                                        
                                    }

                                }
                                catch (Exception ex)
                                {
                                    System.Windows.MessageBox.Show(ex.ToString(), ex.ToString());
                                }
                            }

                        }
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {
                        Grid_PatientMain.Visibility = Visibility.Visible;
                    }

                }

            }


            if (label_PatientWindowType.Content.ToString() == "Remove Patient")
            {

                MessageBoxResult dialogResult = System.Windows.MessageBox.Show("Are you sure you would like to delete this patient, " + firstName + " " + surname + ",  from the system?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (dialogResult == MessageBoxResult.Yes)
                {
                    // Delete Patient from system.
                        DataAccess.PatientDA.DeletePatient(id);
                     //   System.Windows.MessageBox.Show("Successfully deleted patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);                   
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
                    DataAccess.PatientDA.UpdatePatient(id, firstName, surname, contactNo, email, address1, address2, city, suburb);
                  //  System.Windows.MessageBox.Show("Successfully updated patient.", "Note!", MessageBoxButton.OKCancel, MessageBoxImage.Information);
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
                        textBox_isActivePatient.Text = reader["isActive"].ToString();
                        comboBox_selectCity.Text = reader["cityName"].ToString();
                        populateSuburb();
                        comboBox_selectSuburb.Text = reader["suburbName"].ToString();

                    }
                    reader.Close();

                    string cmdString2 = "SELECT * FROM Patient_MedicalAid_Account WHERE patientIDNumber = @patientID";
                    SqlCommand cmd2 = new SqlCommand(cmdString2, con);
                    cmd2.Parameters.AddWithValue("@patientID", idNumber);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        medAidIDPatient = reader2["medAidID"].ToString();
                        amountRemaining = Convert.ToInt32(reader2["amountRemaining"]);
                        textBox_remainingBalance.Text = "R " + amountRemaining.ToString();
                    }
                    reader2.Close();

                    string cmdString3 = "SELECT * FROM MedicalAidPlan WHERE medAidID = @medAidID";
                    SqlCommand cmd3 = new SqlCommand(cmdString3, con);
                    cmd3.Parameters.AddWithValue("@medAidID", medAidIDPatient );
                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    while (reader3.Read())
                    {
                        textBox_PatientMedicalAid.Text = reader3["Name"].ToString();
                    }

                    con.Close();
                }
                disableTextBoxes();

                if (textBox_isActivePatient.Text == "n")
                {
                    label_isPatientActive.Content = "* Note: This patient is removed and is for viewing purposes only.";
                    label_isPatientActive.Visibility = Visibility.Visible;
                }
            }


            if(label_PatientWindowType.Content.ToString() == "Remove Patient")
            {
                button_next.Width = 220;
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

                    string cmdString2 = "SELECT * FROM Patient_MedicalAid_Account WHERE patientIDNumber = @patientID";
                    SqlCommand cmd2 = new SqlCommand(cmdString2, con);
                    cmd2.Parameters.AddWithValue("@patientID", idNumber);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        medAidIDPatient = reader2["medAidID"].ToString();
                        amountRemaining = Convert.ToInt32(reader2["amountRemaining"]);
                    }
                    reader2.Close();

                    string cmdString3 = "SELECT * FROM MedicalAidPlan WHERE medAidID = @medAidID";
                    SqlCommand cmd3 = new SqlCommand(cmdString3, con);
                    cmd3.Parameters.AddWithValue("@medAidID", medAidIDPatient);
                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    while (reader3.Read())
                    {
                        textBox_PatientMedicalAid.Text = reader3["Name"].ToString();
                    }
                    con.Close();
                }

                disableTextBoxes();
            }

            if(label_PatientWindowType.Content.ToString() == "Update Patient")
            {

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
                        comboBox_selectCity.Text = reader["cityName"].ToString();
                        //comboBox_selectSuburb.Items.Add(reader["suburbName"].ToString());
                        populateSuburb();
                        comboBox_selectSuburb.Text = reader["suburbName"].ToString();

                    }
                    reader.Close();

                    string cmdString2 = "SELECT * FROM Patient_MedicalAid_Account WHERE patientIDNumber = @patientID";
                    SqlCommand cmd2 = new SqlCommand(cmdString2, con);
                    cmd2.Parameters.AddWithValue("@patientID", idNumber);
                    SqlDataReader reader2 = cmd2.ExecuteReader();

                    while (reader2.Read())
                    {
                        medAidIDPatient = reader2["medAidID"].ToString();
                        amountRemaining = Convert.ToInt32(reader2["amountRemaining"]);
                        textBox_remainingBalance.Text = "R " + amountRemaining.ToString();
                    }
                    reader2.Close();

                    string cmdString3 = "SELECT * FROM MedicalAidPlan WHERE medAidID = @medAidID";
                    SqlCommand cmd3 = new SqlCommand(cmdString3, con);
                    cmd3.Parameters.AddWithValue("@medAidID", medAidIDPatient);
                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    while (reader3.Read())
                    {
                        textBox_PatientMedicalAid.Text = reader3["Name"].ToString();
                    }
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
            comboBox_selectCity.IsEnabled = false;
            comboBox_selectSuburb.IsEnabled = false;
            comboBox_selectAllergy.IsEnabled = false;
            textBox_AllergyDescription.IsEnabled = false;
            button_Remove.IsEnabled = false;
            button_Add.IsEnabled = false;

        }

        private void textBox_ContactNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Function on textchanged event, to on the fly not allow input OTHER than numerics.

            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void textBox_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void textBox_Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void textBox_IDNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void textBox_Suburb_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void textBox_City_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;
            String newText = String.Empty;
            int count = 0;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsLetter(c) || Char.IsControl(c) || (c == '.' && count == 0))
                {
                    newText += c;
                    if (c == '.')
                        count += 1;
                }
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ? selectionStart : textBox.Text.Length;
        }

        private void comboBox_selectSuburb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void comboBox_selectCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void comboBox_selectCity_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            comboBox_selectSuburb.Items.Clear();
            comboBox_selectSuburb.Items.Refresh();
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT suburbName FROM Suburb WHERE cityName = @cityName", con);
                    con.Open();
                    sqlCmd.Parameters.AddWithValue("@cityName", comboBox_selectCity.Text.ToString());
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_selectSuburb.Items.Add(sqlReader["suburbName"].ToString());

                    }
                    sqlReader.Close();


                    con.Close();


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Could not populate allergies combobox from database.", ex.ToString());
                }
            }
            comboBox_selectSuburb.Items.Refresh();
        }

        private void button_addMedicalAid_Click(object sender, RoutedEventArgs e)
        {
            Grid_AddMedicalAid.Visibility = Visibility.Visible;
            Grid_PatientMain.Visibility = Visibility.Hidden;
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT Name FROM MedicalAidPlan ORDER BY Name", con);
                    con.Open();
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_selectMedAid.Items.Add(sqlReader["Name"].ToString());
                    }
                    sqlReader.Close();                    
                    con.Close();

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), ex.ToString());
                }
            }

        }

        private void button_addMedAidtoPatient_Click(object sender, RoutedEventArgs e)
        {
            medAidSelected = 1;
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT medAidID, Name, Description, Amount FROM MedicalAidPlan WHERE Name = @Name", con);
                    con.Open();
                    sqlCmd.Parameters.AddWithValue("@Name", comboBox_selectMedAid.Text.ToString());
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {                            
                            medAidID = sqlReader["medAidID"].ToString();
                            medAidName = comboBox_MedicalAidID.Text.ToString();
                            medAidDescription = textBox_medAidDescription.Text.ToString();
                            medAidAmountRemaining = sqlReader["Amount"].ToString();
                            textBox_PatientMedicalAid.Text = sqlReader["Name"].ToString();
                            textBox_remainingBalance.Text = "R " + medAidAmountRemaining.ToString();

                        }
                    }
                    sqlReader.Close();
                    

                    con.Close();


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Could not populate allergies combobox from database.", ex.ToString());
                }
            }
            
            Grid_PatientMain.Visibility = Visibility.Visible;
            Grid_AddMedicalAid.Visibility = Visibility.Hidden;

        }

        private void comboBox_selectMedAid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void comboBox_selectMedAid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT medAidID, Name, Description, Amount FROM MedicalAidPlan WHERE Name = @Name", con);
                    con.Open();
                    sqlCmd.Parameters.AddWithValue("@Name", comboBox_selectMedAid.Text.ToString());
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        while (sqlReader.Read())
                        {
                            textBox_medAidDescription.Text = sqlReader["Description"].ToString();
                           

                        }
                    }
                    sqlReader.Close();


                    con.Close();


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Could not populate allergies combobox from database.", ex.ToString());
                }
            }
         }

        private void populateSuburb()
        {
            comboBox_selectSuburb.Items.Clear();
            comboBox_selectSuburb.Items.Refresh();
            using (SqlConnection con = new SqlConnection(conn))
            {
                try
                {
                    SqlCommand sqlCmd = new SqlCommand("SELECT suburbName FROM Suburb WHERE cityName = @cityName", con);
                    con.Open();
                    sqlCmd.Parameters.AddWithValue("@cityName", comboBox_selectCity.Text.ToString());
                    SqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        comboBox_selectSuburb.Items.Add(sqlReader["suburbName"].ToString());

                    }
                    sqlReader.Close();


                    con.Close();


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Could not populate allergies combobox from database.", ex.ToString());
                }
            }
            comboBox_selectSuburb.Items.Refresh();
        }

        
    }

}

