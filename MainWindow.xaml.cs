using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int index = 1;
        public static string[] paths = new string[3];
        public static string[] descriptions = new string[3];
        public static Window backgroundWindow = new Window();
        public MainWindow()
        {
            InitializeComponent();
            backgroundWindow.Visibility = Visibility.Hidden;
            backgroundWindow.Width = 300;
            backgroundWindow.Height = 170;
            backgroundWindow.AllowsTransparency = true;
            backgroundWindow.Background = Brushes.Red;
            backgroundWindow.WindowStyle = WindowStyle.None;

            var open = new Button();
            var image = new Image();
            var bitmap = new BitmapImage(new Uri(@"pack://application:,,,/Images/monkey_headphones.jpg"));
            image.Source = bitmap;
            open.Content = image;
            
            open.Click += new RoutedEventHandler(Show);
            backgroundWindow.Content = open;
            backgroundWindow.WindowStartupLocation = WindowStartupLocation.Manual;
            backgroundWindow.Top = 0;
            backgroundWindow.Left = 0;
            
            //Paths to directories
            paths[0] = @"C:\Users\V-SHARA\Videos\Anime\One Piece";
            paths[1] = @"C:\Users\V-SHARA\Videos\Anime\Dragonball Super";
            paths[2] = @"C:\Users\V-SHARA\Videos\Anime\Tokyo Ghoul re";

            //Descriptions
            descriptions[0] = "One Piece\nAction, Adventure\n\nFollows the adventures of Monkey D. Luffy and his friends in order to find the greatest treasure ever left by the legendary Pirate, Gol D. Roger. The famous mystery treasure named 'One Piece'";
            descriptions[1] = "Dragon Ball Super\nAction, Adventure\n\nThe continuing  adventures of the mighty warrior Son Goku 'Yes thats his full name' as he encounters new worlds and new warriors to fight. ";
            descriptions[2] = "Tokyo Ghoul:re\nAction\n\nA Tokyo College student is Attacked by a ghoul, a superpowered human who ... He survives, but has become part ghoul and becomes a fugitive on the run";

            Next(index);
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            this.Show();
            backgroundWindow.Visibility = Visibility.Hidden;
        }
        private void Next_buttonClick(object sender, RoutedEventArgs e)
        {
            var next = index + 1;
            index = next == 4 ? 1 : next;
            Next(index);
        }

        private void Back_buttonClick(object sender, RoutedEventArgs e)
        {
            var previous = index - 1;
            index = previous == 0 ? 3 : previous;
            Next(index);
            Tb_Description.Text = ChangeDescription(index);
        }

        private void Open_buttonClick(object sender, RoutedEventArgs e)
        {
        }

        private void HideAll()
        {
            OP.Visibility = Visibility.Hidden;
            DBZ.Visibility = Visibility.Hidden;
            TG.Visibility = Visibility.Hidden;
            Music.Visibility = Visibility.Hidden;
        }

        private void playingMusic(string filename)
        {
            if (filename.EndsWith("mp3"))
            {
                var x = MessageBox.Show("play music in background?", "", MessageBoxButton.YesNo);
                if (x == MessageBoxResult.Yes)
                {
                    this.Hide();
                    backgroundWindow.Visibility = Visibility.Visible;
                }
                else if (x == MessageBoxResult.No)
                {
                    this.WindowState = WindowState.Minimized;
                    mediaelement.Visibility = Visibility.Hidden;
                    Music.Visibility = Visibility.Visible;

                }
            }
        }
        private void Next(int number)
        {
            HideAll();
            mediaelement.Stop();
            PlayerControls.Visibility = Visibility.Hidden;
            mediaelement.Visibility = Visibility.Hidden;
            ImageButton.Visibility = Visibility.Visible;

            switch (number)
            {
                case 1: OP.Visibility = Visibility.Visible; break;
                case 2: DBZ.Visibility = Visibility.Visible; break;
                case 3: TG.Visibility = Visibility.Visible; break;
                case 4: Music.Visibility = Visibility.Visible; break;
            }

            Tb_Description.Text = ChangeDescription(number);
        }

        private string ChangeDescription(int number)
        {
            return descriptions[number - 1];
        }

        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = paths[index - 1];
            fileDialog.ShowDialog();

            try
            {
                mediaelement.Source = new Uri(fileDialog.FileName);
                HideAll();
                ImageButton.Visibility = Visibility.Hidden;
                mediaelement.Visibility = Visibility.Visible;

                var filename = fileDialog.FileName;
                playingMusic(filename);
                mediaelement.Play();
                PlayerControls.Visibility = Visibility.Visible;
            }
            catch (UriFormatException uriException)
            {
                MessageBox.Show("Please select a file next time 'BAKA'");
            }
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimize_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Stop();
            mediaelement.Play();
            Thread.Sleep(100);
            mediaelement.Pause();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Pause();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            mediaelement.Play();
        }

        private void writeToConsole(string text)
        {
            //foreach (Window win in App.Current.Windows)
            //{
            //    if(win.GetType() == typeof(background))
            //        {
            //            win.
            //        }
            //}
        }

        private void Dragger_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}