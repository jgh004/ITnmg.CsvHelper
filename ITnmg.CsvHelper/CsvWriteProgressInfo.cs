using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// 写入 csv 文件时, 进度报告中的数据.
    /// </summary>
    public class CsvWriteProgressInfo
    {
        /// <summary>
        /// 获取当前通知事件中写入的行数
        /// </summary>
        public long CurrentRowCount
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取已写入的总行数
        /// </summary>
        public long WirteRowCount
        {
            get;
            internal set;
        }
    }
}
