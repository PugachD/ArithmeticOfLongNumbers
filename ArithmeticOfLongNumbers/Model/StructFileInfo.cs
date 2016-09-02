using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticOfLongNumbers.Model
{
    struct StructFileInfo
    {
        public string expression;
        public int numberString;

        public StructFileInfo(string _expression, int _numberString)
        {
            expression = _expression;
            numberString = _numberString;
        }
    }
}
