using System;
using System.Collections.Generic;
using System.Linq;
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
                //_ps.AddPhraseResult("error", PhraseType.unknown);
                throw new ArgumentException("Expression exception");
            }
        }

        /// <summary>
        /// 词法分析
        /// </summary>
        /// <returns>是否成功</returns>
        public bool Analyze()
        {
            var i = 0;
            var startIndex = 0;
            var endIndex = 0;
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
                        endIndex = i - 1;
                        SavePhrase(startIndex, endIndex);
                        _dfa = DFAState.S3;
                        startIndex = i;
                    }
                    else
                    {
                        endIndex = i - 1;
                        if (CheckString(startIndex, endIndex))
                        {
                            SavePhrase(startIndex, endIndex);
                            startIndex = i;
                        }
                    }
                    if (i + 1 == _chArray.Length)
                    {
                        SavePhrase(startIndex + 1, endIndex);
                    }
                }
                //指示 Unicode 字符是否属于十进制数字类别。
                else if (char.IsDigit(_chArray[i]))
                {
                    if (_dfa == DFAState.S0)
                    {
                        _dfa = DFAState.S1;
                        startIndex = i;
                    }
                    else if (_dfa != DFAState.S1 && _dfa != DFAState.S2)
                    {
                        endIndex = i - 1;
                        if (_dfa == DFAState.S3 && !CheckString(startIndex, endIndex))
                        {
                            return false;
                        }
                        SavePhrase(startIndex, endIndex);
                        _dfa = DFAState.S1;
                        startIndex = i;
                    }

                    if (i + 1 == _chArray.Length)
                    {
                        SavePhrase(startIndex, i);
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
                else if (_chArray[i] == '+' ||
                    _chArray[i] == '-' ||
                    _chArray[i] == '*' ||
                    _chArray[i] == '/' ||
                    _chArray[i] == '^' ||
                    _chArray[i] == '%' ||
                    _chArray[i] == '=' ||
                    _chArray[i] == '(' ||
                    _chArray[i] == ')' ||
                    _chArray[i] == '!')
                {
                    if (_dfa != DFAState.S0)
                    {
                        endIndex = i - 1;
                        if (_dfa == DFAState.S3 && !CheckString(startIndex, endIndex))
                        {
                            return false;
                        }
                        SavePhrase(startIndex, endIndex);
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
                        if (i + 1 == _chArray.Length)
                        {
                            startIndex++;
                            endIndex++;
                            SavePhrase(startIndex, endIndex);
                        }
                    }
                    else if (_chArray[i] == '!')
                    {
                        _dfa = DFAState.S9;
                    }
                    startIndex = i;
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
        /// <param name="startIndex">字符串开始位置</param>
        /// <param name="endIndex">字符串结束位置</param>
        /// <returns>字符串是否匹配规定范围内容的类型</returns>
        private bool CheckString(int startIndex, int endIndex)
        {
            var len = endIndex - startIndex + 1;
            var temp = _sentence.Substring(startIndex, len);
            return Enum.GetNames(typeof(PhraseType)).Any(f => f == temp);
        }

        /// <summary>
        /// 保存词
        /// </summary>
        /// <param name="startIndex">开始位置</param>
        /// <param name="endIndex">结束位置</param>
        private void SavePhrase(int startIndex, int endIndex)
        {
            if (endIndex >= 0 && startIndex >= 0 && endIndex >= startIndex)
            {
                var temp = _sentence.Substring(startIndex, endIndex - startIndex + 1);
                _ps.AddPhraseResult(temp, PhraseTypeConvert.ConvertTo(temp));
            }

        }
    }
}
