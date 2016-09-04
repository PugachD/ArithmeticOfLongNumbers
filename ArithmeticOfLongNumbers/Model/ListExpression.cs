using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArithmeticOfLongNumbers.Operation;

namespace ArithmeticOfLongNumbers.Model
{
    public class ListExpression
    {
        private Expression[] listExpression;

        public Expression this[int i]
        {
            set
            {
                listExpression[i] = value;
            }
            get
            {
                return listExpression[i];
            }
        }
    }
}
