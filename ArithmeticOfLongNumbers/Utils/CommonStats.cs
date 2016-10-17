using ArithmeticOfLongNumbers.ViewModel;
using System;

namespace ArithmeticOfLongNumbers.Utils
{
    public class CommonStats : PropertyChangedClass
    {
        public int instanceCount;
        public TimeSpan overallProcessingTime;
        private TimeSpan predictionOfRemainingTime;
        private int minValueProgressBar = 0, countExpressionInFile = 0;

        private CommonStats() { }

        #region Статическая часть
        static CommonStats()
        {
            Reference = new CommonStats();
        }

        public static CommonStats Reference { get; private set; }
        #endregion


        #region Properties
        public int MinValueProgressBar
        {
            get { return minValueProgressBar; }
            set { minValueProgressBar = value; OnPropertyChanged("MinValueProgressBar"); }
        }
        /// <summary>
        /// Количество выражений в файле
        /// </summary>
        public int CountExpressionInFile
        {
            get { return countExpressionInFile; }
            set { countExpressionInFile = value; OnPropertyChanged("CountExpressionInFile"); }
        }
        /// <summary>
        /// Число экземпляров Expression (Общее число операторов)
        /// </summary>
        public int InstanceCount
        {
            get { return instanceCount; }
            set
            {
                instanceCount = value;
                OnPropertyChanged("InstanceCount");
            }
        }
        public TimeSpan PredictionOfRemainingTime
        {
            get { return predictionOfRemainingTime; }
            internal set { predictionOfRemainingTime = value; OnPropertyChanged("PredictionOfRemainingTime"); }
        }
        /// <summary>
        /// Общее время обработки всех операций всех операторов
        /// </summary>
        public TimeSpan OverallProcessingTime
        {
            get { return overallProcessingTime; }
            internal set
            { overallProcessingTime = value; UpdateStatPercantOfOverall(); OnPropertyChanged("OverallProcessingTime"); }
        }
        #endregion

        #region Methods
        public void Reset()
        {
            InstanceCount = 0;
            OverallProcessingTime = new TimeSpan();
            PredictionOfRemainingTime = new TimeSpan();
            CountExpressionInFile = 0;
            MinValueProgressBar = 0;
        }

        public void IncrementOverallProcessingTime(TimeSpan value)
        {
            OverallProcessingTime += value;
        }

        /*public double CalculatePercent(TimeSpan value)
        {
            return (((double)value.Ticks / OverallProcessingTime.Ticks) * 100);
        }
        public TimeSpan CalculateAverageTime(TimeSpan value, int countOperation)
        {
            return (new TimeSpan((long)(value.Ticks / (double)countOperation)));
        }*/
        private void UpdateStatPercantOfOverall()
        {
            Reference.InstanceCount = Reference.instanceCount;
            AdditionStats.Reference.CountOperation = AdditionStats.Reference.countOperation;
            AdditionStats.Reference.TotalCalculationTime = AdditionStats.Reference.totalCalculationTime;
            DivisionStats.Reference.CountOperation = DivisionStats.Reference.countOperation;
            DivisionStats.Reference.TotalCalculationTime = DivisionStats.Reference.totalCalculationTime;
            UnaryNegativeStats.Reference.CountOperation = UnaryNegativeStats.Reference.countOperation;
            UnaryNegativeStats.Reference.TotalCalculationTime = UnaryNegativeStats.Reference.totalCalculationTime;
            SubstractionStats.Reference.CountOperation = SubstractionStats.Reference.countOperation;
            SubstractionStats.Reference.TotalCalculationTime = SubstractionStats.Reference.totalCalculationTime;
            MultiplicationStats.Reference.CountOperation = MultiplicationStats.Reference.countOperation;
            MultiplicationStats.Reference.TotalCalculationTime = MultiplicationStats.Reference.totalCalculationTime;

            if (OverallProcessingTime.Ticks != 0)
            {
                AdditionStats.Reference.PercentOfOverallProcessingTime = ((double)AdditionStats.Reference.TotalCalculationTime.Ticks / OverallProcessingTime.Ticks) * 100;
                DivisionStats.Reference.PercentOfOverallProcessingTime = ((double)DivisionStats.Reference.TotalCalculationTime.Ticks / OverallProcessingTime.Ticks) * 100;
                MultiplicationStats.Reference.PercentOfOverallProcessingTime = ((double)MultiplicationStats.Reference.TotalCalculationTime.Ticks / OverallProcessingTime.Ticks) * 100;
                SubstractionStats.Reference.PercentOfOverallProcessingTime = ((double)SubstractionStats.Reference.TotalCalculationTime.Ticks / OverallProcessingTime.Ticks) * 100;
                UnaryNegativeStats.Reference.PercentOfOverallProcessingTime = ((double)UnaryNegativeStats.Reference.TotalCalculationTime.Ticks / OverallProcessingTime.Ticks) * 100;
            }
            if (InstanceCount != 0)
            {
                AdditionStats.Reference.AverageCalculationTime = (new TimeSpan((long)(AdditionStats.Reference.TotalCalculationTime.Ticks / (double)AdditionStats.Reference.CountOperation)));
                DivisionStats.Reference.AverageCalculationTime = (new TimeSpan((long)(DivisionStats.Reference.TotalCalculationTime.Ticks / (double)DivisionStats.Reference.CountOperation)));
                UnaryNegativeStats.Reference.AverageCalculationTime = (new TimeSpan((long)(UnaryNegativeStats.Reference.TotalCalculationTime.Ticks / (double)UnaryNegativeStats.Reference.CountOperation)));
                SubstractionStats.Reference.AverageCalculationTime = (new TimeSpan((long)(SubstractionStats.Reference.TotalCalculationTime.Ticks / (double)SubstractionStats.Reference.CountOperation)));
                MultiplicationStats.Reference.AverageCalculationTime = (new TimeSpan((long)(MultiplicationStats.Reference.TotalCalculationTime.Ticks / (double)MultiplicationStats.Reference.CountOperation)));
                int difference = CountExpressionInFile - InstanceCount;
                if (difference != 0)
                    PredictionOfRemainingTime = new TimeSpan(((OverallProcessingTime.Ticks / InstanceCount) * (difference)));
            }
        }
        #endregion
    }
}
