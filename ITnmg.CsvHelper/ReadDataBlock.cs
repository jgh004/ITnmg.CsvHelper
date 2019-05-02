using System;
using System.Collections.Generic;
using System.Text;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// 读取数据块
    /// </summary>
    internal class ReadDataBlock
    {
        /// <summary>
        /// 解析出的整行数据
        /// </summary>
        public List<List<string>> Rows { get; set; }

        /// <summary>
        /// 不足一行的数据中的字段
        /// </summary>
        public List<string> Fields { get; set; }

        /// <summary>
        /// 剩余不足一行的数据
        /// </summary>
        public List<char> SubData { get; set; }
    }
}
