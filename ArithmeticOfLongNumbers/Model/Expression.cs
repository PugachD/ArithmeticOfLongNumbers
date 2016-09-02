using System;
using System.Diagnostics;
using System.Numerics;
using ArithmeticOfLongNumbers.Interface;

namespace ArithmeticOfLongNumbers
{
    public class Expression: IExpression
    {
        protected BigInteger number1;
        protected BigInteger number2;
        
        private static TimeSpan overallProcessingTime;

        /// <summary>
        /// Общее время обработки всех операций всех операторов
        /// </summary>
        public TimeSpan OverallProcessingTime
        {
            get { return overallProcessingTime; }
            set { overallProcessingTime += value; }
        }

        static Expression()
        {
            overallProcessingTime = new TimeSpan();
        }

        public Expression()
        {
        }

        public Expression(BigInteger lhs, BigInteger rhs)
        {
            number1 = lhs;
            number2 = rhs;
        }

        public virtual BigInteger Operator()
        {
            return 0;
        }
    }
}
