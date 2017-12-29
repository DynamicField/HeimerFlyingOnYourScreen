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

namespace HeimerFlyingOnYourScreen
{
    /// <summary>
    /// Logique d'interaction pour Turret.xaml
    /// </summary>
    public partial class TurretWindow : Window
    {
        public TurretWindow(double volume = 0.5)
        {
            Player.Volume = volume;
            Player.MediaOpened += (sender, e) => { Dispatcher.Invoke(() => { Opacity = 1; }); };
            var path = $@"Sounds\Turret\TurretPlaced{Randomiser.Next(1, 3)}.wav";
            Player.Open(new Uri(path, UriKind.Relative));
            Player.Play();
            Opacity = 0;
            InitializeComponent();
            Top = Randomiser.Next(100, (int)SystemParameters.PrimaryScreenHeight - 100);
            Left = Randomiser.Next(100, (int)SystemParameters.PrimaryScreenWidth - 100);
        }
        private MediaPlayer Player = new MediaPlayer();
        private Random Randomiser = new Random(DateTime.Now.Millisecond);

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
