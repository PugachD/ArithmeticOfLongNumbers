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
        #region Member Fields
        private List<StructFileInfo> listStruct = new List<StructFileInfo>();
        private Parsing parsing;
        private string[] allLineFile, answerTxtFile;
        private uint countExpression;
        #endregion

        #region Properties
        public string[] GetAnswerTxtFile
        {
            get { return answerTxtFile; }
            private set { }
        }
        #endregion

        #region Constructors
        public BasicCalculations()
        {
            parsing = new Parsing();
        }
        #endregion

        #region Functions
        public void RunCalc(string[] _allLinesFile)
        {
            try
            {
                allLineFile = _allLinesFile;
                answerTxtFile = new string[allLineFile.Count()];
                answerTxtFile[0] = allLineFile[0];

                if (allLineFile == null)
                    throw new NullReferenceException();

                GetCountStringFile();
                FillListStructure();

                Parallel.ForEach(listStruct, EvaluatingTheExpression);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ошибки в файле: {0}", ex.Message));
            }

            MessageBox.Show("Расчет закончен");
        }

        private void EvaluatingTheExpression(StructFileInfo structure)
        {
            string result;
            try
            {
                string RPN = parsing.GetExpression(structure.expression);
                result = parsing.Counting(RPN).ToString();
            }
            catch (Exception ex)
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
        #endregion
    }
}
