using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;


namespace GameOfLife
{
    internal class Animation
    {
        FileControl fileControl = new FileControl();

        private const int size = 10;
        private const int space = 2;

        public bool[,]? cells;
        int[,]? neighbours;
        bool[,]? changed;

        public void Main(Canvas MyCanvas, int rows, int columns)
        {
            changed = new bool[columns, rows];
            if (neighbours == null) neighbours = new int[columns, rows];
            DrawTable(MyCanvas, rows, columns, cells);
            neighbours = CheckLives(cells);
            CombineWithNeigbours(ref cells, neighbours, ref changed);
            DrawTable(MyCanvas, rows, columns, cells);
            
        }

        public void DrawTable(Canvas MyCanvas, int rows, int columns, bool[,] cells)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    //ustvari kvadrat
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size,
                        Width = size,
                    };

                    rectangle.MouseDown += Rectangle_MouseDown;

                    //pobarvaj kvadrat
                    if (cells[i, j] && changed[i,j])
                    {
                        MyCanvas.Children.Remove(MyCanvas.Children[j * columns + i]);
                        rectangle.Fill = Brushes.Red;
                        MyCanvas.Children.Insert(j * columns + i, rectangle);
                    }
                    else if (changed[i,j])
                    {
                        MyCanvas.Children.Remove(MyCanvas.Children[j * columns + i]);
                        rectangle.Fill = Brushes.Green;
                        MyCanvas.Children.Insert(j * columns + i, rectangle);
                    }

                    //dodaj ga na kanvas
                    

                    //uredi presledke
                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
            }
        }

        public void ImportTable(Canvas MyCanvas, int rows, int columns, string path)
        {
            MyCanvas.Children.Clear();
            cells = new bool[columns, rows];
            changed = new bool[columns, rows];

            fileControl.ReadImportedFile(path, ref cells, ref changed);

            for (int j = 0; j < cells.GetLength(1); j++)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    //ustvari kvadrat
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size,
                        Width = size,
                    };

                    rectangle.MouseDown += Rectangle_MouseDown;

                    //pobarvaj kvadrat
                    if (cells[i, j])
                    {
                        rectangle.Fill = Brushes.Red;
                    }
                    else
                    {
                        rectangle.Fill = Brushes.Green;
                    }

                    //dodaj ga na kanvas
                    MyCanvas.Children.Add(rectangle);

                    //uredi presledke
                    Canvas.SetLeft(rectangle, i * (size + space));
                    Canvas.SetTop(rectangle, j * (size + space));
                }
            }
        }

        public void ClearTable(Canvas MyCanvas, int rows, int columns)
        {
            MyCanvas.Children.Clear();
            cells = new bool[columns, rows];

            for (int j = 0; j < cells.GetLength(1); j++)
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    //ustvari kvadrat
                    Rectangle rectangle = new Rectangle
                    {
                        Height = size,
                        Width = size,
                    };

                    rectangle.MouseDown += Rectangle_MouseDown;

                    //pobarvaj kvadrat
                    rectangle.Fill = Brushes.Green;


                    //dodaj ga na kanvas
                    MyCanvas.Children.Add(rectangle);

                    //uredi presledke
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

        static void CombineWithNeigbours(ref bool[,] cells, int[,] neighbours, ref bool[,] changed)
        {
            //Zamenjaj stanje celice, če je potrebno
            for (int x = 0; x < neighbours.GetLength(0); x++)
            {
                for (int y = 0; y < neighbours.GetLength(1); y++)
                {
                    if (neighbours[x, y] < 2 && cells[x, y])
                    {
                        cells[x, y] = false;
                        changed[x, y] = true;
                    }
                    if (neighbours[x, y] > 3 && cells[x, y])
                    {
                        cells[x, y] = false;
                        changed[x, y] = true;
                    }
                    if (neighbours[x, y] == 3 && cells[x, y] == false)
                    {
                        cells[x, y] = true;
                        changed[x, y] = true;
                    }
                }
            }
        }

        public void CheckChanged(ref bool[,] changed, int[,] neighbours)
        {
            for(int x = 0; x < changed.GetLength(0); x++)
            {
                for(int y= 0; y < changed.GetLength(1); y++)
                {
                    if (neighbours[x, y] < 2 && cells[x, y])
                        changed[x, y] = true;
                    if (neighbours[x, y] > 3 && cells[x, y])
                        changed[x, y] = true;
                    if(neighbours[x, y] == 3 && cells[x, y] == false)
                        changed[x, y] = true;
                }
            }
        }

        static int[,] CheckLives(bool[,] cells)
        {
            int[,] neighbours = new int[cells.GetLength(0), cells.GetLength(1)];
            int neighbourAlive = 0;
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    //Preveri celice okoli

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2;j++)
                        {
                            if ((i != 0 || j != 0) && x + i > -1 && y + j > -1 && x + i < cells.GetLength(0) && y + j < cells.GetLength(1))
                                if (cells[x + i, y + j])
                                    neighbourAlive++;
                        }
                    }

                    neighbours[x, y] = neighbourAlive;
                    neighbourAlive = 0;
                }
            }

            return neighbours;
        }

    }
}
