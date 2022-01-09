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
using System.Text.RegularExpressions;

namespace ProjectCyclistsWPF
{
    /// <summary>
    /// Logique d'interaction pour globalcalendar.xaml
    /// </summary>
    public partial class globalcalendar : Window
    {
        private int numbcli;
        private bool vttbool;
        private bool cyclobool;
        private int vttcheck;
        private int cyclocheck;
        private String strRegexNumb;
        private Regex regexrule;
        public globalcalendar(int idcli)
        {
            numbcli = idcli;
            strRegexNumb = @"[0-9]";
            regexrule = new Regex(strRegexNumb);
            InitializeComponent();
            binddatagrid();
            unHide();
        }
        private void binddatagrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString;
            connection.Open();
            SqlCommand datagridbinding = new SqlCommand();
            Checkcats();
            if (vttbool)
            {
                if (cyclobool)
                {
                    string commandotext = $"SELECT * FROM Ride inner JOIN Category ON Category.IdCat=Ride.IdCatRide WHERE IdCatRide In(1,2)";
                    datagridbinding.CommandText = commandotext;
                }
                else
                {
                    datagridbinding.CommandText = $"SELECT * FROM Ride INNER JOIN Category ON Category.IdCat=Ride.IdCatRide WHERE IdCatRide = 1 ";
                }
            }
            else
            {
                if (cyclobool)
                {
                    datagridbinding.CommandText = $"SELECT * FROM Ride  INNER JOIN Category ON Category.IdCat=Ride.IdCatRide WHERE IdCatRide = 2";
                }
            }
            datagridbinding.Connection = connection;
            SqlDataAdapter dataadapter = new SqlDataAdapter(datagridbinding);
            DataTable datatable = new DataTable("Ride");
            dataadapter.Fill(datatable);
            Ride.ItemsSource = datatable.DefaultView;

        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Calendar dashboard = new Calendar(numbcli);
            dashboard.Show();
            this.Close();
        }

        private void Checkcats()
        {
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
        }

        private void AddRide_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                {
                    String insertcar = $"INSERT INTO Ride(DeparturePlace,DepartureDate,DepartureHour,RidePrice,IdCatRide,MaxClient,Bike,CurrentClient) VALUES (@place,@date,@hour,@price,@idride,@maxclient,0,0)";
                    SqlCommand sqlinsert = new SqlCommand(insertcar, connection);
                    sqlinsert.CommandType = CommandType.Text;
                    sqlinsert.Parameters.AddWithValue("@place", DepPlace.Text);
                    sqlinsert.Parameters.AddWithValue("@date", DepDate.Text);
                    sqlinsert.Parameters.AddWithValue("@hour", DepHour.Text);
                    sqlinsert.Parameters.AddWithValue("@price", RidePrice.Text);
                    sqlinsert.Parameters.AddWithValue("@idride", CatRide.Text);
                    sqlinsert.Parameters.AddWithValue("@maxclient", MaxClient.Text);
                    connection.Open();
                    sqlinsert.ExecuteNonQuery();
                    connection.Close();
                    binddatagrid();
                }
            }
        }

        private void RideDel_Click(object sender, RoutedEventArgs e)
        {
            if (!(DeleteRideId.Text == String.Empty))
            {
                Match matchdelete = regexrule.Match(DeleteRideId.Text);
                if (matchdelete.Success)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                    {
                        try
                        {
                            String deleteride = $"DELETE FROM Ride WHERE IdRide=@idride";
                            SqlCommand sqldelete = new SqlCommand(deleteride, connection);
                            connection.Open();
                            sqldelete.Parameters.AddWithValue("@idride", DeleteRideId.Text);
                            sqldelete.ExecuteNonQuery();
                            MessageBox.Show("Deleted your Ride with id number " + DeleteRideId.Text);
                            connection.Close();
                            binddatagrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Pick a good Id");
                }
            }
        }
        private void visible()
        {
            l1.Visibility = Visibility.Visible;
            DepPlace.Visibility = Visibility.Visible;

            l2.Visibility = Visibility.Visible;
            DepDate.Visibility = Visibility.Visible;

            l3.Visibility = Visibility.Visible;
            DepHour.Visibility = Visibility.Visible;    

            l4.Visibility = Visibility.Visible;
            RidePrice.Visibility = Visibility.Visible;

            l5.Visibility = Visibility.Visible;
            CatRide.Visibility=Visibility.Visible;
            
            l7.Visibility = Visibility.Visible;
            MaxClient.Visibility = Visibility.Visible;

            AddRide.Visibility = Visibility.Visible;

            l6.Visibility = Visibility.Visible;
            DeleteRideId.Visibility = Visibility.Visible;
            RideDel.Visibility=Visibility.Visible;
        }
        private void unHide()
        {
            int idresp;
            idresp=Checkresponsable(numbcli);
            if(idresp==1)
            {
               visible();
                CatRide.Text = "1";
                CatRide.IsReadOnly = true;
            }
            else
            {
                if(idresp==2)
                {
                    visible();
                    CatRide.Text = "2";
                    CatRide.IsReadOnly = true;

                }
            }
        }
        private int Checkresponsable(int checkresp)
        {
            vttbool = false;
            cyclobool = false;
            vttcheck = 0;
            ///checkresp est egal au numero du client
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    ///on recherche si l'id du client est dans la table
                    String querysearch = $"SELECT * FROM Category WHERE IdCatResp={checkresp} ";
                    SqlCommand search = new SqlCommand(querysearch, connection);
                    using (SqlDataReader reader = search.ExecuteReader())
                    {
                            while (reader.Read())
                            {
                                if((vttcheck=reader.GetInt32(2))==1)
                                {
                                    vttbool = true;
                                    break;
                                }
                                if((vttcheck=reader.GetInt32(2))==2)
                                {
                                    cyclobool = true;
                                    break;
                                }
                            }
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
            return vttcheck;
        }

        private bool isValid()
        {
            Boolean tester;
            tester = false;

            if (DepPlace.Text == String.Empty)
            {
                MessageBox.Show("Please enter a Departure Place");
            }
            else
            {
                if (DepDate.Text == String.Empty)
                {
                    MessageBox.Show("Please enter a Departure Date");
                }
                else
                {
                        if (DepHour.Text == String.Empty)
                        {
                            MessageBox.Show("Please enter the Departure Hour");
                        }
                        else
                        {
                            if (RidePrice.Text == String.Empty)
                            {
                                MessageBox.Show("Please enter the number of the Ride Category, even if it's 0");
                            }
                            else
                            {
                                if (CatRide.Text == String.Empty)
                                {
                                    MessageBox.Show("Please enter the number of the Ride Category, even if it's 0");
                                }
                                else
                                {
                                    tester = true;
                                }
                            }
                        }
                }
            }
            if(tester)
            {
                return true;
            }
            else
                return false;
            
        }

        private void AddPersonalRide_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
            {
                String insert = $"INSERT INTO LinkRide(IdCliRide,IdRide) VALUES ({numbcli},@idRide)";
                SqlCommand sqlinsert = new SqlCommand(insert, connection);
                connection.Open();
                sqlinsert.Parameters.AddWithValue("@idRide", AddPersonalRideId.Text);
                sqlinsert.ExecuteNonQuery();
                MessageBox.Show("The Ride was added");
                connection.Close();
            }
        }
    }
}
