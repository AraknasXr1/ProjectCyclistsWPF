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
    /// Logique d'interaction pour Payments.xaml
    /// </summary>
    public partial class Payments : Window
    {
        private int numbcli;
        private int vttcheck;
        private int cyclocheck;
        private Boolean vttbool;
        private Boolean cyclobool;
        private int Counter;
        public Payments(int idcli)
        {
            numbcli = idcli;
            Counter = 0;
            Counter =CountPayDay(Counter);
            InitializeComponent();
            Counter = CountTrips(Counter);
            Counter = CountCars(Counter);
            payupdate();
            binddatagrid();
            PayDay.Text = $"{Counter}";
            if(numbcli==3)
            {
                PayGrid.Visibility = Visibility.Visible;
            }
        }
        private void binddatagrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString;
            connection.Open();
            SqlCommand datagridbinding = new SqlCommand();
            datagridbinding.CommandText = $"SELECT FirstName, LastName, Wallet FROM Clients";
            datagridbinding.Connection = connection;
            SqlDataAdapter dataadapter = new SqlDataAdapter(datagridbinding);
            DataTable datatable = new DataTable("Clients");
            dataadapter.Fill(datatable);
            PayGrid.ItemsSource = datatable.DefaultView;
        }

        private void payupdate()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                {
                    connection.Open();
                    String querymodifyclient = $"Update Clients set Wallet={Counter} where IdClient = {numbcli}";
                    using (SqlCommand sqlcmdupdate = new SqlCommand(querymodifyclient, connection))
                    {
                        int rows = sqlcmdupdate.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
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
        private int CountCars(int minus)
        {
            int subs = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {

                string commandotext = $"select * From LinkCarToRide lcr inner join Car ca on lcr.IdCar = ca.IdCar inner join Ride r on lcr.IdLinkRide = r.IdRide where idCarClient = {numbcli}";
                SqlCommand sqlcmdretrieve = new SqlCommand(commandotext, connection);
                connection.Open();
                using (SqlDataReader readclientinfo = sqlcmdretrieve.ExecuteReader())
                {
                    while (readclientinfo.Read())
                    {
                        ///indice tableau 
                        subs = (readclientinfo.GetInt32(5))*(readclientinfo.GetInt32(12));
                        minus -= subs;
                    }
                }
            }
            return minus;
        }

        private int CountTrips(int zero)
        {
            int subs = 0;
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                {
                    
                    string commandotext = $"SELECT * FROM Ride r inner join LinkRide l on r.IdRide = L.IdRide inner join Category ON Category.IdCat=r.IdCatRide WHERE IdCatRide In(1,2) and IdCliRide={numbcli}";
                    SqlCommand sqlcmdretrieve = new SqlCommand(commandotext, connection);
                    connection.Open();
                    using (SqlDataReader readclientinfo = sqlcmdretrieve.ExecuteReader())
                    {
                        while (readclientinfo.Read())
                        {
                            ///indice tableau 
                            subs = readclientinfo.GetInt32(5);
                            zero += subs;
                        }
                    }
                }
            return zero;
        }
            private int CountPayDay(int zero)
        {
            vttcheck = 0;
            cyclocheck = 0;
            vttbool = false;
            cyclobool = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {
                String queryretrieveinfo = $"SELECT * FROM LinkCat WHERE IdCliCat={numbcli}";
                SqlCommand sqlcmdretrieve = new SqlCommand(queryretrieveinfo, connection);
                connection.Open();
                using (SqlDataReader readclientcat = sqlcmdretrieve.ExecuteReader())
                {
                    while (readclientcat.Read())
                    {
                        if ((vttcheck = readclientcat.GetInt32(2)) == 1)
                        {
                            vttbool = true;
                        }
                        if ((cyclocheck = readclientcat.GetInt32(2)) == 2)
                        {
                            cyclobool = true;
                        }
                    }
                }
                connection.Close();
            }
            if(vttbool&&cyclobool)
            {
                return 25;
            }
            else
            {
                return 20;
            }

        }
    }
}
