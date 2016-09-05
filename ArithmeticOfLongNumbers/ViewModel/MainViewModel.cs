using System.Windows.Input;
using ArithmeticOfLongNumbers.Model;
using System.Windows;
using System;
using System.ComponentModel;
using ArithmeticOfLongNumbers.Commands;
using System.Threading;
using ArithmeticOfLongNumbers.Operation;

namespace ArithmeticOfLongNumbers.ViewModel
{
    public class MainViewModel : PropertyChangedView
    {
        #region Member Fields
        readonly string nameUntilRun = "Запустить расчет";
        readonly string nameAfterRun = "Остановить расчет";

        private BasicCalculations basicCalc;
        private bool isEnabledBtnRun = false, isEnabledBtnOpenFile = false;
        private FileWork file;
        private string fullPathNameTxtFile;
        private string nameButtonRun = "Запустить расчет";
        private int minValueProgressBar = 0, maxValueProgressBar = 10;
        double valueProgressBar;
        #endregion

        #region Member RelayCommands that implement ICommand
        RelayCommand _ResetCounter;
        RelayCommand _OpenFileCommand;
        RelayCommand _RunCalcCommand;
        #endregion

        #region Constructors
        public MainViewModel()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(Initialization);
            worker.RunWorkerAsync();
            //Initialization();
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
                OnPropertyChanged("IsNotEnabledBtnRun");
            }
        }

        public bool IsNotEnabledBtnRun
        {
            get { return !IsEnabledBtnRun; }
        }

        public bool IsEnabledBtnOpenFile
        {
            get { return isEnabledBtnOpenFile; }
            set
            {
                isEnabledBtnOpenFile = value;
                OnPropertyChanged("IsEnabledBtnOpenFile");
                OnPropertyChanged("IsNotEnabledBtnOpenFile");
            }
        }

        public bool IsNotEnabledBtnOpenFile
        {
            get { return !IsEnabledBtnOpenFile; }
        }
        public string NameButtonRun
        {
            get { return nameButtonRun; }
            set
            {
                nameButtonRun = value;
                OnPropertyChanged("NameButtonRun");
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
        public ICommand RunCalcCommand
        {
            get
            {
                if (_RunCalcCommand == null)
                {
                    _RunCalcCommand = new RelayCommand(param => this.RunCalc());
                }
                return _RunCalcCommand;
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
                NameButtonRun = nameUntilRun;
                //Инициализация объектов
                file = new FileWork();
                basicCalc = new BasicCalculations();
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
                file.OpenFile();
                FullPathNameTxtFile = file.FullPathNameTxtFile;
                IsEnabledBtnRun = true;
            }
            catch (Exception ex)
            {
                FullPathNameTxtFile = "";
                IsEnabledBtnRun = false;
                MessageBox.Show(ex.Message);
            }
        }

        private void RunCalc()
        {
            try
            {
                //Запрещаем открытие нового файла во время работы алгоритма
                IsEnabledBtnOpenFile = false;
                if (NameButtonRun == nameUntilRun)
                {
                    NameButtonRun = nameAfterRun;

                    using (BackgroundWorker worker = new BackgroundWorker())
                    {
                        worker.DoWork += new DoWorkEventHandler(worker_DoRun);

                        // Configure the function to run when completed
                        worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

                        // Launch the worker
                        worker.RunWorkerAsync();
                    }

                }
                else if (NameButtonRun == nameAfterRun)
                {
                    NameButtonRun = nameUntilRun;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }

        private void worker_DoRun(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {
                basicCalc.RunCalc(FileWork.ReadFile(FullPathNameTxtFile));
                file.SaveFile(basicCalc.GetAnswerTxtFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            NameButtonRun = nameUntilRun;
            IsEnabledBtnOpenFile = true;
        }

        /// <summary>
        /// This worker_ProgressChanged function is not in use for this project. Thanks to INotifyPropertyChanged, this is
        /// completely unnecessary.
        /// </summary>
        /// <param name="sender">The worker as Object, but it can be cast to a worker.</param>
        /// <param name="e">The ProgressChangedEventArgs object.</param>
        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Does nothing yet
            throw new NotImplementedException();
        }

        /// <summary>
        /// This worker_RunWorkerCompleted is called when the worker is finished.
        /// </summary>
        /// <param name="sender">The worker as Object, but it can be cast to a worker.</param>
        /// <param name="e">The RunWorkerCompletedEventArgs object.</param>
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //IsEnabledBtnRun = false;
        }

        /// <summary>
        /// This function resets the Value of the counter to 0.
        /// </summary>
        private void Reset()
        {
            //Value = Min;
        }
        #endregion
    }
}