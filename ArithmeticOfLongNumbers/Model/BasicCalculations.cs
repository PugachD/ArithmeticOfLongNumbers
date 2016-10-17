#define Timer
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
using System.Diagnostics;

namespace ArithmeticOfLongNumbers.Model
{
    public class BasicCalculations:PropertyChangedClass
    {
        #region Member Fields
        private DispatcherTimer dispatcherTimer;
        private TimeSpan timerInterval = new TimeSpan(0, 0, 0, 0, 1000); //Интервал изменения таймера и обновления некоторой статистики
        private Stopwatch stWatch;
        private List<StructFileInfo> listStruct;
        private MainViewModel mainViewModel;
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
        public BasicCalculations(MainViewModel _mainViewModel)
        {
            mainViewModel = _mainViewModel;
#if Timer
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = timerInterval;
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
#endif
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
                countExpression = (uint)CommonStats.Reference.CountExpressionInFile;
               
                FillListStructure();
#if Timer
                dispatcherTimer.Start();
                stWatch.Start();
#endif
                Parallel.ForEach(listStruct, EvaluatingTheExpression);
#if Timer
                dispatcherTimer.Stop();
                stWatch.Stop();
#endif
                dispatcherTimer_Tick(null, null);
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
                string result = "";
                try
                {
                    Parsing parsing = new Parsing();
                    parsing.GetExpression(structure.expression);
                    result = parsing.Counting().ToString();
                    //lock (CommonStats.Reference)
                    {
                        Interlocked.Increment(ref CommonStats.Reference.instanceCount);
                        //CommonStats.Reference.InstanceCount += 1;
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
                result = null;
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
            lock (CommonStats.Reference)
            {
                CommonStats.Reference.OverallProcessingTime = stWatch.Elapsed;
            }
        }

        private void ResetData()
        {
            listStruct = new List<StructFileInfo>();
            stWatch = new Stopwatch();
        }

#endregion
    }
}
