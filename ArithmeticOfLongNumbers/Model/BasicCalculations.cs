using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ArithmeticOfLongNumbers.Parser;
using System.Numerics;
using ArithmeticOfLongNumbers.Utils;
using ArithmeticOfLongNumbers.ViewModel;
using System.Threading;

namespace ArithmeticOfLongNumbers.Model
{
    public class BasicCalculations:PropertyChangedClass
    {
        #region Member Fields
        private List<StructFileInfo> listStruct;
        private MathStatistics statistics;
        private MainViewModel mainViewModel;
        private Parsing parsing;
        private string[] allLineFile, answerTxtFile;
        private uint countExpression;
        #endregion

        #region Properties
        public MathStatistics Statistics
        {
            get { return statistics; }
            set { statistics = value; OnPropertyChanged("Statistics"); }
        }
        public string[] GetAnswerTxtFile
        {
            get { return answerTxtFile; }
            private set { }
        }
        #endregion

        #region Constructors
        public BasicCalculations(MainViewModel _mainViewModel)
        {
            parsing = new Parsing();
            mainViewModel = _mainViewModel;
        }
        #endregion

        #region Functions
        public void RunCalc(string[] _allLinesFile)
        {
            try
            {
                ResetData();

                allLineFile = _allLinesFile;
                answerTxtFile = new string[allLineFile.Count()];
                answerTxtFile[0] = allLineFile[0];

                if (allLineFile == null)
                    throw new NullReferenceException();
                statistics = mainViewModel.Statistics;

                GetCountStringFile();
                FillListStructure();

                Parallel.ForEach(listStruct, EvaluatingTheExpression);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ошибки в файле: {0}", ex.Message));
            }

        }

        private void EvaluatingTheExpression(StructFileInfo structure)
        {
            Thread.Sleep(3000);
            string result;
            try
            {
                lock (statistics)
                {
                    string RPN = parsing.GetExpression(structure.expression);
                    result = parsing.Counting(RPN, ref statistics).ToString();
                    statistics.InstanceCount += 1;
                }
            }
            catch (Exception ex)
            {
                //Записать "Ошибка" в файл
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

        private void ResetData()
        {
            listStruct = new List<StructFileInfo>();
            Statistics = null;
        }
        #endregion
    }
}
