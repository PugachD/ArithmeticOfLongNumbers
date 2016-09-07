using ArithmeticOfLongNumbers.Utils;
using System;
using System.Diagnostics;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Operation
{
    public class UnaryNegative : Expression
    {
        public UnaryNegative():base()
        {
        }

        public UnaryNegative(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
        }
        public override BigInteger Operator(ref MathStatistics stat)
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = -number2;

            sWatch.Stop();

            stat.CountUnaryNegativeOperation++;
            stat.TotalCalculationTimeUnaryNegative += sWatch.Elapsed;
            stat.AverageCalculationTimeUnaryNegative = stat.CalculateAverageTime(stat.TotalCalculationTimeUnaryNegative,stat.CountUnaryNegativeOperation);
           // stat.IncrementOverallProcessingTime(sWatch.Elapsed);

            return result;
        }
    }
}
