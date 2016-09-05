using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ArithmeticOfLongNumbers.Model;

namespace ArithmeticOfLongNumbers
{
    public class FileWork
    {
        private string[] allLinesFile;
        private string nameTxtFile { get; set; }

        public string FullPathNameTxtFile { get; set; }

        public string[] GetAllLinesFile
        {
            get { return allLinesFile; }
            private set { allLinesFile = value; }
        }

        public void OpenFile ()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                FullPathNameTxtFile = openFileDialog.FileName;
                nameTxtFile = Path.GetFileNameWithoutExtension(FullPathNameTxtFile);
                
            }
        }


        public void SaveFile(string[] answer)
        {
            string newFile = Directory.GetCurrentDirectory() + "\\Answer_" + nameTxtFile + ".txt";
            try
            {
                File.WriteAllLines(newFile, answer, System.Text.Encoding.Default);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message + ". Чтение из файла не удалось");
            }
        }

        public static string[] ReadFile(string nameFile)
        {
            string[] textFile;
            try
            {
                textFile = File.ReadAllLines(nameFile, System.Text.Encoding.Default);
            }
            catch (IOException ex)
            {
                textFile = new string[] { "Чтение из файла не удалось" };
                throw new IOException(ex.Message + ". Чтение из файла не удалось");
            }
            return textFile;
        }
        
    }
}
