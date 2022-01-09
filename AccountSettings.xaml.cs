using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Configuration;
using System.Collections.Specialized;

namespace ProjectCyclistsWPF
{
    /// <summary>
    /// Logique d'interaction pour AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : Window
    {
        private int numbcli;
        private String ClientNameStr;
        private String LastNameStr;
        private String TelNumberStr;
        public AccountSettings(int idcli)
        {
            numbcli = idcli;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {
                String queryretrieveinfo = $"SELECT * FROM Clients WHERE IdClient={numbcli}";
                SqlCommand sqlcmdretrieve = new SqlCommand(queryretrieveinfo, connection);
                connection.Open();
                using (SqlDataReader readclientinfo = sqlcmdretrieve.ExecuteReader())
                {
                    while (readclientinfo.Read())
                    {
                        ///indice tableau 
                        ClientNameStr = readclientinfo.GetString(2);
                        LastNameStr = readclientinfo.GetString(3);
                        TelNumberStr = readclientinfo.GetString(4);
                    }
                }
            }
            InitializeComponent();
        }

        private void ModifyClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                {
                    connection.Open();
                    String querymodifyclient = $"Update Clients set FirstName=@firstname , LastName=@lastname , Tel=@tel where IdClient = {numbcli}";
                    using (SqlCommand sqlcmdupdate = new SqlCommand(querymodifyclient, connection))
                    {
                        sqlcmdupdate.Parameters.AddWithValue("@firstname", FirstName.Text);
                        sqlcmdupdate.Parameters.AddWithValue("@lastname", LastName.Text);
                        sqlcmdupdate.Parameters.AddWithValue("@tel", Tel.Text);
                        int rows = sqlcmdupdate.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Your Modifications have been taken into account");
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(numbcli);
            dashboard.Show();
            this.Close();
        }

        private void FirstName_Initialized(object sender, EventArgs e)
        {
            FirstName.Text = ClientNameStr;
        }

        private void LastName_Initialized(object sender, EventArgs e)
        {
            LastName.Text = LastNameStr;
        }

        private void Tel_Initialized(object sender, EventArgs e)
        {
            Tel.Text = TelNumberStr;
        }
    }
}
