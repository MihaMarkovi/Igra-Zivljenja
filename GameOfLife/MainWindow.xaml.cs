using System;
using System.Threading.Tasks;
using System.Windows;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Animation animation = new Animation();
        private FileControl fileControl = new FileControl();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void DrawTable_TextChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(RowsText.Text) > 200 || int.Parse(ColumnText.Text) > 200)
                    MessageBox.Show("Vnesli ste preveliko količino stolpcev ali vrstic.");
                else
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

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            string file = fileControl.ImportFileName();
            if (file != "None")
            {
                animation.ImportTable(MyCanvas, int.Parse(RowsText.Text), int.Parse(ColumnText.Text), file);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            bool[,] grid = animation.cells;
            fileControl.ExportFile(grid);
            MessageBox.Show("Datoteka izvožena na namizje");
        }
    }
}
