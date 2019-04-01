using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace VchyCalculator
{
    public class PhraseStorage
    {
        /// <summary>
        /// 分词结果保存
        /// </summary>
        public List<string> _strs = new List<string>();

        /// <summary>
        /// 分词类型保存
        /// </summary>
        public List<PhraseType> _types = new List<PhraseType>();

        /// <summary>
        /// 清除缓存结果
        /// </summary>
        public void ClearResult()
        {
            _strs.Clear();
            _types.Clear();
        }

        public void RemoveFirst()
        {
            _strs.RemoveAt(0);
            _types.RemoveAt(0);
        }


        /// <summary>
        /// 添加词和词类
        /// </summary>
        /// <param name="phrase">词</param>
        /// <param name="pt">词类</param>
        public void AddPhraseResult(string phrase, PhraseType pt)
        {
            _strs.Add(phrase);
            _types.Add(pt);
        }
    }
}
