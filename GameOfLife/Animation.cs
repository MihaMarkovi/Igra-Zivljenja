using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace GameOfLife
{
    internal class Animation
    {
        private const int size = 8;
        private const int space = 2;

        bool[,]? cells; 

        public void DrawRectangles(Canvas MyCanvas, int rows, int columns)
        {
            MyCanvas.Children.Clear();
            cells = new bool[columns, rows];

            for (int j = 0; j < cells.GetLength(1); j++)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size,
                        Width = size,
                    };

                    rectangle.MouseDown += Rectangle_MouseDown;

                    rectangle.Fill = cells[i, j] ? Brushes.Red : Brushes.Green;
                    MyCanvas.Children.Add(rectangle);

                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
            }
        }

        public void SwitchState(int x, int y)
        {
            cells[x, y] = !cells[x,y];
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;

            rectangle.Fill = (rectangle.Fill == Brushes.Green) ? Brushes.Red : Brushes.Green;


            int px = Convert.ToInt32(Canvas.GetLeft(rectangle)) / (size + space);
            int py = Convert.ToInt32(Canvas.GetTop(rectangle)) / (size + space);

            if (px < cells.GetLength(0) && py < cells.GetLength(1))
                SwitchState(px, py);
        }

    }
}
