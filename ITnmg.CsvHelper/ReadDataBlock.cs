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
        /// 块编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Data { get; set; }
    }
}
