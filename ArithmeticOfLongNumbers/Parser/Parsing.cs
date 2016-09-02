using System;
using System.Collections.Generic;
using System.Numerics;
using ArithmeticOfLongNumbers.Operation;

namespace ArithmeticOfLongNumbers.Parser
{
    public class Parsing
    {
        public static Expression expression;

        //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
        static private bool IsDelimeter(char c)
        {
            if ((" ".IndexOf(c) != -1))
                return true;
            return false;
        }

        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if (("+-/*().".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Метод возвращает приоритет оператора
        static private byte GetPriority(char s)
        {
            switch (s)
            {
                //case '(': return 0;
                case '(': return 1;
                case '+':
                case '-':
                    return 3;
                case '*': //return 4;
                case '/':
                    return 4;
                default: return 7;
            }
        }

        //"Входной" метод класса
        /*static public double Calculate(Vector v1)
        {
            //input = SplitVariables(x, input);
            //string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
            double result = Counting(MainWindow.Input, v1, MainWindow.variableList); //Решаем полученное выражение
            return result; //Возвращаем результат
        }*/

        static public string GetExpression(string input)
        {
            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов

            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }
                //Если символ - оператор
                if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                        operStack.Push(input[i]); //Записываем её в стек
                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        //Выписываем все операторы до открывающей скобки в строку
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {
                        if (operStack.Count > 0) //Если в стеке есть элементы
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением

                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        static public BigInteger Counting(string input)
        {
            BigInteger result = 0; //Результат
            Stack<BigInteger> temp = new Stack<BigInteger>(); //Временный стек для решения
            string output = string.Empty; //Строка для хранения выражения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }
                    temp.Push(BigInteger.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //Берем два последних значения из стека
                    BigInteger a = temp.Pop();
                    BigInteger b;
                    bool isUnaryMinus = false;
                    if (temp.Count == 0) { b = 0; isUnaryMinus = true; }
                    else { b = temp.Pop(); }

                    switch (input[i]) //И производим над ними действие, согласно оператору
                    {
                        //case '.': Double.TryParse(Int32.Parse(b.ToString()).ToString() + "." + Int32.Parse(a.ToString()).ToString(), NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-GB"), out result); break;//b + double.Parse("0." + Int32.Parse(a.ToString()).ToString()); break;
                        case '+':
                            expression = new Addition(b, a);
                            result = expression.Operator();
                            break;
                        case '-':
                            if (isUnaryMinus)
                            {
                                expression = new UnaryNegative(b, a); 
                                result = expression.Operator();
                            }
                            else
                            {
                                expression = new Substraction(b, a);
                                result = expression.Operator();
                            }
                            break;
                        case '*':
                            expression = new Multiplication(b, a);
                            result = expression.Operator();
                            break;
                        case '/':
                            expression = new Division(b, a);
                            result = expression.Operator();
                            break;
                            //case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его
        }
    }
}
