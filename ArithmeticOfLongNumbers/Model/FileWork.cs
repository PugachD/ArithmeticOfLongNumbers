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
        private string nameTxtFile;
        private string nameNewTxtFile;
        private int countExpressions;
        private string[] textFile;

        public string FullPathNameTxtFile { get; set; }
        public string NameNewTxtFile { get { return nameNewTxtFile; } set {nameNewTxtFile = value; } }
        public int CountExpressions { get { return countExpressions; } private set { countExpressions = value; } }

        public bool OpenFile ()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.RestoreDirectory = true;

            if ((bool)openFileDialog.ShowDialog())
            {
                FullPathNameTxtFile = openFileDialog.FileName;
                nameTxtFile = Path.GetFileNameWithoutExtension(FullPathNameTxtFile);
                return true;
            }
            else if (FullPathNameTxtFile == "")
            {
                throw new FileNotFoundException("Файл не выбран");
            }
            else
                return false;

        }


        public void SaveFile(string[] answer)
        {
            NameNewTxtFile = Directory.GetCurrentDirectory() + "\\Answer_" + nameTxtFile + ".txt";
            try
            {
                File.WriteAllLines(NameNewTxtFile, answer, System.Text.Encoding.Default);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message + ". Чтение из файла не удалось");
            }

        }

        public string[] ReadFile(string nameFile)
        {
            
            try
            {
                textFile = File.ReadAllLines(nameFile, System.Text.Encoding.Default);
                if (textFile.Length < 2)
                    throw new ArgumentNullException("Файл пустой или неполный");
                CountExpressions = GetCountExpressions();
                CheckedForErrorsFile();
            }
            catch (IOException ex)
            {
                textFile = new string[] { "Чтение из файла не удалось" };
                throw new IOException(ex.Message + ". Чтение из файла не удалось");
            }
            return textFile;
        }

        public void CheckedForErrorsFile()
        {
            if (countExpressions > 1000000)
                throw new OverflowException("Задано недопустимое количество выражений");
            if (countExpressions > (textFile.Length - 1))
                throw new OverflowException("Количество строк для расчета превышает число строк в файле");
        }
        
        private int GetCountExpressions()
        {
            return int.Parse(textFile[0]);
        }
    }
}
