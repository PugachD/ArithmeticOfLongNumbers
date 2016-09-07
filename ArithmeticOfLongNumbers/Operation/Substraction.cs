using ArithmeticOfLongNumbers.Utils;
using System;
using System.Diagnostics;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Operation
{
    
    public class Substraction : Expression
    {
        
        public Substraction():base()
        {
        }
        public Substraction(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
        }
        public override BigInteger Operator(ref MathStatistics stat)
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = number1 - number2;

            sWatch.Stop();

            stat.CountSubstractionOperation++;
            stat.TotalCalculationTimeSubstraction += sWatch.Elapsed;
            stat.AverageCalculationTimeSubstraction = stat.CalculateAverageTime(stat.TotalCalculationTimeSubstraction, stat.CountSubstractionOperation);
            //stat.IncrementOverallProcessingTime(sWatch.Elapsed);

            return result;
        }
    }
}
