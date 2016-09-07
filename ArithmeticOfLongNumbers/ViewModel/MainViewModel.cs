using System.Windows.Input;
using ArithmeticOfLongNumbers.Model;
using System.Windows;
using System;
using System.ComponentModel;
using ArithmeticOfLongNumbers.Commands;
using System.Threading;
using ArithmeticOfLongNumbers.Operation;
using ArithmeticOfLongNumbers.Utils;

namespace ArithmeticOfLongNumbers.ViewModel
{
    public class MainViewModel : PropertyChangedClass
    {
        #region Member Fields
        private BasicCalculations basicCalculate;
        private BackgroundWorker bgWorkerCalculation;
        private bool isEnabledBtnRun = false, isEnabledBtnOpenFile = false;
        private FileWork file;
        private MathStatistics statistics;
        private string fullPathNameTxtFile;
        private string[] allLineFile;
        private bool isNameButtonAfterRun;
        private int minValueProgressBar = 0, maxValueProgressBar = 10;
        double valueProgressBar;
        #endregion

        #region Member RelayCommands that implement ICommand
        RelayCommand _ResetCounter;
        RelayCommand _OpenFileCommand;
        RelayCommand _RunCalculationCommand;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(Initialization);
            worker.RunWorkerAsync();
            Reset();
            //Initialization();
        }
        #endregion

        #region Properties
        
        public MathStatistics Statistics
        {
            get { return statistics; }
            set { statistics = value; OnPropertyChanged("Statistics"); }
        }
        public bool IsEnabledBtnRun
        {
            get { return isEnabledBtnRun; }
            set
            {
                isEnabledBtnRun = value;
                OnPropertyChanged("IsEnabledBtnRun");
            }
        }

        public bool IsEnabledBtnOpenFile
        {
            get { return isEnabledBtnOpenFile; }
            set
            {
                isEnabledBtnOpenFile = value;
                OnPropertyChanged("IsEnabledBtnOpenFile");
            }
        }
        public bool IsNameButtonAfterRun
        {
            get { return isNameButtonAfterRun; }
            set
            {
                isNameButtonAfterRun = value;
                OnPropertyChanged("IsNameButtonAfterRun");
            }
        }

        public string FullPathNameTxtFile
        {
            get { return fullPathNameTxtFile; }
            set
            {
                if (fullPathNameTxtFile != value)
                {
                    fullPathNameTxtFile = value;
                    OnPropertyChanged("FullPathNameTxtFile");
                }
            }
        }
        public int MaxValueProgressBar
        {
            get { return maxValueProgressBar; }
            set { maxValueProgressBar = value; OnPropertyChanged("MaxValueProgressBar"); }
        }

        public int MinValueProgressBar
        {
            get { return minValueProgressBar; }
            set { minValueProgressBar = value; OnPropertyChanged("MinValueProgressBar"); }
        }

        /// <summary>
        /// This is the Value.  The Counter should display this.
        /// </summary>
        public Double ValueProgressBar
        {
            get { return valueProgressBar; }
            set
            {
                if (value <= maxValueProgressBar)
                {
                    if (value >= minValueProgressBar) { valueProgressBar = value; }
                    else { valueProgressBar = minValueProgressBar; }
                }
                else { valueProgressBar = maxValueProgressBar; }
                OnPropertyChanged("ValueProgressBar");
            }
        }
        #region ICommand Properties
        public ICommand OpenFileCommand
        {
            get
            {
                if (_OpenFileCommand == null)
                {
                    _OpenFileCommand = new RelayCommand(param => this.OpenFile());
                }
                return _OpenFileCommand;
            }
        }
        public ICommand RunCalculationCommand
        {
            get
            {
                if (_RunCalculationCommand == null)
                {
                    _RunCalculationCommand = new RelayCommand(param => this.RunCalculation());
                }
                return _RunCalculationCommand;
            }
        }
        public ICommand ResetCounter
        {
            get
            {
                if (_ResetCounter == null)
                {
                    _ResetCounter = new RelayCommand(param => this.Reset());
                }
                return _ResetCounter;
            }
        }
        #endregion ICommand Properties
        #endregion

        #region Functions

        private void Initialization(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                IsNameButtonAfterRun = false;
                //Инициализация объектов
                file = new FileWork();
                basicCalculate = new BasicCalculations(this);
                IsEnabledBtnOpenFile = true;
            }
            catch (Exception ex)
            {
                IsEnabledBtnOpenFile = false;
                IsEnabledBtnRun = false;
                MessageBox.Show(ex.Message);
            }

        }
        private void OpenFile()
        {
            try
            {
                if (file.OpenFile())
                {
                    FullPathNameTxtFile = file.FullPathNameTxtFile;
                    allLineFile = file.ReadFile(FullPathNameTxtFile);
                    MaxValueProgressBar = int.Parse(allLineFile[0]);
                    IsEnabledBtnRun = true;
                }

            }
            catch (Exception ex)
            {
                FullPathNameTxtFile = "";
                IsEnabledBtnRun = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void RunCalculation()
        {
            try
            {
                //Запрещаем открытие нового файла во время работы алгоритма
                IsEnabledBtnOpenFile = false;
                if (!IsNameButtonAfterRun)
                {
                    IsNameButtonAfterRun = !IsNameButtonAfterRun;

                    Reset();
                    Statistics.CountExpressionInFile = MaxValueProgressBar;
                    bgWorkerCalculation = new BackgroundWorker() { WorkerSupportsCancellation = true, WorkerReportsProgress = true };
                    bgWorkerCalculation.DoWork += new DoWorkEventHandler(worker_DoRunCalculation);
                

                    // Configure the function to run when completed
                    bgWorkerCalculation.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

                    // Launch the worker
                    bgWorkerCalculation.RunWorkerAsync();

                }
                else
                {
                    bgWorkerCalculation.CancelAsync();
                    IsNameButtonAfterRun = false;
                    IsEnabledBtnOpenFile = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void worker_DoRunCalculation(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
                try
                {
                    basicCalculate.RunCalculation(allLineFile);
                    file.SaveFile(basicCalculate.GetAnswerTxtFile);
                    worker.ReportProgress(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }
        

        /// <summary>
        /// This worker_RunWorkerCompleted is called when the worker is finished.
        /// </summary>
        /// <param name="sender">The worker as Object, but it can be cast to a worker.</param>
        /// <param name="e">The RunWorkerCompletedEventArgs object.</param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsNameButtonAfterRun = false;
            IsEnabledBtnOpenFile = true;
            Statistics.PredictionOfRemainingTime = new TimeSpan(0);
            if (e.Cancelled)
                MessageBox.Show("Подсчет остановлен");
            else
                MessageBox.Show("Данные записаны в файл: " + file.NameNewTxtFile);
        }

        /// <summary>
        /// This function resets the Value of the counter to 0.
        /// </summary>
        private void Reset()
        {
            Expression.ResetInstanceStatistic();
            Statistics = new MathStatistics();
            Expression.InitializeStat(Statistics);
            basicCalculate = new BasicCalculations(this);
            //Value = Min;
        }
        
        public bool CancellationPending()
        {
            return bgWorkerCalculation.CancellationPending;
        }
        #endregion
    }
}