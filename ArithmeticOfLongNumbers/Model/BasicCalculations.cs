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
using System.Windows.Threading;

namespace ArithmeticOfLongNumbers.Model
{
    public class BasicCalculations:PropertyChangedClass
    {
        #region Member Fields
        private DispatcherTimer dispatcherTimer;
        private TimeSpan timerInterval = new TimeSpan(0,0,0,0,222); //Интервал изменения таймера и обновления некоторой статистики
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
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = timerInterval;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
        }
        #endregion

        #region Functions
        public void RunCalculation(string[] _allLinesFile)
        {
            try
            {
                ResetData();

                allLineFile = _allLinesFile;
                answerTxtFile = new string[allLineFile.Count()];
                answerTxtFile[0] = allLineFile[0];
                countExpression = (uint)mainViewModel.MaxValueProgressBar;
                
                statistics = mainViewModel.Statistics;
                
                FillListStructure();

                dispatcherTimer.Start();
                Parallel.ForEach(listStruct, EvaluatingTheExpression);
                dispatcherTimer.Stop();
                //answerTxtFile[allLineFile.Count()-1] += Environment.NewLine;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Ошибки в файле: {0}", ex.Message));
            }

        }

        private void EvaluatingTheExpression(StructFileInfo structure)
        {
            if (!mainViewModel.CancellationPending())
            {
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
                    result = "Ошибка";
                }

                lock (answerTxtFile)
                {
                    answerTxtFile[structure.numberString] = result;
                }
            }
        }

        private void FillListStructure()
        {
            for (int i = 1; i < countExpression+1; i++)
            {
                listStruct.Add(new StructFileInfo(allLineFile[i], i));
            }
        }

        private void dispatcherTimer_Tick(object Sender, EventArgs e)
        {
            lock (Statistics)
            {
                Statistics.IncrementOverallProcessingTime(timerInterval);
            }
        }

        private void ResetData()
        {
            listStruct = new List<StructFileInfo>();
            Statistics = null;
        }

        #endregion
    }
}
