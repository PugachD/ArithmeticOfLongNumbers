#define Timer
using ArithmeticOfLongNumbers.Utils;
using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;

namespace ArithmeticOfLongNumbers.Operation
{
    
    public sealed class Substraction : Expression
    {
        public Substraction(BigInteger bigInt1, BigInteger bigInt2) : base(bigInt1, bigInt2) { }

        public override BigInteger Operator()
        {
#if Timer
            Stopwatch sWatch = new Stopwatch();
            sWatch.Start();
#endif
            number1 = number1 - number2;

#if Timer
            sWatch.Stop();

            Interlocked.Increment(ref SubstractionStats.Reference.countOperation);
            lock (SubstractionStats.Reference)
                SubstractionStats.Reference.totalCalculationTime += sWatch.Elapsed;
#endif
            sWatch = null;
            return number1;
        }
    }
}
