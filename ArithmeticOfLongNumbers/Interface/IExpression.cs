using System;
using System.Numerics;

namespace ArithmeticOfLongNumbers.Interface
{
    public interface IExpression
    {
        TimeSpan GetOverallProcessingTime { get; set; }

        void IncrementOverallProcessingTime(TimeSpan value);
        BigInteger Operator();
    }
}
