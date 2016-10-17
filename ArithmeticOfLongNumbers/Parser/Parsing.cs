#define Inheritance
using System;
using System.Collections.Generic;
using System.Numerics;
using ArithmeticOfLongNumbers.Operation;
using ArithmeticOfLongNumbers.Model;
using ArithmeticOfLongNumbers.Utils;
using System.Text;

namespace ArithmeticOfLongNumbers.Parser
{
    public struct Parsing
    {
        private const char unaryMinus = '_';
        private string RPN;
        
        //Метод возвращает true, если проверяемый символ - оператор
        private bool IsOperator(char с)
        {
            if (("+-/*()".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Метод возвращает приоритет оператора
        private byte GetPriority(char s)
        {
            switch (s)
            {
                //case '(': return 0;
                case '(': return 1;
                case '+':
                case '-':
                case unaryMinus:
                    return 2;
                case '*': //return 4;
                case '/':
                    return 3;
                default: return 7;
            }
        }

        public void GetExpression(string input)
        {
            bool prevSymbolOpBrace = false;
            char inputI;
            StringBuilder output = new StringBuilder();
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов
            short Length = (short)input.Length;
            output.Capacity = Length;

            for (int i = 0; i < Length; i++) //Для каждого символа в входной строке
            {
                inputI = input[i];
                //Разделители пропускаем
                if (inputI == ' ')
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(inputI)) //Если цифра
                {
                    prevSymbolOpBrace = false;
                    //Читаем до разделителя или оператора, что бы получить число
                    while (input[i] != ' ' && !IsOperator(input[i]))
                    {
                        output.Append(input[i]); //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу
                        //inputI = input[i];
                        if (i == Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output.Append(" "); //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                    inputI = input[i];
                }
                //Если символ - оператор
                else if (IsOperator(inputI)) //Если оператор
                {
                    if (inputI == '-')
                    {
                        if (i == 0 || prevSymbolOpBrace)
                        {
                            operStack.Push(unaryMinus);
                            continue;
                        }
                    }
                    prevSymbolOpBrace = false;
                    switch (inputI)
                    {
                        case '(': //Если символ - открывающая скобка
                            prevSymbolOpBrace = true;
                            operStack.Push(inputI); //Записываем её в стек
                            break;
                        case ')': //Если символ - закрывающая скобка
                                  //Выписываем все операторы до открывающей скобки в строку
                            char s = operStack.Pop();

                            while (s != '(')
                            {
                                output.Append(s.ToString());
                                output.Append(' ');
                                s = operStack.Pop();
                            }
                            break;
                        default: //Если любой другой оператор
                            while (operStack.Count > 0) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                            {
                                if (GetPriority(inputI) <= GetPriority(operStack.Peek()))
                                {
                                    output.Append(operStack.Pop()); //То добавляем последний оператор из стека в строку с выражением
                                    output.Append(" ");
                                }
                                else
                                    break;
                            }

                            operStack.Push(inputI); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека
                            break;
                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
            {
                output.Append(operStack.Pop());
                output.Append(" ");
            }
            operStack = null;
            RPN = output.ToString(); //Возвращаем выражение в постфиксной записи
        }

        public BigInteger Counting()
        {
            //BigInteger result = 0; //Результат
            Stack<BigInteger> temp = new Stack<BigInteger>(); //Временный стек для решения
            BigInteger firstNumber = 0, secondNumber;
            StringBuilder a;
            Expression expression;// = new Addition();
            short Length = (short)RPN.Length;
            char RPNI;

            for (int i = 0; i < Length; i++) //Для каждого символа в строке
            {
                RPNI = RPN[i];
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(RPNI))
                {
                    a = new StringBuilder(Length);

                    while (RPNI != ' ' && !IsOperator(RPNI) && RPNI != unaryMinus) //Пока не разделитель
                    {
                        a.Append(RPNI); //Добавляем
                        i++;
                        RPNI = RPN[i];

                        if (i == Length) break;
                    }
                    temp.Push(BigInteger.Parse(a.ToString())); //Записываем в стек
                    i--;
                    RPNI = RPN[i];
                    a = null;
                }
                else if (IsOperator(RPNI) || RPNI == unaryMinus) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    firstNumber = temp.Pop();
                    if (temp.Count == 0 || RPNI == unaryMinus) { secondNumber = 0; }
                    else { secondNumber = temp.Pop(); }

                    switch (RPNI) //И производим над ними действие, согласно оператору
                    {
                        case '+':
#if Inheritance
                            //lock (expression)
                            {
                                expression = new Addition(secondNumber, firstNumber);
                                firstNumber = expression.Operator();
                            }
#else
                            firstNumber = firstNumber + secondNumber;
#endif
                            break;
                        case '-':
#if Inheritance
                                //lock (expression)
                                {
                                    expression = new Substraction(secondNumber, firstNumber);
                                firstNumber = expression.Operator();
                                }
#else
                            firstNumber = -firstNumber;
#endif
                                break;
                        case unaryMinus:
#if Inheritance
                                //lock (expression)
                                {
                                    expression = new UnaryNegative(secondNumber, firstNumber);
                                firstNumber = expression.Operator();
                                }
#else
                            firstNumber =  secondNumber - firstNumber;
#endif
                            break;
                        case '*':
#if Inheritance
                            //lock (expression)
                            {
                                expression = new Multiplication(secondNumber, firstNumber);
                                firstNumber = expression.Operator();
                            }
#else
                            firstNumber = firstNumber * secondNumber;
#endif
                            break;
                        case '/':
#if Inheritance
                            //lock (expression)
                            {
                                expression = new Division(secondNumber, firstNumber);
                                firstNumber = expression.Operator();
                            }
#else
                            firstNumber = secondNumber/ firstNumber;
#endif
                            break;
                    }
                    temp.Push(firstNumber); //Результат вычисления записываем обратно в стек
                }
            }
            temp = null;
            expression = null;
            return firstNumber;//temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}
