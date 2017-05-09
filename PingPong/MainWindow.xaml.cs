using Firebase.Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PingPong
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            llistarJugadors();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new AddPlayer().Show();
        }


        private void button2_Click(object sender, RoutedEventArgs e)
        {
            new AddPlayer().Show();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private async void llistarJugadors()
        {
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var dinos = await firebase
                .Child("Players").OnceAsync<Player>();
            
            foreach (var player in dinos)
            {
                Player p = new Player(player.Object.nom, player.Object.fotoPath);
                p.punts = player.Object.punts;
                p.partitsJugats = player.Object.partitsJugats;
                player.Object.id = player.Key;

                listView.Items.Add(p);
            }           
        }

        private async void esborrarJugadors()
        {
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var players = listView.SelectedItems;

            foreach (Player player in players)
            {
                MessageBox.Show(player.id);
                var child = firebase.Child("Players/" + player.id);

                await child.DeleteAsync();
               
            }
            llistarJugadors();
        }
    }
}
