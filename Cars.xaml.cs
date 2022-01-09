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
    /// Logique d'interaction pour Cars.xaml
    /// </summary>
    public partial class Cars : Window
    {
        private int numbcli;
        private String strRegexNumb;
        private Regex regexrule;
        public Cars(int idcli)
        {
            strRegexNumb = @"[0-9]";
            numbcli = idcli;
            regexrule = new Regex(strRegexNumb);
            InitializeComponent();
            binddatagrid();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow dashboard = new MainWindow(numbcli);
            dashboard.Show();
            this.Close();
        }

        private void binddatagrid()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString;
            connection.Open();
            SqlCommand datagridbinding = new SqlCommand();
            datagridbinding.CommandText = $"SELECT * FROM Car WHERE IdCarClient = {numbcli}";
            datagridbinding.Connection = connection;
            SqlDataAdapter dataadapter=new SqlDataAdapter(datagridbinding);
            DataTable datatable=new DataTable("Car");
            dataadapter.Fill(datatable);
            CarGrid.ItemsSource = datatable.DefaultView;
        }


        private bool isValid()
        {
            Boolean testerbrand=false, testerSeat=false, testerBike=false;
            if(CarBrand.Text == String.Empty)
            {
                MessageBox.Show("Please enter a Car Brand");
            }
            else
            {
                testerbrand=true;
                if(CarSeat.Text == String.Empty)
                {
                    MessageBox.Show("Please enter a seat");
                }
                else
                {
                    Match matchSeat = regexrule.Match(CarSeat.Text);
                    if (matchSeat.Success )
                    {
                        testerSeat = true;
                        if (CarBikeSpace.Text == String.Empty)
                        {
                            MessageBox.Show("Please enter the number of Bike Space you have, even if it's 0");
                        }
                        else
                        {
                            Match matchBike = regexrule.Match(CarBikeSpace.Text);
                            if(matchBike.Success )
                            {
                                testerBike = true;
                            }
                            else
                            {
                                MessageBox.Show("Bike Numbers between 0 and 9 allowed");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seat Numbers between 0 and 9 allowed");
                    }
                }
            }
            if (testerbrand&&testerSeat&&testerBike)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                {
                    String insertcar = $"INSERT INTO Car(IdCarClient,CarSeat,CarBike,CarBrand) VALUES ({numbcli},@seats,@bikes,@brand)";
                    SqlCommand sqlinsert = new SqlCommand(insertcar, connection);
                    sqlinsert.CommandType = CommandType.Text;
                    sqlinsert.Parameters.AddWithValue("@seats", CarSeat.Text);
                    sqlinsert.Parameters.AddWithValue("@bikes", CarBikeSpace.Text);
                    sqlinsert.Parameters.AddWithValue("@brand", CarBrand.Text);
                    connection.Open();
                    sqlinsert.ExecuteNonQuery();
                    connection.Close();
                    binddatagrid();
                }
            }
        }

        private void UpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                if(!(CarIdUpdateNumber.Text==String.Empty))
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                    {
                        connection.Open();
                        String updatecar = $"UPDATE Car set CarSeat=@seats , CarBike=@bikes , CarBrand=@brand WHERE IdCarClient={numbcli} AND IdCar=@id";
                        using (SqlCommand sqlupdate = new SqlCommand(updatecar, connection))
                        {
                            sqlupdate.CommandType = CommandType.Text;
                            sqlupdate.Parameters.AddWithValue("@id", CarIdUpdateNumber.Text);
                            sqlupdate.Parameters.AddWithValue("@seats", CarSeat.Text);
                            sqlupdate.Parameters.AddWithValue("@bikes", CarBikeSpace.Text);
                            sqlupdate.Parameters.AddWithValue("@brand", CarBrand.Text);
                            sqlupdate.ExecuteNonQuery();
                            connection.Close();
                        }
                        binddatagrid();
                    }
                }
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if(!(DeleteCarNumber.Text==String.Empty))
            {
                Match matchdelete = regexrule.Match(DeleteCarNumber.Text);
                if (matchdelete.Success)
                {
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                    {
                        String deletecar = $"DELETE FROM Car WHERE IdCarClient={numbcli} AND IdCar=@idcar";
                        SqlCommand sqldelete = new SqlCommand(deletecar, connection);
                        connection.Open();
                        sqldelete.Parameters.AddWithValue("@idcar", DeleteCarNumber.Text);
                        sqldelete.ExecuteNonQuery();
                        MessageBox.Show("Deleted your Car with id number " + DeleteCarNumber.Text);
                        connection.Close();
                        binddatagrid();
                    }
                }
                else
                {
                    MessageBox.Show("Pick a good Id");
                }
            }
        }
    }
}
