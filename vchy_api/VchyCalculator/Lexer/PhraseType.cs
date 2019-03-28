using System;
using System.Collections.Generic;
using System.Text;

namespace VchyCalculator
{
    public enum PhraseType : int
    {
        unknown = 0,
        ln = 1,
        lg = 2,
        log = 3,
        pow = 4,        //a^b
        cbrt = 6,       //a^-0.5
        sbrt = 7,       //a^-1/3
        fact = 8,
        sin = 10,
        cos = 11,
        asin = 12,
        acos = 13,
        tg = 14,
        ctg = 15,
        atg = 16,
        actg = 17,
        plus = 18,
        minus = 19,
        mutiple = 20,
        divide = 21,
        mod = 23,
        leftbracket = 24,       //(
        rightbracket = 25,  //)
        ans = 26,       //variable ans
        sto = 27,       //save to var
        clr = 28,       //clear vars
        ax = 29,        //variable a
        bx = 30,        //variable b
        cx = 31,        //variable c
        dx = 32,        //variable d
        ex = 33,        //variable e
        fx = 34,        //variable f
        e = 35,
        pi = 36,
        number = 37,
        sharp = 38
    }

    public static class PhraseTypeConvert
    {
        /// <summary>
        /// 转换字符串为所对应的词类
        /// </summary>
        /// <param name="str">词字符串</param>
        /// <returns>词类</returns>
        public static PhraseType ConvertTo(string str)
        {
            switch (str)
            {
                case "sin": return PhraseType.sin;
                case "cos": return PhraseType.cos;
                case "ln": return PhraseType.ln;
                case "lg": return PhraseType.lg;
                case "log": return PhraseType.log;
                case "^": return PhraseType.pow;
                case "cbrt": return PhraseType.cbrt;
                case "sbrt": return PhraseType.sbrt;
                case "asin": return PhraseType.asin;
                case "acos": return PhraseType.acos;
                case "!": return PhraseType.fact;
                case "tg": return PhraseType.tg;
                case "ctg": return PhraseType.ctg;
                case "atg": return PhraseType.atg;
                case "actg": return PhraseType.actg;
                case "+": return PhraseType.plus;
                case "-": return PhraseType.minus;
                case "*": return PhraseType.mutiple;
                case "/": return PhraseType.divide;
                case "%": return PhraseType.mod;
                case "(": return PhraseType.leftbracket;
                case ")": return PhraseType.rightbracket;
                case "#": return PhraseType.sharp;
                case "ans": return PhraseType.ans;
                case "sto": return PhraseType.sto;
                case "clr": return PhraseType.clr;
                case "A": return PhraseType.ax;
                case "B": return PhraseType.bx;
                case "C": return PhraseType.cx;
                case "D": return PhraseType.dx;
                case "E": return PhraseType.ex;
                case "F": return PhraseType.fx;
                case "e": return PhraseType.e;
                case "PI": return PhraseType.pi;
                default: return PhraseType.number;
            }
        }
    }
}
