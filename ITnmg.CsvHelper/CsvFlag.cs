using System;
using System.Collections.Generic;
using System.Text;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// CSV 字段分隔符与限定符
    /// </summary>
    public class CsvFlag
    {
        private string qualifier;
        private string doubleQualifier;
        private char fieldQualifier;

        /// <summary>
        /// 获取字段限定符的字符串表示, 用于在序列化 CSV 字段时快速读取此值, 避免过于频繁的转换类型, 提高效率.
        /// </summary>
        internal string Qualifier
        {
            get
            {
                return this.qualifier;
            }
        }

        /// <summary>
        /// 获取连续两个字段限定符的字符串表示, 用于在序列化 CSV 字段时快速读取此值, 避免过于频繁的转换类型, 提高效率.
        /// </summary>
        internal string DoubleQualifier
        {
            get
            {
                return this.doubleQualifier;
            }
        }

        /// <summary>
        /// 获取字段分隔符
        /// </summary>
        public char FieldSeparator
        {
            get; private set;
        }

        /// <summary>
        /// 获取字段含有特殊字符时使用的限定字符
        /// </summary>
        public char FieldQualifier
        {
            get
            {
                return this.fieldQualifier;
            }
            private set
            {
                this.fieldQualifier = value;
                this.qualifier = value.ToString();
                this.doubleQualifier = new string( value, 2 );
            }
        }


        /// <summary>
        /// 使用指定的分隔符与限定符创建实例.
        /// </summary>
        /// <param name="separator">字段分隔符, 默认为 RFC4180 中定义的 ','</param>
        /// <param name="enclosed">字段限定符, 默认为 RFC4180 中定义的 '"'</param>
        public CsvFlag( char separator = ',', char enclosed = '"' )
        {
            this.FieldQualifier = enclosed;
            this.FieldSeparator = separator;
        }
    }
}
