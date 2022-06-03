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
using System.Threading;

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
                animation.ClearTable(MyCanvas, int.Parse(RowsText.Text), int.Parse(ColumnText.Text));
            }
            catch
            {
                MyCanvas.Children.Clear();
            }

        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmButton.Content.ToString() == "Začni animacijo")
            {
                ConfirmButton.Content = "Ustavi animacijo";
                async Task PutTaskDelay()
                {
                    int x=100;
                    switch (FPS.SelectedIndex.ToString())
                    {
                        case "0":
                            x = 100;
                            break;
                        case "1":
                            x = 200;
                            break;
                        case "2":
                            x = 1000;
                            break;
                        case "3":
                            x = 2000;
                            break;
                        case "4":
                            x = 4000;
                            break;
                    }
                    await Task.Delay(x);
                }
                while (ConfirmButton.Content.ToString() == "Ustavi animacijo")
                {
                    await PutTaskDelay();
                    animation.Main(MyCanvas, int.Parse(RowsText.Text), int.Parse(ColumnText.Text));
                }
            }
            else
            {
                ConfirmButton.Content = "Začni animacijo";
            }
        }
    }
}
