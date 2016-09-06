using System;
using System.Diagnostics;
using System.Numerics;
using ArithmeticOfLongNumbers.Utils;

namespace ArithmeticOfLongNumbers.Operation
{
    public class Addition : Expression
    {
        public Addition():base()
        {
        }
        public Addition(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
        }

        public override BigInteger Operator(ref MathStatistics stat)
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = number1 + number2;

            sWatch.Stop();

            stat.CountAdditionOperation++;
            stat.TotalCalculationTimeAddition += sWatch.Elapsed;
            stat.AverageCalculationTimeAddition = stat.CalculateAverageTime(stat.TotalCalculationTimeAddition, stat.CountAdditionOperation);
            stat.IncrementOverallProcessingTime(sWatch.Elapsed);
            stat.PercentOfOverallProcessingTimeAddition = stat.CalculatePercent(stat.TotalCalculationTimeAddition);
                return result;
            }
    }
}
