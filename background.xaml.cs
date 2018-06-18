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

namespace Demo
{
    /// <summary>
    /// Interaction logic for background.xaml
    /// </summary>
    public partial class background : Window
    {
        public static Window backgroundWindow;
        public background()
        {
            this.Owner = App.Current.MainWindow; 
            InitializeComponent();
        }
    }
}
