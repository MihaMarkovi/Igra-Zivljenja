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

        public int[] ReturnImportedRowColumns(string path)
        {
            char[] tempRow;
            int columns = 0, rows = 0;

            //preštej število vrstic
            StreamReader sr = File.OpenText(path);
            string tempString = sr.ReadToEnd();
            tempRow = tempString.ToCharArray();
            foreach (char c in tempRow)
            {
                if (c == ';')
                {
                    columns++;
                }
            }
            sr.Close();

            //preštej število stolpcev
            sr = File.OpenText(path);
            tempString = sr.ReadLine();
            tempRow = tempString.ToCharArray();
            foreach (char c in tempRow)
            {
                if (c == '1' || c == '0')
                    rows++;
            }
            sr.Close();

            int[] result = { columns, rows };
            return result;
        }

        public void ReadImportedFile(string path, ref bool[,] cells, ref bool[,] changed)
        {
            char[] tempRow;
            int columns = 0, rows = 0;

            //preštej število vrstic
            StreamReader sr = File.OpenText(path);
            string tempString = sr.ReadToEnd();
            tempRow = tempString.ToCharArray();
            foreach(char c in tempRow)
            {
                if(c == ';')
                {
                    columns++;
                }
            }
            sr.Close();

            //preštej število stolpcev
            sr = File.OpenText(path);
            tempString = sr.ReadLine();
            tempRow = tempString.ToCharArray();
            foreach(char c in tempRow)
            {
                if(c == '1' || c == '0')
                    rows++;
            }
            sr.Close();

            changed = new bool[columns, rows];
            cells = new bool[columns, rows];

            sr = File.OpenText(path);
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                string row = sr.ReadLine();
                tempRow = row.ToCharArray();
                for(int j = 0; j < tempRow.Length; j++)
                {
                    if (tempRow[j] == '0')
                        cells[j, i] = false;
                    else if (tempRow[j] == '1')
                        cells[j, i] = true;
                }

            }
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
                        if(grid[j,i] == true )
                            newFile.Write("1");
                        else
                            newFile.Write("0");
                    }
                    newFile.Write(';');
                    newFile.WriteLine();
                }
                newFile.Close();
            }
        }
    }
}
