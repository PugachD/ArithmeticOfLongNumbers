using System.Windows.Input;
using ArithmeticOfLongNumbers.Model;
using System.Windows;
using System;
using System.ComponentModel;
using ArithmeticOfLongNumbers.Commands;
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
        private string fullPathNameTxtFile;
        private string[] allLineFile;
        private bool isNameButtonAfterRun;
        private int maxValueProgressBar = 10;
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
        }
        #endregion

        #region Properties
        
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
                file = new FileWork();
                basicCalculate = new BasicCalculations(this);
                IsEnabledBtnOpenFile = true;
            }
            catch (Exception ex)
            {
                IsEnabledBtnOpenFile = false;
                IsEnabledBtnRun = false;
                MessageBox.Show("Инициализация прервана" + ex.Message);
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
                    MaxValueProgressBar = file.CountExpressions;
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
                    CommonStats.Reference.CountExpressionInFile = MaxValueProgressBar;
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
                    //IsNameButtonAfterRun = false;
                    //IsEnabledBtnOpenFile = true;
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
                //try
                {
                    basicCalculate.RunCalculation(allLineFile);
                    file.SaveFile(basicCalculate.GetAnswerTxtFile);
                    worker.ReportProgress(0);
                }
                //catch (Exception ex)
                {
                   // MessageBox.Show(ex.Message);
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
            CommonStats.Reference.PredictionOfRemainingTime = new TimeSpan(0);
            if (e.Error != null)
                MessageBox.Show(e.Error.Message);
            else if (e.Cancelled)
                MessageBox.Show("Подсчет остановлен. Данные не записаны в файл");
            else
                MessageBox.Show("Данные записаны в файл: " + file.NameNewTxtFile);
        }

        /// <summary>
        /// This function resets the Value of the counter to 0.
        /// </summary>
        private void Reset()
        {
            basicCalculate = new BasicCalculations(this);
            AdditionStats.Reference.Reset();
            DivisionStats.Reference.Reset();
            MultiplicationStats.Reference.Reset();
            SubstractionStats.Reference.Reset();
            UnaryNegativeStats.Reference.Reset();
            CommonStats.Reference.Reset();
        }
        
        public bool CancellationPending()
        {
            return bgWorkerCalculation.CancellationPending;
        }
        #endregion
    }
}