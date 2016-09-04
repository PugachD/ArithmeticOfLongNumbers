using GalaSoft.MvvmLight;
using System.Windows.Input;
using ArithmeticOfLongNumbers.Model;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ArithmeticOfLongNumbers.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region ����
        readonly string nameUntilRun = "��������� ������";
        readonly string nameAfterRun = "���������� ������";
        private bool runCalc;
        private FileWork file;
        private BasicCalculations basicCalc;
        private string fullPathNameTxtFile;
        private string nameButtonRun;
        private ListExpression expressionList;
        #endregion

        #region ��������

        public ListExpression ExpressionList
        {
            get
            {
                return expressionList;
            }
            set
            {
                expressionList = value;
                RaisePropertyChanged("ExpressionList");
            }
        }
        public string NameButtonRun
        {
            get { return nameButtonRun; }
            set
            {
                nameButtonRun = value;
                RaisePropertyChanged("NameButtonRun");
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
                    RaisePropertyChanged("FullPathNameTxtFile");
                }
            }
        }
        public ICommand ClickOpenFile { get; private set; }
        public ICommand ClickRunCalc { get; private set; }
        #endregion

        #region ������
        public MainViewModel()
        {
            runCalc = false;
            NameButtonRun = nameUntilRun;
            //������������� ��������
            file = new FileWork();
            basicCalc = new BasicCalculations();

            ClickOpenFile = new RelayCommand(OpenFile);
            ClickRunCalc = new RelayCommand(RunCalc);
        }

        private void OpenFile()
        {
            try
            {
                file.OpenFile();
                FullPathNameTxtFile = file.FullPathNameTxtFile;
            }
            catch
            {
                MessageBox.Show("���-�� �� ���");
            }
        }

        private void RunCalc()
        {
            expressionList = new ListExpression();
            try
            {
                if (runCalc)
                {
                    runCalc = !runCalc;
                    NameButtonRun = nameAfterRun;
                    
                    basicCalc.RunCalc(FileWork.ReadFile(FullPathNameTxtFile), expressionList);
                    file.SaveFile(basicCalc.GetAnswerTxtFile);
                    ExpressionList = basicCalc.GetListExpression;

                    NameButtonRun = nameUntilRun;
                }
                else
                {
                    NameButtonRun = nameUntilRun;
                }
            }
            catch
            {
                MessageBox.Show("���-�� �� ���");
            }
        }
        #endregion
    }
}