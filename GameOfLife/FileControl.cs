using System;
using System.IO;

namespace GameOfLife
{
    internal class FileControl
    {
        public string ImportFileName()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                return filename;
            }

            return "None";
        }

        public string ReadImportedFile(string path)
        {
            StreamReader sr = File.OpenText(path);
            string row = sr.ReadLine();
            return row;
        }

        public void ExportFile(bool[,] grid)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            path =  path + "\\Izvoz.txt";

            if (!File.Exists(path))
            {
                StreamWriter newFile = File.CreateText(path);
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    for (int j = 0; j < grid.GetLength(1); j++)
                    {
                        newFile.Write(grid[j, i]);
                    }
                    newFile.WriteLine();
                }
                newFile.Close();
            }
        }
    }
}
