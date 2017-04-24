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

        
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            new AddPlayer().Show();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private async void afegirJugador(Player[] players)
        {
            ListViewItem itm;
            var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
            var child = firebase.Child("Players");
            var dinos = firebase
                .Child("Players")
                .AsObservable<Player>().AsObservableCollection();

            foreach (var player in dinos)
            {
               
            }
        }

    }
}
