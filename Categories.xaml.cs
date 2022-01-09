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
    /// Logique d'interaction pour Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        private int numbcli;
        private int vttcheck;
        private int cyclocheck;
        private int CatCount;
        private Boolean vttbool;
        private Boolean cyclobool;
        public Categories(int idcli)
        {
            numbcli = idcli;
            DataLoader();
            
        }
        private void DataLoader()
        {
            
            vttcheck = 0;
            cyclocheck = 0;
            vttbool = false;
            cyclobool = false;
            InitializeComponent();
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
                            VttCheck.IsChecked = true;
                            vttbool = true;
                        }
                        if ((cyclocheck = readclientcat.GetInt32(2)) == 2)
                        {

                            CycloCheck.IsChecked = true;
                            cyclobool = true;
                        }
                    }
                }
                connection.Close();
            }
        }

        private void ChangeCat_Click(object sender, RoutedEventArgs e)
        {
           
            CatCount=0;
            if(VttCheck.IsChecked == true)
            {
                ++CatCount;
            }
            if(CycloCheck.IsChecked == true)
            {
                ++CatCount;
            }
            if(CatCount ==0)
            {
                MessageBox.Show("You need to have at least 1 Cat checked");
            }
            else
            {
                if (vttbool == true)
                {
                    if (VttCheck.IsChecked == false)
                    {
                        //search for line and delete line in DB
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                        {
                            String deletevtt = $"DELETE FROM LinkCat WHERE IdCliCat={numbcli} AND IdCatLink=1";
                            SqlCommand sqldelete = new SqlCommand(deletevtt, connection);
                            connection.Open();
                            sqldelete.ExecuteNonQuery();
                            MessageBox.Show("Deleted your Vtt category");
                            connection.Close();
                            DataLoader();
                        }
                    }
                }
                else
                {
                    if (VttCheck.IsChecked == true)
                    {
                        //search for line and Add line in DB
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                        {
                            String insertvtt = $"INSERT INTO LinkCat(IdCliCat,IdCatLink) VALUES ({numbcli},1)";
                            SqlCommand sqlinsert = new SqlCommand(insertvtt, connection);
                            connection.Open();
                            sqlinsert.ExecuteNonQuery();
                            MessageBox.Show("Added the Vtt Category");
                            connection.Close();
                            DataLoader();
                        }
                    }
                }
                if (cyclobool == true)
                {
                    if (CycloCheck.IsChecked == false)
                    {
                        //search for line and delete line in DB
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                        {
                            String deletecyclo = $"DELETE FROM LinkCat WHERE IdCliCat={numbcli} AND IdCatLink=2";
                            SqlCommand sqldelete = new SqlCommand(deletecyclo, connection);
                            connection.Open();
                            sqldelete.ExecuteNonQuery();
                            MessageBox.Show("Deleted your Cyclo category");
                            connection.Close();
                            DataLoader();
                        }
                    }
                    //dont do anything
                }
                else
                {
                    if (CycloCheck.IsChecked == true)
                    {
                        
                        //add line in DB
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CyclingDB"].ConnectionString))
                        {
                            String insertcyclo = $"INSERT INTO LinkCat(IdCliCat,IdCatLink) VALUES ({numbcli},2)";
                            SqlCommand sqlinsert = new SqlCommand(insertcyclo, connection);
                            connection.Open();
                            sqlinsert.ExecuteNonQuery();
                            MessageBox.Show("Added the Cyclo Category");
                            connection.Close();
                            DataLoader();
                        }
                    }
                }
            }
        }
        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            CatCount = 0;
            if (VttCheck.IsChecked == true)
            {
                ++CatCount;
            }
            if (CycloCheck.IsChecked == true)
            {
                ++CatCount;
            }
            if (CatCount == 0)
            {
                MessageBox.Show("You need to have at least 1 Cat checked");
            }
            else
            {
                MainWindow dashboard = new MainWindow(numbcli);
                dashboard.Show();
                this.Close();
            }
            
        }
    }
}
