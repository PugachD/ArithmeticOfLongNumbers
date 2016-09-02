using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArithmeticOfLongNumbers.Parser;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Model
{
    class BasicCalculations
    {
        private string[] allLineFile;
        private string[] answerTxtFile;
        private List<StructFileInfo> listStruct = new List<StructFileInfo>();
        private uint countExpression;

        public string[] GetAnswerTxtFile
        {
            get { return answerTxtFile; }
        }

        public void RunCalc(string[] _allLinesFile)
        {
            try
            {
                allLineFile = _allLinesFile;
                answerTxtFile = new string[allLineFile.Count()];
                answerTxtFile[0] = allLineFile[0];

                if (allLineFile == null)
                    throw new Exception();

                GetCountStringFile();
                FillListStructure();

                Parallel.ForEach(listStruct, EvaluatingTheExpression);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Ошибки в файле: {0}", ex.Message));
            }

            MessageBox.Show("Расчет закончен");
        }

        private void EvaluatingTheExpression(StructFileInfo structure)
        {
            string result;
            try
            {
                string RPN = Parsing.GetExpression(structure.expression);
                result = Parsing.Counting(RPN).ToString();

            }
            catch
            {
                result  = "Ошибка";
            }

            lock (answerTxtFile)
            {
                answerTxtFile[structure.numberString] = result;
            }
        }

        private void FillListStructure()
        {
            for (int i = 1; i < countExpression+1; i++)
            {
                listStruct.Add(new StructFileInfo(allLineFile[i], i));
            }
        }

        private void GetCountStringFile()
        {
            countExpression = uint.Parse(allLineFile[0]);
            if (countExpression > (allLineFile.Length - 1))
                throw new Exception("Количество строк для расчета превышает число строк в файле");
        }
    }
}
