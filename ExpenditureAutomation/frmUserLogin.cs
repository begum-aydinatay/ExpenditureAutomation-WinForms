using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccessLayer;
using ExpenditureAutomation;

namespace ExpensesAutomation
{
    public partial class frmUserLogin : Form
    {
        public frmUserLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (PreInspection()) // hasError == true
            {
                return;
            }

            // If there are no errors, proceed with the following credential checks with the database
            SQLDataProvider provider = new SQLDataProvider(ConfigurationHelper.GetConnectionString("EADBConnectionString"));

            string query = @"SELECT * FROM Employee AS E WHERE E.Username = @Username AND E.Password = @Password AND E.isActive = 1";
            provider.Command.Parameters.AddWithValue("@Username", txtUsername.Text);
            provider.Command.Parameters.AddWithValue("@Password", txtPassword.Text);
            DataTable result = provider.GetDataTable(query);

            if (result.Rows.Count > 0)
            {
                // The login was successfully completed
                this.Hide(); // Hide the login form

                // Show the main screen
                frmMainScreen mainScreen = new frmMainScreen();
                mainScreen.ShowDialog();

                this.Close(); // Close the login form
            }
            else
            {
                MessageBox.Show("Invalid username or password...", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private bool PreInspection()
        {
            bool hasError = false;

            errorProvider1.Clear(); // If there are any errors, clear them

            string username = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                errorProvider1.SetError(txtUsername, "Username cannot be left empty...");
                hasError = true;
            }

            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(password))
            {
                errorProvider1.SetError(txtPassword, "Password cannot be left empty...");
                hasError = true;
            }

            return hasError;
        }
    }
}
