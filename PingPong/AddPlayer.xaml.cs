using Firebase.Database;
using Microsoft.Win32;
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

namespace PingPong
{
    public partial class AddPlayer : Window
    {
        String name;
        string path;
        public AddPlayer()
        {
            InitializeComponent();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            file.ShowDialog();

            if (file.FileName != null)
            {
                pathLabel.Content = file.FileName;
                path = file.FileName;
                image.Source = new BitmapImage(new Uri(@path, UriKind.RelativeOrAbsolute));
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            name = textBox.Text;
            if (name != null && path != null)
            {
                var player = new Player(name, path);

                afegirJugador(player);

            }
            else
            {
                MessageBox.Show("Faltan campos");
            }

        }
        private async void afegirJugador(Player player)
        {
                var firebase = new FirebaseClient("https://pingpongtournament-b0d42.firebaseio.com/");
                var child = firebase.Child("Players");
                var dino = await child.PostAsync(player);
                this.Close();

        }
    }
}
