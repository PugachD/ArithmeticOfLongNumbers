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

        public string FullPathNameTxtFile { get; set; }
        private string NameTxtFile { get; set; }

        public string[] GetAllLinesFile
        {
            get { return allLinesFile; }
            private set { allLinesFile = value; }
        }

        public void OpenFile ()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                FullPathNameTxtFile = openFileDialog.FileName;
                NameTxtFile = Path.GetFileNameWithoutExtension(FullPathNameTxtFile);
                
            }
        }


        public void SaveFile(string[] answer)
        {
            string newFile = Directory.GetCurrentDirectory() + "\\Answer_" + NameTxtFile + ".txt";
            try
            {
                File.WriteAllLines(newFile, answer, System.Text.Encoding.Default);
            }
            catch
            {
                MessageBox.Show("Запись в файл не удалась");
            }
        }

        public static string[] ReadFile(string nameFile)
        {
            string[] textFile;
            try
            {
                textFile = File.ReadAllLines(nameFile, System.Text.Encoding.Default);
            }
            catch (Exception ex)
            {
                textFile = new string[] { "Чтение из файла не удалось" };
            }
            return textFile;
        }
        
    }
}
