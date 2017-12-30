using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeimerFlyingOnYourScreen
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            TurretTimer = new System.Timers.Timer((double)Interval);
            Player.Volume = 0.20;
            Player.MediaOpened += (sender, e) => { Dispatcher.Invoke(() => { Opacity = 1; }); };
            Player.Open(new Uri(@"Sounds\Start.mp3", UriKind.Relative));
            Player.Play();
            Opacity = 0;
            DataContext = this;
            InitializeComponent();
            TurretTimer.Elapsed += TurretTimer_Elapsed;
            TurretTimer.Start();
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            TurretTimer.Stop();
            finalClose = true;
            foreach (var item in Turrets)
            {
                item.Close();
            }
        }
        #region Fields and properties
        private bool finalClose = false;

        private int turretsLimit = 3;

        public int TurretsLimit
        {
            get { return turretsLimit; }
            set
            {
                if (value >= 0)
                {
                    turretsLimit = value;
                }
                else
                {
                    turretsLimit = 3;
                }
            }
        }

        private List<TurretWindow> Turrets { get; set; } = new List<TurretWindow>();

        private System.Timers.Timer TurretTimer;

        private decimal interval = 15000;

        public decimal Interval
        {
            get { return interval; }
            set { interval = value * 1000; TurretTimer.Interval = (double)interval; }
        }

        public double Volume
        {
            get
            {
                return Player.Volume;
            }
            set
            {
                if (Volume >= 0 && Volume <= 1.0)
                {
                    Player.Volume = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value),value,"Volume cannot be higher than 1.0 or lower than 0. Sorry for the inconvience. BUT YOU ARE DUMBBBBBBBBBBBBBBBBB");
                }
            }
        }

        public bool TurretSpawnState { get; set; } = true;

        private MediaPlayer Player { get; set; } = new MediaPlayer();
        #endregion
        #region Functions and Events
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }

        private void TurretTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (Turrets.Count < TurretsLimit)
                {
                    var t = new TurretWindow(Player.Volume) { Topmost = this.Topmost };
                    t.Closed += (secondSender, eventArgs) =>
                    {
                        if (!finalClose)
                            Turrets.Remove((TurretWindow)secondSender);
                    };
                    Turrets.Add(t);
                    t.Show();
                }
            });
        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TopMostMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            ChangeTurretTopMost(true);
        }

        private void TopMostMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            Topmost = false;
            ChangeTurretTopMost(false);
        }

        private void ChangeTurretTopMost(bool state)
        {
            foreach (var item in Turrets)
            {
                item.Topmost = state;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var source = (TextBox)sender;
            if (source.Text == String.Empty)
            {
                source.Text = "3";
                TurretsLimit = 3;
                return;
            }
            string newString = String.Empty;
            foreach (var item in source.Text)
            {
                if (char.IsNumber(item))
                {
                    newString += item;
                }
            }
            source.Text = newString;
            try
            {
                var i = int.Parse(newString);
                TurretsLimit = i;
            }
            catch (OverflowException)
            {
                source.Text = int.MaxValue.ToString();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = (ComboBox)sender;
            Interval = (s.SelectedItem as decimal?) ?? 15000;
        }

        private void CloseAllTurretsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            finalClose = true;
            foreach (var item in Turrets)
            {
                item.Close();
            }
            Turrets.Clear();
            finalClose = false;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = e.NewValue;
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            TurretTimer.Stop();
        }

        private void ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if (TurretSpawnState)
                TurretTimer.Start();
        }
        #endregion
    }
}
