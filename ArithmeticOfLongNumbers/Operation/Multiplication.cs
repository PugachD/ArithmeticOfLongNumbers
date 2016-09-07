﻿using ArithmeticOfLongNumbers.Utils;
using System;
using System.Diagnostics;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Operation
{
    public class Multiplication : Expression
    {
        
        public Multiplication():base()
        {
        }
        public Multiplication(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2)
        {
        }
        public override BigInteger Operator(ref MathStatistics stat)
        {
            BigInteger result = new BigInteger();
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();

            result = number1 * number2;

            sWatch.Stop();

            stat.CountMultiplicationOperation++;
            stat.TotalCalculationTimeMultiplication += sWatch.Elapsed;
            stat.AverageCalculationTimeMultiplication = stat.CalculateAverageTime(stat.TotalCalculationTimeMultiplication,stat.CountMultiplicationOperation);
            //stat.IncrementOverallProcessingTime(sWatch.Elapsed);

            return result;
        }
    }
}
