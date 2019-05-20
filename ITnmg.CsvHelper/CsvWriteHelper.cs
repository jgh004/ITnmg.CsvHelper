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
        private StreamWriter csvStream;

        /// <summary>
        /// 获取或设置通知事件参数
        /// </summary>
        private Action<CsvWriteProgressInfo> dataHandler;

        /// <summary>
        /// 写入时序列化缓存
        /// </summary>
        private StringBuilder writeBuilder = new StringBuilder(1024);

        /// <summary>
        /// 获取每行的字段数
        /// </summary>
        private int columnCount;

        /// <summary>
        /// 获取写入的总行数
        /// </summary>
        internal long TotalRowCount { get; private set; }

        /// <summary>
        /// 获取字段分隔符与限定符, 默认为 CsvFlag.FlagForRFC4180
        /// </summary>
        public CsvFlag Flag { get; private set; } = CsvFlag.CsvFlagForRFC4180;

        /// <summary>
        /// 获取当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0
        /// </summary>
        public int WriteProgressSize { get; private set; } = 1000;

        /// <summary>
        /// 获取字符编码,默认为 Encoding.UTF8
        /// </summary>
        public Encoding DataEncoding { get; private set; } = Encoding.UTF8;

        /// <summary>
        /// 获取取消操作的token, 默认为 CancellationToken.None
        /// </summary>
        public CancellationToken CancelToken { get; private set; } = CancellationToken.None;

        /// <summary>
        /// 写入IO缓存，默认为4M.
        /// </summary>
        public int WriteBufferLength { get; private set; } = 40960;


        /// <summary>
        /// 初始化写入流
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="dataHandler">通知事件参数, 每次通返回自上次通知以来写入的行数. </param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        /// <param name="writeBufferLength">写入 IO 缓存,默认为 4M.</param>
        public CsvWriteHelper( Stream stream, Encoding dataEncoding, CsvFlag flag,  Action<CsvWriteProgressInfo> dataHandler
            , CancellationToken cancelToken, int writeProgressSize = 1000, int writeBufferLength = 40960 )
        {
            if ( stream == null )
            {
                throw new ArgumentNullException( nameof( stream ) );
            }

            if ( dataEncoding == null )
            {
                throw new ArgumentNullException( nameof( dataEncoding ) );
            }

            if ( flag == null )
            {
                throw new ArgumentNullException( nameof( flag ) );
            }

            if ( writeProgressSize <= 0 )
            {
                throw new ArgumentException( "The property 'writeProgressSize' must be greater than 0" );
            }

            if ( writeBufferLength <= 0 )
            {
                throw new ArgumentException( "The property 'writeBufferLength' must be greater than 0" );
            }

            columnCount = 0;
            TotalRowCount = 0L;
            DataEncoding = dataEncoding;
            Flag = flag;
            CancelToken = cancelToken;
            this.dataHandler = dataHandler;
            WriteProgressSize = writeProgressSize;
            WriteBufferLength = writeBufferLength;
            csvStream = new StreamWriter( stream, DataEncoding, WriteBufferLength );
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码初始化写入流
        /// </summary>
        /// <param name="stream">要写入的流</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. </param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        /// <param name="writeBufferLength">写入 IO 缓存,默认为 4M.</param>
        public CsvWriteHelper( Stream stream, CsvFlag flag, Action<CsvWriteProgressInfo> dataHandler
            , int writeProgressSize = 1000, int writeBufferLength = 40960 )
            : this( stream, Encoding.UTF8, flag, dataHandler, CancellationToken.None, writeProgressSize, writeBufferLength )
        {
        }

        /// <summary>
        /// 初始化写入文件
        /// </summary>
        /// <param name="csvFileName">csv 文件路径及名称</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消操作的token</param>
        /// <param name="progress">通知事件参数, 每次通返回自上次通知以来写入的行数. </param>
        /// <param name="writeProgressSize">当写入多少条数据时应触发进度通知事件, 默认为 1000, 此值应大于 0.</param>
        /// <param name="writeBufferLength">写入 IO 缓存,默认为 4M.</param>
        public CsvWriteHelper( string csvFileName, Encoding dataEncoding, CsvFlag flag, Action<CsvWriteProgressInfo> dataHandler
            , CancellationToken cancelToken, int writeProgressSize = 1000, int writeBufferLength = 40960 )
            : this( File.Open( csvFileName, FileMode.Create ), dataEncoding, flag, dataHandler, cancelToken, writeProgressSize, writeBufferLength )
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
        /// <param name="writeBufferLength">写入 IO 缓存,默认为 4M.</param>
        public CsvWriteHelper( string csvFileName, CsvFlag flag, Action<CsvWriteProgressInfo> dataHandler = null
            , int writeProgressSize = 1000, int writeBufferLength = 40960 )
            : this( csvFileName, Encoding.UTF8, flag, dataHandler, CancellationToken.None, writeProgressSize, writeBufferLength )
        {
        }


        /// <summary>
        /// 异步写入单行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <param name="rowData">一行数据,由字段集合组成.</param>
        /// <returns>Task</returns>
        public async Task WriteLineAsync( List<string> rowData )
        {
            if ( CancelToken.IsCancellationRequested )
            {
                CancelToken.ThrowIfCancellationRequested();
            }

            if ( rowData == null )
            {
                throw new ArgumentNullException( nameof( rowData ) );
            }

            //如果写入过一条数据, 则字段数固定. 如果再次写入的字段数不同, 报异常.
            if ( columnCount > 0 && columnCount != rowData.Count )
            {
                throw new ArgumentException( "the rowData count must be equal to " + columnCount.ToString() );
            }

            await csvStream.WriteLineAsync( SerializeRow( rowData ) );
            TotalRowCount++;

            //设置字段数
            if ( columnCount == 0 )
            {
                columnCount = rowData.Count;
            }

            //发送通知
            if ( dataHandler != null )
            {
                //如果取余数=0, 发送通知.
                if ( TotalRowCount % WriteProgressSize == 0L )
                {
                    CsvWriteProgressInfo info = new CsvWriteProgressInfo();
                    info.CurrentRowCount = WriteProgressSize;
                    info.WirteRowCount = TotalRowCount;
                    dataHandler.Invoke( info );
                }
            }
        }

        /// <summary>
        /// 异步写入单行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <typeparam name="T">要写入的数据对象类型</typeparam>
        /// <param name="rowData">要写入的数据对象实例</param>
        /// <param name="convertRowData">处理对象实例,返回字段集合的方法.</param>
        /// <returns>Task</returns>
        public async Task WriteLineAsync<T>( T rowData, Func<T, List<string>> convertRowData ) where T : new()
        {
            if ( convertRowData == null )
            {
                throw new ArgumentNullException( nameof( convertRowData ) );
            }

            await WriteLineAsync( convertRowData.Invoke( rowData ) );
        }

        /// <summary>
        /// 异步写入多行数据, 可多次执行, 之后执行 Close 方法关闭写入流.
        /// </summary>
        /// <typeparam name="T">要写入的数据对象类型</typeparam>
        /// <param name="rowDataList">要写入的数据对象实例集合</param>
        /// <param name="convertRowData">处理对象实例集合,返回包含字段集合的行集合方法.</param>
        /// <returns>Task</returns>
        public async Task WriteAsync<T>( List<T> rowDataList, Func<T, List<string>> convertRowData ) where T : new()
        {
            if ( rowDataList == null )
            {
                throw new ArgumentNullException( nameof( rowDataList ) );
            }

            for ( int i = 0; i < rowDataList.Count; i++ )
            {
                await WriteLineAsync( rowDataList[i], convertRowData );
            }
        }

        /// <summary>
        /// 异步清除缓冲区,将数据写入流.
        /// </summary>
        /// <returns></returns>
        public async Task FlushAsync()
        {
            await csvStream?.FlushAsync();
        }

        /// <summary>
        /// 关闭写入流, 并引发可能的最后一次通知事件.
        /// </summary>
        public void Close()
        {
            try
            {
                if ( csvStream != null )
                {
                    csvStream.Close();

                    if ( dataHandler != null )
                    {
                        //如果记录总数等于通知设定总数, 说明写入结束时刚好是要通知的数量, 但在 WriteLineAsync 方法中已经通知, 所以在这不再通知.
                        if ( TotalRowCount != WriteProgressSize )
                        {
                            CsvWriteProgressInfo info = new CsvWriteProgressInfo();
                            info.CurrentRowCount = TotalRowCount % WriteProgressSize;
                            info.WirteRowCount = TotalRowCount;
                            dataHandler.Invoke( info );
                        }
                    }
                }
            }
            finally
            {
                csvStream = null;
            }
        }


        /// <summary>
        /// 转义 CSV 多行内容, 返回转义后的行集合内容.
        /// </summary>
        /// <param name="lines">转义前的行集合</param>
        /// <returns>转义后的行集合</returns>
        private string SerializeRow( List<string> line )
        {
            writeBuilder.Clear();

            if ( line != null )
            {
                for ( int i = 0; i < line.Count; i++ )
                {
                    writeBuilder.Append( SerializeField( line[i] )  );

                    if ( i + 1 < line.Count )
                    {
                        writeBuilder.Append( Flag.FieldSeparator );
                    }
                }
            }

            return writeBuilder.ToString();
        }

        /// <summary>
        /// 转义字段值。如果字段包含分隔符或 '"' 或 ‘\n’ 或 "\r\n" 或字段分隔符，用双引号将字段包围起来，再將字段中的每个双引号替换为两个双引号。
        /// </summary>
        /// <param name="field">输入字符串</param>
        /// <returns>加上转义符后的字段</returns>
        private string SerializeField( string field )
        {
            string result = field;

            if ( !string.IsNullOrEmpty( field ) )
            {
                if ( result.Contains( Flag.Separator ) || result.Contains( Flag.Qualifier ) || result.Contains( "\r" ) || result.Contains( "\n" ) )
                {
                    result = Flag.Qualifier + result.Replace( Flag.Qualifier, Flag.DoubleQualifier ) + Flag.Qualifier;
                }
            }

            return result;
        }
    }
}
