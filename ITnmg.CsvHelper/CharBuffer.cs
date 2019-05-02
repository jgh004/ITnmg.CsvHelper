using System;
using System.Collections.Generic;
using System.Text;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// 字符缓存
    /// </summary>
    public class CharBuffer
    {
        /// <summary>
        /// 缓存
        /// </summary>
        public char[] Buffer { get; private set; }

        /// <summary>
        /// 有效数据起始索引
        /// </summary>
        public int StartIndex { get; set; }

        /// <summary>
        /// 有效数据长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 有效数据结束索引
        /// </summary>
        public int EndIndex => StartIndex + Length;

        public CharBuffer( int capacity )
        {
            Buffer = new char[capacity];
        }
    }
}
