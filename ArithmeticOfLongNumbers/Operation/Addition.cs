﻿using System;
using System.Diagnostics;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Operation
{
    public class Addition : Expression
    {
        private static int countCalculations;
        private static TimeSpan totalCalculationTime;
        private static TimeSpan averageCalculationTime;

        /// <summary>
        /// Полное время обработки всех операций одного оператора
        /// </summary>
        public static TimeSpan TotalCalculationTime
        {
            get { return totalCalculationTime; }
            set { totalCalculationTime += value; }
        }
        /// <summary>
        /// Среднее время обработки всех операций одного оператора
        /// </summary>
        public static TimeSpan AverageCalculationTime
        {
            get { return averageCalculationTime; }
            set { averageCalculationTime += value; }
        }

        public static int CountCalculations
        {
            get { return countCalculations; }
            set { countCalculations += value; }
        }

        public Addition():base()
        {
            CountCalculations = 1;
        }
        public Addition(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
            CountCalculations = 1;
        }

        public override BigInteger Operator()
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = number1 + number2;

            sWatch.Stop();

            TotalCalculationTime = sWatch.Elapsed;
            IncrementOverallProcessingTime(sWatch.Elapsed);
            return result;
        }
    }
}
