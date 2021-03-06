﻿using System.Numerics;
using ArithmeticOfLongNumbers.Interface;
using ArithmeticOfLongNumbers.Utils;

namespace ArithmeticOfLongNumbers
{
    public abstract class Expression: IExpression
    {
        protected BigInteger number1;
        protected BigInteger number2;

        public Expression(BigInteger lhs, BigInteger rhs)
        {
            number1 = lhs;
            number2 = rhs;
        }

        public abstract BigInteger Operator();
    }
}
