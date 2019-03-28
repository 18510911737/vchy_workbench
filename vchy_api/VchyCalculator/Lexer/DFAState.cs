using System;
using System.Collections.Generic;
using System.Text;

namespace VchyCalculator
{
    public enum DFAState:int
    {
        /// <summary>
        ///初态 
        /// </summary>
        S0,
        /// <summary>
        /// 整数
        /// </summary>
        S1,
        /// <summary>
        /// 浮点数
        /// </summary>
        S2,
        /// <summary>
        /// 字母
        /// </summary>
        S3,
        /// <summary>
        /// +
        /// </summary>
        S4,
        /// <summary>
        /// -
        /// </summary>
        S5,
        /// <summary>
        /// *
        /// </summary>
        S6,
        /// <summary>
        ///  /
        /// </summary>
        S7,
        /// <summary>
        ///  %
        /// </summary>
        S8,
        /// <summary>
        /// !
        /// </summary>
        S9,
        /// <summary>
        /// ^
        /// </summary>
        S10,
        /// <summary>
        /// =
        /// </summary>
        S11,
        /// <summary>
        /// (
        /// </summary>
        S12,
        /// <summary>
        /// )
        /// </summary>
        S13,
        /// <summary>
        /// 未知态
        /// </summary>
        SX,
    }
}
