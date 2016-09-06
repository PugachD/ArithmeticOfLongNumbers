using ArithmeticOfLongNumbers.Utils;
using System;
using System.Diagnostics;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Operation
{
    public class Division : Expression
    {
        public Division():base()
        {
        }
        public Division(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
        }

        public override BigInteger Operator(ref MathStatistics stat)
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = number1 / number2;

            sWatch.Stop();

            stat.CountDivisionOperation++;
            stat.TotalCalculationTimeDivision += sWatch.Elapsed;
            stat.AverageCalculationTimeDivision = stat.CalculateAverageTime(stat.TotalCalculationTimeDivision,stat.CountDivisionOperation);
            stat.IncrementOverallProcessingTime(sWatch.Elapsed);
            stat.PercentOfOverallProcessingTimeDivision = stat.CalculatePercent(stat.TotalCalculationTimeDivision);

            return result;
        }
    }
}
