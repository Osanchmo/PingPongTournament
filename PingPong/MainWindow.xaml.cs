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
            listTournament();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new AddPlayer().Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            esborrarJugadors();
        }

        private void updateBut_Click(object sender, RoutedEventArgs e)
        {
            llistarJugadors();
        }

        private void startTournament_Click(object sender, RoutedEventArgs e)
        {
            tournamentStart();
        }

        private async void llistarJugadors()
        {
            listView.Items.Clear();
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var dinos = await firebase
                    .Child("Players").OnceAsync<Player>();

                foreach (var player in dinos)
                {
                    Player p = new Player(player.Object.nom, player.Object.fotoPath);
                    p.punts = player.Object.punts;
                    p.partitsJugats = player.Object.partitsJugats;
                    p.id = player.Key;

                    listView.Items.Add(p);
                }

        }

        private async void esborrarJugadors()
        {
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var players = listView.SelectedItems;

            foreach (Player player in players)
            {
                MessageBox.Show(player.nom + " esborrat");
                var child = firebase.Child("Players/" + player.id);

                await child.DeleteAsync();
            }

            llistarJugadors();
        }

        private async void tournamentStart()
        {
            var players = listView.Items;
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var child = firebase.Child("Games");

            if (listView1.HasItems)
            {
                MessageBox.Show("Ya s'ha començat un torneig");
            }
            else
            {

                foreach (Player player in players)
                {
                    foreach (Player p in players)
                    {
                        if (p.id != player.id)
                        {
                            Partit partit = new Partit(p.id, p.nom, player.id, player.nom);
                            await child.PostAsync(partit);
                        }
                    }
                }
                listTournament();
            }
        }

        private async void listTournament()
        {

                var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
                var child = firebase.Child("Games");
                var dinos = await child.OnceAsync<Partit>();

                foreach (var game in dinos)
                {
                    Partit partit = game.Object;
                    partit.id = game.Key;
                    listView1.Items.Add(partit);
                }
        }

    }
}
