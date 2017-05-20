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
            Player[] players = new Player[listView.Items.Count];
            listView.Items.CopyTo(players, 0);

            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var child = firebase.Child("Games");

            if (listView1.HasItems)
            {
                MessageBox.Show("Ya s'ha començat un torneig");
            }
            else
            {
                for (int i = 0; i < listView.Items.Count; ++i)
                {
                    for (int j = 0; j < listView.Items.Count; ++j)
                    {
                        if (j > i)
                        {
                            Partit partit = new Partit(players[i].id, players[i].nom, players[j].id, players[j].nom);

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

        private async void  puntuacio_Click(object sender, RoutedEventArgs e)
        {
            
            Partit partit = (Partit)listView1.SelectedItem;

            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");

            int x = 0;
            int y = 0;
            if(Int32.TryParse(jugador1.Text, out x) && Int32.TryParse(jugador1.Text, out y) && partit != null)
            {
                partit.punts1 = x;
                partit.punts2 = y;

                partit.setGanador();

                if (partit.nGuanyador != "")
                {
                    var winnerChild = firebase.Child("Players/" + partit.idGuanyador);
                    var looserChild = firebase.Child("Players/" + partit.getLooser());
                   

                    var winner = await winnerChild.OnceSingleAsync<Player>();
                    var looser = await winnerChild.OnceSingleAsync<Player>();

                    winner.punts += 3;
                    winner.partitsJugats += 1;
                    looser.partitsJugats += 1;

                    await winnerChild.PutAsync<Player>(winner);
                    await looserChild.PutAsync<Player>(looser);
                    
                } else //aqui es empat
                {
                    var jugador1Child = firebase.Child("Players/" + partit.jugador1);
                    var jugador2Child = firebase.Child("Players/" + partit.jugador2);

                    var j1 = await jugador1Child.OnceSingleAsync<Player>();
                    var j2 = await jugador2Child.OnceSingleAsync<Player>();

                    j1.partitsJugats += 1;
                    j2.partitsJugats += 1;

                    await jugador1Child.PutAsync<Player>(j1);
                    await jugador2Child.PutAsync<Player>(j2);
                }
                var partitChild = firebase.Child("Games/" + partit.id);
                await partitChild.PutAsync<Partit>(partit);

                listTournament();
            }
            else
            {
                MessageBox.Show("La puntiació no es un número o no has selecionat partit");
            }


        }

        private void deleteTournament_Click(object sender, RoutedEventArgs e)
        {
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            firebase.Child("").DeleteAsync();
            llistarJugadors();
            listTournament();
        }
    }
}
