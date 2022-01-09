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
    /// Logique d'interaction pour LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        
        
        public LoginScreen()
        {
            InitializeComponent();
        }
        
        private void btnSubmitLogin_Click(object sender, RoutedEventArgs e)
        {
            
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    String querylogin = "SELECT * FROM Clients WHERE ClientLogin like @ClientLogin AND Password like @Password";
                    SqlCommand sqlcmdlogin = new SqlCommand(querylogin, connection);
                    sqlcmdlogin.CommandType = CommandType.Text;
                    sqlcmdlogin.Parameters.AddWithValue("@ClientLogin", txtUsername.Text);
                    sqlcmdlogin.Parameters.AddWithValue("@Password", txtPassword.Password);
                    int idclient = Convert.ToInt32(sqlcmdlogin.ExecuteScalar());
                    if(idclient > 0)
                    {
                        using (SqlDataReader reader = sqlcmdlogin.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MainWindow dashboard = new MainWindow(idclient);
                                dashboard.Show();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username or Password is incorrect");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            
        }
    }
}
