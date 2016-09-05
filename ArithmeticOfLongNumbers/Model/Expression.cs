using System;
using System.Diagnostics;
using System.Numerics;
using ArithmeticOfLongNumbers.Interface;

namespace ArithmeticOfLongNumbers
{
    public abstract class Expression: IExpression
    {
        protected BigInteger number1;
        protected BigInteger number2;
        
        private TimeSpan overallProcessingTime;

        /// <summary>
        /// Общее время обработки всех операций всех операторов
        /// </summary>
        public TimeSpan GetOverallProcessingTime
        {
            get { return overallProcessingTime; }
            set { overallProcessingTime = value; }
        }

        public void IncrementOverallProcessingTime(TimeSpan value)
        {
            overallProcessingTime += value;
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

        public abstract BigInteger Operator();
    }
}
