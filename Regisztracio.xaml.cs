using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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

namespace jatek
{
    /// <summary>
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
        }

        private void regisztral_Click(object sender, RoutedEventArgs e)
        {
            string nev=felhasznalo.Text;
            string jelszos = jelszo.Password;
            string jelszo2 = jelszoismet.Password;
            string emails = email.Text;
            if(string.IsNullOrEmpty(nev) || string.IsNullOrEmpty(jelszos)|| string.IsNullOrEmpty(emails))
            {
                MessageBox.Show("Kérlek, tölts ki minden mezőt!","Hiányzó adat!",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            if(jelszos!= jelszo2)
            {
                MessageBox.Show("A jelszavak nem egyeznek!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(!emails.Contains("@")|| !emails.Contains("."))
            {
                MessageBox.Show("Hibás e-mail cím!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using var con = new MySql.Data.MySqlClient.MySqlConnection(App.ConnStr);
                con.Open();
                string sql = "select count(*) from felhasznalo where fnev=@nev or gmail=@mails";
                var checkCmd=new MySql.Data.MySqlClient.MySqlCommand(sql,con);
                checkCmd.Parameters.AddWithValue("@nev",nev);
                checkCmd.Parameters.AddWithValue("@mails", emails);
                int db=(int)checkCmd.ExecuteScalar();
                if (db > 0)
                {
                    MessageBox.Show("Ilyen felhasználó vagy email már létezik!", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


            }
            catch (Exception ex) 
            {
                MessageBox.Show("Hiba történt az adatbáziskapcsolat során:\n" + ex.Message, "Adatbázis hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
