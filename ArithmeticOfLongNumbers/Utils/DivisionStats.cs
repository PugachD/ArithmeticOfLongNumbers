using ArithmeticOfLongNumbers.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticOfLongNumbers.Utils
{
    public class DivisionStats : PropertyChangedClass
    {
        #region Division Operation
        public int countOperation;
        public TimeSpan totalCalculationTime;
        private TimeSpan averageCalculationTime;
        private double percentOfOverallProcessingTime;
        #endregion Division Operation

        private DivisionStats() { }

        #region Статическая часть
        static DivisionStats()
        {
            Reference = new DivisionStats();
        }

        public static DivisionStats Reference { get; private set; }
        #endregion
        public void Reset()
        {
            CountOperation = 0;
            TotalCalculationTime = new TimeSpan();
            AverageCalculationTime = new TimeSpan();
            PercentOfOverallProcessingTime = 0;
        }
        public double PercentOfOverallProcessingTime
        {
            get { return Math.Round(percentOfOverallProcessingTime, 6); }
            internal set { percentOfOverallProcessingTime = value; OnPropertyChanged("PercentOfOverallProcessingTime"); }
        }
        public TimeSpan TotalCalculationTime
        {
            get { return totalCalculationTime; }
            internal set { totalCalculationTime = value; OnPropertyChanged("TotalCalculationTime"); }
        }

        public TimeSpan AverageCalculationTime
        {
            get { return averageCalculationTime; }
            internal set { averageCalculationTime = value; OnPropertyChanged("AverageCalculationTime"); }
        }

        public int CountOperation
        {
            get { return countOperation; }
            internal set { countOperation = value; OnPropertyChanged("CountOperation"); }
        }
    }
}
