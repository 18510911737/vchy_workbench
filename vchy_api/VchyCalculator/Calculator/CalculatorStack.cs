using System;
using System.Collections.Generic;
using System.Text;

namespace VchyCalculator
{
    public class CalculatorStack
    {
        public double Calculator(string expr)
        {
            PhraseStorage ps = new PhraseStorage();
            Lexer l = new Lexer(expr, ref ps);
            var index = 0;
            return Calculator(ps, ref index);
        }
        private double Calculator(PhraseStorage ps, ref int index)
        {
            var numberStack = new Stack<double>();
            var operatorStack = new Stack<PhraseType>();
            while (index < ps._strs.Count)
            {
                var val = ps._strs[index];
                var oper = ps._types[index];
                var flag = operatorStack.Count == 0;
                //number
                if (oper == PhraseType.number)
                {
                    numberStack.Push(Convert.ToDouble(val));
                }
                //+ -
                else if (oper == PhraseType.plus || oper == PhraseType.minus)
                {
                    operatorStack.Push(oper);
                }
                // * /
                else if (oper == PhraseType.mutiple || oper == PhraseType.divide)
                {
                    var nextOper = ps._types[index + 1];
                    index++;
                    var prevNumber = numberStack.Pop();
                    var nextNumber = default(double);
                    var result = default(double);
                    if (nextOper == PhraseType.number)
                    {
                        nextNumber = Convert.ToDouble(ps._strs[index]);
                    }
                    else
                    {
                        nextNumber = Calculator(ps, ref index);
                    }
                    result = oper == PhraseType.mutiple ? prevNumber * nextNumber : prevNumber / nextNumber;
                    numberStack.Push(result);
                }
                // (
                else if (oper == PhraseType.leftbracket)
                {
                    index++;
                    numberStack.Push(Calculator(ps, ref index));
                }
                // )
                else if (oper == PhraseType.rightbracket)
                {
                    //计算堆栈结果
                    var result = Calculator(numberStack, operatorStack);
                    numberStack.Push(result);
                }
                index++;
            }
            return Calculator(numberStack, operatorStack);
        }

        private double Calculator(Stack<double> numberStack, Stack<PhraseType> operatorStack)
        {
            var count = operatorStack.Count;
            for (int i = 0; i < count; i++)
            {
                var oper = operatorStack.Pop();
                var right = numberStack.Pop();
                var left = numberStack.Pop();
                //+
                if (oper == PhraseType.plus)
                {
                    numberStack.Push(left + right);
                }
                //-
                else if (oper == PhraseType.minus)
                {
                    numberStack.Push(left - right);
                }
            }
            return numberStack.Pop();
        }
    }
}
