using System;
using ArithmeticOfLongNumbers.ViewModel;

namespace ArithmeticOfLongNumbers.Utils
{
    public class MathStatistics : PropertyChangedClass
    {

        #region  Member Fields
        private double instanceCount;
        private TimeSpan overallProcessingTime;

        #region Addition Operation
        private int countAdditionOperation;
        private TimeSpan totalCalculationTimeAddition;
        private TimeSpan averageCalculationTimeAddition;
        private double percentOfOverallProcessingTimeAddition;
        #endregion Addition Operation

        #region Division Operation
        private int countDivisionOperation;
        private TimeSpan totalCalculationTimeDivision;
        private TimeSpan averageCalculationTimeDivision;
        private double percentOfOverallProcessingTimeDivision;
        #endregion Division Operation

        #region Unary Negative Operation
        private int countUnaryNegativeOperation;
        private TimeSpan totalCalculationTimeUnaryNegative;
        private TimeSpan averageCalculationTimeUnaryNegative;
        private double percentOfOverallProcessingTimeUnaryNegative;
        #endregion Unary Negative Operation

        #region Substraction Operation
        private int countSubstractionOperation;
        private TimeSpan totalCalculationTimeSubstraction;
        private TimeSpan averageCalculationTimeSubstraction;
        private double percentOfOverallProcessingTimeSubstraction;
        #endregion Substraction Operation

        #region Multiplication Operation
        private int countMultiplicationOperation;
        private TimeSpan totalCalculationTimeMultiplication;
        private TimeSpan averageCalculationTimeMultiplication;
        private double percentOfOverallProcessingTimeMultiplication;
        #endregion Multiplication Operation
        #endregion

        #region Constructors
        public MathStatistics()
        {
            InstanceCount = 0;
        }
        #endregion

        #region Properties

        public double InstanceCount
        {
            get { return instanceCount; }
            set { instanceCount = value; OnPropertyChanged("InstanceCount"); }
        }
        /// <summary>
        /// Общее время обработки всех операций всех операторов
        /// </summary>
        public TimeSpan OverallProcessingTime
        {
            get { return overallProcessingTime; }
            set { overallProcessingTime = value; OnPropertyChanged("OverallProcessingTime"); }
        }

        public double PercentOfOverallProcessingTimeAddition
        {
            get { return Math.Round(percentOfOverallProcessingTimeAddition, 2); }
            set { percentOfOverallProcessingTimeAddition = value; OnPropertyChanged("PercentOfOverallProcessingTimeAddition"); }
        }
        public double PercentOfOverallProcessingTimeDivision
        {
            get { return Math.Round(percentOfOverallProcessingTimeDivision, 2); }
            set { percentOfOverallProcessingTimeDivision = value; OnPropertyChanged("PercentOfOverallProcessingTimeDivision"); }
        }
        public double PercentOfOverallProcessingTimeMultiplication
        {
            get { return Math.Round(percentOfOverallProcessingTimeMultiplication, 2); }
            set { percentOfOverallProcessingTimeMultiplication = value; OnPropertyChanged("PercentOfOverallProcessingTimeMultiplication"); }
        }
        public double PercentOfOverallProcessingTimeSubstraction
        {
            get { return Math.Round(percentOfOverallProcessingTimeSubstraction, 2); }
            set { percentOfOverallProcessingTimeSubstraction = value; OnPropertyChanged("PercentOfOverallProcessingTimeSubstraction"); }
        }
        public double PercentOfOverallProcessingTimeUnaryNegative
        {
            get { return Math.Round(percentOfOverallProcessingTimeUnaryNegative, 2); }
            set { percentOfOverallProcessingTimeUnaryNegative = value; OnPropertyChanged("PercentOfOverallProcessingTimeUnaryNegative"); }
        }
        /// <summary>
        /// Полное время обработки всех операций одного оператора
        /// </summary>
        public TimeSpan TotalCalculationTimeAddition
        {
            get { return totalCalculationTimeAddition; }
            set { totalCalculationTimeAddition = value; OnPropertyChanged("TotalCalculationTimeAddition"); }
        }
        /// <summary>
        /// Среднее время обработки всех операций одного оператора
        /// </summary>
        public TimeSpan AverageCalculationTimeAddition
        {
            get { return averageCalculationTimeAddition; }
            set { averageCalculationTimeAddition = value; OnPropertyChanged("AverageCalculationTimeAddition"); }
        }

