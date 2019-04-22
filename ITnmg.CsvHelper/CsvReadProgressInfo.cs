using System.Collections.Generic;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// 读取 csv 文件时, 进度报告中的数据.
    /// </summary>
    /// <typeparam name="T">每行数据要转换成的实体类</typeparam>
    public class CsvReadProgressInfo<T> where T : new()
    {
        /// <summary>
        /// 获取是否读取完毕
        /// </summary>
        public bool IsComplete
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取列标题集合, 如果指定了将 csv 第一行做为列标题, 则返回第一行的数据; 如果没有指定, 则返回各列的索引.
        /// </summary>
        public List<string> ColumnNames
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取当前批次的数据行集合
        /// </summary>
        public List<T> CurrentRowsData
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取已读取的字节数
        /// </summary>
        public long ReadBytes
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取总字节数
        /// </summary>
        public long TotalBytes
        {
            get;
            internal set;
        }

        /// <summary>
        /// 获取当前进度(已读字节数 / 总字节数)
        /// </summary>
        public decimal ProgressValue
        {
            get
            {
                return this.TotalBytes <= 0 || this.ReadBytes < 0 ? 0 : this.ReadBytes / (decimal)this.TotalBytes * 100;
            }
        }


        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CsvReadProgressInfo()
        {
            this.IsComplete = false;
            this.ColumnNames = new List<string>();
            this.CurrentRowsData = new List<T>();
            this.ReadBytes = 0L;
            this.TotalBytes = 0L;
        }
    }
}
