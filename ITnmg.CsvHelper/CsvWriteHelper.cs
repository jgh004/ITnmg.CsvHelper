using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// CSV 写入类
    /// </summary>
    public class CsvWriteHelper
    {
        /// <summary>
        /// 读取或写入 CSV 数据的流
        /// </summary>
        private StreamWriter CsvStream;

        /// <summary>
        /// 获取或设置通知事件参数
        /// </summary>
        private IProgress<CsvWriteProgressInfo> Progress;


        /// <summary>
        /// 获取每行的字段数
        /// </summary>
        public int ColumnCount
        {
            get; private set;
        }

        /// <summary>
        /// 获取写入的总行数
        /// </summary>
        public long TotalRowCount
        {
            get; private set;
        }

        /// <summary>
        /// 获取字段分隔符与限定符, 默认为 CsvFlag.FlagForRFC4180
        /// </summary>
        public CsvFlag Flag
        {
            get; private set;
        }

        /// <summary>
        /// 获取当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0
        /// </summary>
        public int WriteProgressSize
        {
            get; private set;
        }

        /// <summary>
        /// 获取字符编码,默认为 Encoding.UTF8
        /// </summary>
        public Encoding DataEncoding
        {
            get; private set;
        }

        /// <summary>
        /// 获取取消操作的token, 默认为 CancellationToken.None
        /// </summary>
        public CancellationToken CancelToken
        {
            get; private set;
        }


        /// <summary>
        /// 初始化写入流
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( Stream stream, Encoding dataEncoding, CsvFlag flag
            , CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
        {
            if ( stream == null )
            {
                throw new ArgumentNullException( "stream" );
            }

            if ( dataEncoding == null )
            {
                throw new ArgumentNullException( "dataEncoding" );
            }

            if ( flag == null )
            {
                throw new ArgumentNullException( "flag" );
            }

            if ( progress != null )
            {
                if ( writeProgressSize <= 0 )
                {
                    throw new ArgumentException( "The property 'writeProgressSize' must be greater than 0" );
                }
            }

            this.ColumnCount = 0;
            this.TotalRowCount = 0L;
            this.DataEncoding = dataEncoding;
            this.Flag = flag;
            this.CancelToken = cancelToken;
            this.Progress = progress;
            this.WriteProgressSize = writeProgressSize;
            this.CsvStream = new StreamWriter( stream, this.DataEncoding );
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码初始化写入流
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( Stream stream, CsvFlag flag, CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( stream, Encoding.UTF8, flag, cancelToken, progress, writeProgressSize )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码, CsvFlag.FlagForRFC4180 分隔符与限定符 初始化写入流.
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( Stream stream, CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( stream, Encoding.UTF8, new CsvFlag(), cancelToken, progress, writeProgressSize )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码, CsvFlag.FlagForRFC4180 分隔符与限定符 初始化写入流, 且不允许取消操作.
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( Stream stream, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( stream, CancellationToken.None, progress, writeProgressSize )
        {
        }


        /// <summary>
        /// 初始化写入文件
        /// </summary>
        /// <param name="csvFileName">csv 文件路径及名称</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( string csvFileName, Encoding dataEncoding, CsvFlag flag
            , CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( File.Open( csvFileName, FileMode.Create ), dataEncoding, flag, cancelToken, progress, writeProgressSize )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码初始化写入文件
        /// </summary>
        /// <param name="csvFileName">csv 文件路径及名称</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( string csvFileName, CsvFlag flag, CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( csvFileName, Encoding.UTF8, flag, cancelToken, progress, writeProgressSize )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码, CsvFlag.FlagForRFC4180 分隔符与限定符 初始化写入文件.
        /// </summary>
        /// <param name="csvFileName">csv 文件路径及名称</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( string csvFileName, CancellationToken cancelToken, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( csvFileName, Encoding.UTF8, new CsvFlag(), cancelToken, progress, writeProgressSize )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码, CsvFlag.FlagForRFC4180 分隔符与限定符 初始化写入文件, 且不允许取消操作.
        /// </summary>
        /// <param name="csvFileName">csv 文件路径及名称</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. 默认为 null, 表示不通知.</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        public CsvWriteHelper( string csvFileName, IProgress<CsvWriteProgressInfo> progress = null, int writeProgressSize = 1000 )
            : this( csvFileName, CancellationToken.None, progress, writeProgressSize )
        {
        }



        /// <summary>
        /// 异步写入单行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <typeparam name="T">要写入的数据对象类型</typeparam>
        /// <param name="rowData">要写入的数据对象实例</param>
        /// <param name="expression">处理对象实例,返回字段集合的方法.</param>
        /// <returns>Task</returns>
        public async Task WriteLineAsync<T>( T rowData, Func<T, List<string>> expression ) where T : new()
        {
            if ( this.CancelToken.IsCancellationRequested )
            {
                this.CancelToken.ThrowIfCancellationRequested();
            }

            if ( rowData == null )
            {
                throw new ArgumentNullException( "rowData" );
            }

            if ( expression == null )
            {
                throw new ArgumentNullException( "expression" );
            }

            //取转换后的行数据
            var row = expression.Invoke( rowData );

            //如果写入过一条数据, 则字段数固定. 如果再次写入的字段数不同, 报异常.
            if ( this.ColumnCount > 0 && this.ColumnCount != row.Count )
            {
                throw new ArgumentException( "the rowData count must be equal to " + ColumnCount.ToString() );
            }

            List<string> rows = this.SerializeRows( row );
            await this.CsvStream.WriteLineAsync( rows[0] );
            this.TotalRowCount++;

            //设置字段数
            if ( this.ColumnCount == 0 )
            {
                this.ColumnCount = row.Count;
            }

            //发送通知
            if ( Progress != null )
            {
                //如果取余数=0, 发送通知.
                if ( TotalRowCount % this.WriteProgressSize == 0L )
                {
                    CsvWriteProgressInfo info = new CsvWriteProgressInfo();
                    info.CurrentRowCount = this.WriteProgressSize;
                    info.WirteRowCount = this.TotalRowCount;
                    Progress.Report( info );
                }
            }
        }

        /// <summary>
        /// 异步写入单行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <param name="rowData">一行数据,由字段集合组成.</param>
        /// <returns>Task</returns>
        public async Task WriteLineAsync( List<string> rowData )
        {
            await WriteLineAsync( rowData, f=>
            {
                return f;
            } );
        }

        /// <summary>
        /// 异步写入多行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <typeparam name="T">要写入的数据对象类型</typeparam>
        /// <param name="rowDataList">要写入的数据对象实例集合</param>
        /// <param name="expression">处理对象实例集合,返回包含字段集合的行集合方法.</param>
        /// <returns>Task</returns>
        public async Task WriteAsync<T>( List<T> rowDataList, Func<T, List<string>> expression ) where T : new()
        {
            if ( rowDataList == null )
            {
                throw new ArgumentNullException( "rowDataList" );
            }

            foreach ( var row in rowDataList )
            {
                //大量循环, 所以用 ConfigureAwait( false ).
                await WriteLineAsync( row, expression ).ConfigureAwait( false );
            }
        }

        /// <summary>
        /// 异步写入多行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <param name="rowDataList">行数据集合</param>
        /// <returns>Task</returns>
        public async Task WriteAsync( List<List<string>> rowDataList )
        {
            await WriteAsync( rowDataList, f =>
            {
                return f;
            } );
        }

        /// <summary>
        /// 异步清除缓冲区,将数据写入流.
        /// </summary>
        /// <returns></returns>
        public async Task FlushAsync()
        {
            if ( this.CsvStream != null )
            {
                await this.CsvStream.FlushAsync();
            }
        }

        /// <summary>
        /// 关闭写入流, 并引发可能的最后一次通知事件.
        /// </summary>
        public void Close()
        {
            if ( this.CsvStream != null )
            {
                this.CsvStream.Close();
                this.CsvStream = null;

                if ( Progress != null )
                {
                    //如果记录总数等于通知设定总数, 说明写入结束时刚好是要通知的数量, 但在 WriteLineAsync 方法中已经通知, 所以在这不再通知.
                    if ( TotalRowCount != WriteProgressSize )
                    {
                        CsvWriteProgressInfo info = new CsvWriteProgressInfo();
                        info.CurrentRowCount = this.TotalRowCount % this.WriteProgressSize;
                        info.WirteRowCount = this.TotalRowCount;
                        Progress.Report( info );
                    }
                }
            }
        }


        /// <summary>
        /// 转义 CSV 多行内容, 返回转义后的行集合内容.
        /// </summary>
        /// <param name="lines">转义前的行集合</param>
        /// <returns>转义后的行集合</returns>
        private List<string> SerializeRows( params List<string>[] lines )
        {
            List<string> result = new List<string>();

            if ( lines != null )
            {
                foreach ( var line in lines )
                {
                    StringBuilder sb = new StringBuilder( 2048 );

                    for ( int i = 0; i < line.Count; i++ )
                    {
                        sb.Append( SerializeField( line[i] ) );

                        if ( i + 1 < line.Count )
                        {
                            sb.Append( this.Flag.FieldSeparator );
                        }
                    }

                    if ( sb.Length > 0 )
                    {
                        result.Add( sb.ToString() );
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 转义字段值。如果字段包含分隔符或 '"' 或 ‘\n’ 或 "\r\n" 或字段分隔符，用双引号将字段包围起来，再將字段中的每个双引号替换为两个双引号。
        /// </summary>
        /// <param name="field">输入字符串</param>
        /// <returns>加上转义符后的字段</returns>
        private string SerializeField( string field )
        {
            string result = field;

            if ( string.IsNullOrEmpty( field ) )
            {
                result = "";
            }
            else
            {
                if ( result.IndexOf( Flag.FieldSeparator ) >= 0 || result.IndexOf( Flag.FieldQualifier ) >= 0 || result.IndexOf( '\r' ) >= 0 || result.IndexOf( "\n" ) >= 0 )
                {
                    result = this.Flag.Qualifier + result.Replace( this.Flag.Qualifier, this.Flag.DoubleQualifier ) + this.Flag.Qualifier;
                }
            }

            return result;
        }
    }
}
