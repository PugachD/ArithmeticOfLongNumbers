#define Timer
using System;
using System.Diagnostics;
using System.Numerics;
using ArithmeticOfLongNumbers.Utils;
using System.Threading;

namespace ArithmeticOfLongNumbers.Operation
{
    public sealed class Addition : Expression
    {
        public Addition(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2) { }

        public override BigInteger Operator()
        {
#if Timer
            Stopwatch sWatch = Stopwatch.StartNew();
#endif
            number1 = number1 + number2;
#if Timer
            sWatch.Stop();
            Interlocked.Increment(ref AdditionStats.Reference.countOperation);
            lock (AdditionStats.Reference)
                AdditionStats.Reference.totalCalculationTime += sWatch.Elapsed;
#endif
            sWatch = null;
            return number1;
        }
    }
}
