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

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Animation animation = new Animation();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void DrawTable_TextChanged(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Izbral si { this.RowsText.Text} vrstic in {this.ColumnText.Text} stolpcev");
            try
            {
                animation.DrawTable(MyCanvas, int.Parse(RowsText.Text), int.Parse(ColumnText.Text));
            }
            catch
            {
                MyCanvas.Children.Clear();
            }

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
