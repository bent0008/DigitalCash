using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Customer
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        public string connectionString = "Data Source=BEN_T\\SQLEXPRESS;Initial Catalog=DigitalCash;Integrated Security=True";


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            bool codeExecuted = false;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string username = usernameTxtbx.Text;
            string password = passwordTxtbx.Text;
            int dbBalance = 0;
            int dbID = 0;

            string query = "SELECT [balance],[ID] FROM LoginCredentials WHERE username = @username AND password = @password";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dbBalance = reader.GetInt32(0);
                    dbID = reader.GetInt32(1);
                    codeExecuted = true;
                }
                if (codeExecuted == false)
                {
                    MessageBox.Show("Error, the username or password is invalid.", "Error");
                    usernameTxtbx.Clear();
                    passwordTxtbx.Clear();
                }
                else
                {
                    CustomerForm custForm = this.Owner as CustomerForm;

                    if (custForm != null)
                    {
                        custForm.Balance = Convert.ToInt32(dbBalance);
                        custForm.Username = username;
                        custForm.LoggedIn = true;
                        custForm.ID = Convert.ToInt32(dbID);
                        con.Close();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("The customer form was not found.", "Error");
                    }
                    con.Close();
                    this.Close();
                }
                reader.Close();
            }

        }
    }
}
