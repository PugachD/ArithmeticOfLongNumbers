using System;
using System.Diagnostics;
using System.Numerics;
using ArithmeticOfLongNumbers.Interface;
using ArithmeticOfLongNumbers.Utils;

namespace ArithmeticOfLongNumbers
{
    public abstract class Expression: IExpression
    {
        protected BigInteger number1;
        protected BigInteger number2;
        
        private static MathStatistics statistics;

        public static void InitializeStat(MathStatistics _statistics)
        {
            statistics = _statistics;
        }

        public static void ResetInstanceStatistic()
        {
            statistics = null;
        }


        /*static Expression()
        {
            overallProcessingTime = new TimeSpan();
        }*/

        public Expression()
        {
        }

        public Expression(BigInteger lhs, BigInteger rhs)
        {
            number1 = lhs;
            number2 = rhs;
        }

        public abstract BigInteger Operator(ref MathStatistics stat);
    }
}