        public int CountAdditionOperation
        {
            get { return countAdditionOperation; }
            set { countAdditionOperation = value; OnPropertyChanged("CountAdditionOperation"); }
        }

        public TimeSpan TotalCalculationTimeDivision
        {
            get { return totalCalculationTimeDivision; }
            set { totalCalculationTimeDivision = value; OnPropertyChanged("TotalCalculationTimeDivision"); }
        }

        public TimeSpan AverageCalculationTimeDivision
        {
            get { return averageCalculationTimeDivision; }
            set { averageCalculationTimeDivision = value; OnPropertyChanged("AverageCalculationTimeDivision"); }
        }

        public int CountDivisionOperation
        {
            get { return countDivisionOperation; }
            set { countDivisionOperation = value; OnPropertyChanged("CountDivisionOperation"); }
        }

        public TimeSpan TotalCalculationTimeMultiplication
        {
            get { return totalCalculationTimeMultiplication; }
            set { totalCalculationTimeMultiplication = value; OnPropertyChanged("TotalCalculationTimeMultiplication"); }
        }

        public TimeSpan AverageCalculationTimeMultiplication
        {
            get { return averageCalculationTimeMultiplication; }
            set { averageCalculationTimeMultiplication = value; OnPropertyChanged("AverageCalculationTimeMultiplication"); }
        }

        public int CountMultiplicationOperation
        {
            get { return countMultiplicationOperation; }
            set { countMultiplicationOperation = value; OnPropertyChanged("CountMultiplicationOperation"); }
        }
        public TimeSpan TotalCalculationTimeSubstraction
        {
            get { return totalCalculationTimeSubstraction; }
            set { totalCalculationTimeSubstraction = value; OnPropertyChanged("TotalCalculationTimeSubstraction"); }
        }

        public TimeSpan AverageCalculationTimeSubstraction
        {
            get { return averageCalculationTimeSubstraction; }
            set { averageCalculationTimeSubstraction = value; OnPropertyChanged("AverageCalculationTimeSubstraction"); }
        }

        public int CountSubstractionOperation
        {
            get { return countSubstractionOperation; }
            set { countSubstractionOperation = value; OnPropertyChanged("CountSubstractionOperation"); }
        }
        public TimeSpan TotalCalculationTimeUnaryNegative
        {
            get { return totalCalculationTimeUnaryNegative; }
            set { totalCalculationTimeUnaryNegative = value; OnPropertyChanged("TotalCalculationTimeUnaryNegative"); }
        }

        public TimeSpan AverageCalculationTimeUnaryNegative
        {
            get { return averageCalculationTimeUnaryNegative; }
            set { averageCalculationTimeUnaryNegative = value; OnPropertyChanged("AverageCalculationTimeUnaryNegative"); }
        }

        public int CountUnaryNegativeOperation
        {
            get { return countUnaryNegativeOperation; }
            set { countUnaryNegativeOperation = value; OnPropertyChanged("CountUnaryNegativeOperation"); }
        }
        #endregion


        public void IncrementOverallProcessingTime(TimeSpan value)
        {
            OverallProcessingTime += value;
        }
        public double CalculatePercent(TimeSpan value)
        {
            return (((double)value.Ticks / OverallProcessingTime.Ticks) * 100);
        }
        public TimeSpan CalculateAverageTime(TimeSpan value, int countOperation)
        {
            return (new TimeSpan((long)(value.Ticks / (double)countOperation)));
        }
    }
}
