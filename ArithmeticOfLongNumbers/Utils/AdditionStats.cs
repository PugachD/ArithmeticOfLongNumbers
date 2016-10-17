using ArithmeticOfLongNumbers.ViewModel;
using System;

namespace ArithmeticOfLongNumbers.Utils
{
    public class AdditionStats : PropertyChangedClass
    {
        #region Addition Operation
        public int countOperation;
        public TimeSpan totalCalculationTime;
        private TimeSpan averageCalculationTime;
        private double percentOfOverallProcessingTime;
        #endregion Addition Operation

        private AdditionStats() { }

        #region Статическая часть
        static AdditionStats()
        {
            Reference = new AdditionStats();
        }

        public static AdditionStats Reference { get; private set; }
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

        /// <summary>
        /// Полное время обработки всех операций одного оператора
        /// </summary>
        public TimeSpan TotalCalculationTime
        {
            get { return totalCalculationTime; }
            internal set { totalCalculationTime = value; OnPropertyChanged("TotalCalculationTime"); }
        }
        /// <summary>
        /// Среднее время обработки всех операций одного оператора
        /// </summary>
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
