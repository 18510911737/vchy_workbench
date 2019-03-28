using System;
using System.Collections.Generic;
using System.Text;

namespace VchyCalculator
{
    /// <summary>
    /// 词法分析
    /// </summary>
    public class Lexer
    {
        private DFAState _dfa;
        private char[] _chArray;
        private string _sentence;
        private PhraseStorage _ps;
        private bool _success = true;

        public Lexer(string sentence, ref PhraseStorage ps)
        {
            _ps = ps;
            _ps.ClearResult();
            _sentence = sentence;
            _chArray = _sentence.ToCharArray();
            _success = true;
            if (!Analyze())
            {
                _ps.AddPhraseResult("error", PhraseType.unknown);
            }
        }

        /// <summary>
        /// 词法分析
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Analyze()
        {
            var i = 0;
            var startpos = 0;
            var endpos = 0;
            _dfa = DFAState.S0;//设置初态
            while (i < _chArray.Length)
            {
                //未知态处理
                if (_dfa == DFAState.SX)
                {
                    return _success = false;
                }
                //指示 Unicode 字符是否属于 Unicode 字母类别。
                if (char.IsLetter(_chArray[i]))
                {
                    if (_dfa == DFAState.S0)
                    {
                        _dfa = DFAState.S3;//初态变字母串
                    }
                    else if (_dfa != DFAState.S3)
                    {
                        //处理前一个词
                        endpos = i - 1;
                        SavePhrase(startpos, endpos);
                        _dfa = DFAState.S3;
                        startpos = i;
                    }
                    else
                    {
                        endpos = i - 1;
                        if (CheckString(startpos, endpos))
                        {
                            SavePhrase(startpos, endpos);
                            startpos = i;
                        }
                    }
                    if (i + 1 == _chArray.Length)
                    {
                        SavePhrase(startpos + 1, endpos);
                    }
                }
                else if (char.IsDigit(_chArray[i]))//指示 Unicode 字符是否属于十进制数字类别。
                {
                    if (_dfa == DFAState.S0)
                    {
                        _dfa = DFAState.S1;
                        startpos = i;
                    }
                    else if (_dfa != DFAState.S1 && _dfa != DFAState.S2)
                    {
                        endpos = i - 1;
                        if (_dfa == DFAState.S3 && !CheckString(startpos, endpos))
                        {
                            return false;
                        }
                        SavePhrase(startpos, endpos);
                        _dfa = DFAState.S1;
                        startpos = i;
                    }

                    if (i + 1 == _chArray.Length)
                    {
                        SavePhrase(startpos, startpos);
                    }
                }
                else if (_chArray[i] == '.')
                {
                    if (i + 1 == _chArray.Length)
                    {
                        return _success = false;
                    }
                    if (_dfa == DFAState.S0 || _dfa == DFAState.S1)
                    {
                        _dfa = DFAState.S2;
                    }
                    else
                    {
                        _dfa = DFAState.SX;
                    }
                }
                else if (_chArray[i] == '+' || _chArray[i] == '-' || _chArray[i] == '*' || _chArray[i] == '/' || _chArray[i] == '^' || _chArray[i] == '%' || _chArray[i] == '=' || _chArray[i] == '(' || _chArray[i] == ')' || _chArray[i] == '!')
                {
                    if (_dfa != DFAState.S0)
                    {
                        endpos = i - 1;
                        if (_dfa == DFAState.S3 && !CheckString(startpos, endpos))
                        {
                            return false;
                        }
                        SavePhrase(startpos, endpos);
                    }
                    if (_chArray[i] == '+')
                    {
                        _dfa = DFAState.S4;
                    }
                    else if (_chArray[i] == '-')
                    {
                        _dfa = DFAState.S5;
                    }
                    else if (_chArray[i] == '*')
                    {
                        _dfa = DFAState.S6;
                    }
                    else if (_chArray[i] == '/')
                    {
                        _dfa = DFAState.S7;
                    }
                    else if (_chArray[i] == '=')
                    {
                        _dfa = DFAState.S11;
                    }
                    else if (_chArray[i] == '%')
                    {
                        _dfa = DFAState.S8;
                    }
                    else if (_chArray[i] == '^')
                    {
                        _dfa = DFAState.S10;
                    }
                    else if (_chArray[i] == '(')
                    {
                        _dfa = DFAState.S12;
                    }
                    else if (_chArray[i] == ')')
                    {
                        _dfa = DFAState.S13;
                    }
                    else if (_chArray[i] == '!')
                    {
                        _dfa = DFAState.S9;
                    }
                    startpos = i;
                }
                else
                {
                    _dfa = DFAState.SX;
                }
                i++;
            }
            return true;
        }

        /// <summary>
        /// 字符串类型检查
        /// </summary>
        /// <param name="startpos">字符串开始位置</param>
        /// <param name="endpos">字符串结束位置</param>
        /// <returns>字符串是否匹配规定范围内容的类型</returns>
        private bool CheckString(int startpos, int endpos)
        {
            var len = endpos - startpos + 1;
            var temp = _sentence.Substring(startpos, len);
            if (len == 1)
            {
                switch (temp)
                {
                    case "e":
                        return true;
                }
            }
            else if (len == 2)
            {
                switch (temp)
                {
                    case "tg":
                    case "ax":
                    case "bx":
                    case "cx":
                    case "dx":
                    case "ex":
                    case "fx":
                    case "pi":
                        return true;
                }
            }
            else if (len == 3)
            {
                switch (temp)
                {
                    case "cos":
                    case "sin":
                    case "ctg":
                    case "atg":
                    case "ans":
                    case "clr":
                    case "sto":
                        return true;
                }
            }
            else if (len == 4)
            {
                switch (temp)
                {
                    case "acos":
                    case "asin":
                    case "actg":
                    case "sbrt":
                    case "cbrt":
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保存词
        /// </summary>
        /// <param name="startpos">开始位置</param>
        /// <param name="endpos">结束位置</param>
        private void SavePhrase(int startpos, int endpos)
        {

            if (endpos >= 0 && startpos >= 0 && endpos >= startpos)
            {
                var temp = _sentence.Substring(startpos, endpos - startpos + 1);
                _ps.AddPhraseResult(temp, PhraseTypeConvert.ConvertTo(temp));
            }

        }
    }
}
