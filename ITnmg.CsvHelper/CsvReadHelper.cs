/* Description:	CSV文件读写类，字段以","分隔，以‘"’为限定符,也可自定义分隔符与限定符.
 *				IETF标准	https://tools.ietf.org/html/rfc4180
 * Creator:		ITnmg
 * Create date:	2011.12.01
 * Modified date:	2016.09.03
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ITnmg.CsvHelper
{
    /// <summary>
    /// CSV 读取类
    /// </summary>
    public class CsvReadHelper
    {
        /// <summary>
        /// 读取或写入 CSV 数据的流
        /// </summary>
        private StreamReader csvStream;

        /// <summary>
        /// 读取缓存长度
        /// </summary>
        private int readBufferLength = 40960;

        /// <summary>
        /// 取消用token
        /// </summary>
        public CancellationToken CancelToken { get; private set; } = CancellationToken.None;

        /// <summary>
        /// 获取是否将第一行数据做为列标题, 默认为 true.
        /// </summary>
        public bool FirstRowIsHead { get; private set; } = true;

        /// <summary>
        /// csv 字段分隔符与限定符, default: CsvFlag.CsvFlagForRFC4180.
        /// </summary>
        public CsvFlag Flag { get; private set; } = CsvFlag.CsvFlagForRFC4180;

        /// <summary>
        /// 字符编码,默认为 UTF8.
        /// </summary>
        public Encoding DataEncoding { get; private set; } = Encoding.UTF8;


        /// <summary>
        /// 初始化读取流
        /// </summary>
        /// <param name="stream">要读取的流</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消用token</param>
        /// <param name="firstRowIsHead">是否将第一行数据做为标题行</param>
        /// <param name="readBufferLength">流读取缓冲大小, 默认为 40960 字节.</param>
        public CsvReadHelper( Stream stream, Encoding dataEncoding, CsvFlag flag, CancellationToken cancelToken, bool firstRowIsHead = true, int readBufferLength = 40960 )
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

            DataEncoding = dataEncoding;
            Flag = flag;
            CancelToken = cancelToken;
            FirstRowIsHead = firstRowIsHead;
            this.readBufferLength = readBufferLength;
            csvStream = new StreamReader( stream, DataEncoding, false, this.readBufferLength );
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码初始化读取流
        /// </summary>
        /// <param name="stream">要读取的流</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="firstRowIsHead">是否将第一行数据做为标题行</param>
        /// <param name="readBufferLength">流读取缓冲大小, 默认为 40960 字节.</param>
        public CsvReadHelper( Stream stream, CsvFlag flag, bool firstRowIsHead = true, int readBufferLength = 40960 )
            : this( stream, Encoding.UTF8, flag, CancellationToken.None, firstRowIsHead, readBufferLength )
        {
        }

        /// <summary>
        /// 初始化读取文件
        /// </summary>
        /// <param name="csvFileName">要读取的文件路径及名称</param>
        /// <param name="dataEncoding">字符编码</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="cancelToken">取消用token</param>
        /// <param name="firstRowIsHead">是否将第一行数据做为标题行</param>
        /// <param name="readBufferLength">流读取缓冲大小, 默认为 40960 字节.</param>
        public CsvReadHelper( string csvFileName, Encoding dataEncoding, CsvFlag flag, CancellationToken cancelToken, bool firstRowIsHead = true, int readBufferLength = 40960 )
            : this( File.Open( csvFileName, FileMode.Open ), dataEncoding, flag, cancelToken, firstRowIsHead, readBufferLength )
        {
        }

        /// <summary>
        /// 使用 Encoding.UTF8 编码初始化读取文件
        /// </summary>
        /// <param name="csvFileName">要读取的文件路径及名称</param>
        /// <param name="flag">csv 字段分隔符与限定符</param>
        /// <param name="firstRowIsHead">是否将第一行数据做为标题行</param>
        /// <param name="readBufferLength">流读取缓冲大小, 默认为 40960 字节.</param>
        public CsvReadHelper( string csvFileName, CsvFlag flag, bool firstRowIsHead = true, int readBufferLength = 40960 )
            : this( csvFileName, Encoding.UTF8, flag, CancellationToken.None, firstRowIsHead, readBufferLength )
        {
        }


        /// <summary>
        /// 异步读取, 每读取 readProgressSize 条记录或到文件末尾触发通知事件. 此方法只能调用一次, 如果多次调用会产生异常.
        /// </summary>
        /// <typeparam name="T">数据行转换时对应的实体类型</typeparam>
        /// <param name="dataHandler">数据处理方法</param>
        /// <param name="convertRowData">数据行转换为 T 实例的方法</param>
        /// <param name="cancelToken">取消参数</param>
        /// <param name="readProgressSize">每读取多少行数据触发通知事件, 默认为 1000.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">dataHandler 为空时</exception>
        /// <exception cref="ArgumentNullException">convertRowData 为空时</exception>
        /// <exception cref="ArgumentException">readProgressSize 不大于0时</exception>
        /// <exception cref="OperationCanceledException">cancelToken.IsCancellationRequested 为 true 时</exception>
        public async Task ReadAsync<T>( Action<CsvReadProgressInfo<T>> dataHandler, Func<List<string>, T> convertRowData, int readProgressSize = 1000 ) where T : new()
        {
            #region Check params

            if ( CancelToken != null && CancelToken.IsCancellationRequested )
            {
                CancelToken.ThrowIfCancellationRequested();
            }

            if ( dataHandler == null )
            {
                throw new ArgumentNullException( nameof( dataHandler ) );
            }

            if ( convertRowData == null )
            {
                throw new ArgumentNullException( nameof( convertRowData ) );
            }

            if ( readProgressSize <= 0 )
            {
                throw new ArgumentException( $"The property {nameof( readProgressSize )} must be greater than 0" );
            }

            #endregion

            //读取数据缓冲
            CharBuffer buffer = new CharBuffer( readBufferLength );
            //标题行
            List<string> columnNames = new List<string>();
            //完整的行
            List<List<string>> rows = new List<List<string>>();
            //用于临时存放不足一行的数据
            List<char> subLine = new List<char>();

            //获得数据流总字节数
            long totalBytes = csvStream.BaseStream.Length;
            //当前读取字节数
            long currentBytes = 0;

            //开始循环读取数据
            while ( !csvStream.EndOfStream )
            {
                if ( CancelToken.IsCancellationRequested )
                {
                    CancelToken.ThrowIfCancellationRequested();
                }

                //读取一块数据
                int count = await csvStream.ReadAsync( buffer.Buffer, buffer.EndIndex, buffer.Buffer.Length - buffer.EndIndex );
                buffer.Length = buffer.Length + count;
                buffer.StartIndex = 0;
                currentBytes = csvStream.BaseStream.Position;
                GetRows( ref buffer, ref rows );
                Progress( currentBytes, totalBytes, dataHandler, convertRowData, readProgressSize, rows, ref columnNames );

                //如果到了文件流末尾且还有未处理的字符,添加末尾换行符处理余下字符.
                if ( csvStream.EndOfStream && buffer.EndIndex > 0 )
                {
                    //在末尾添加换行符
                    buffer.Buffer.SetValue( '\r', buffer.EndIndex );
                    buffer.Buffer.SetValue( '\n', buffer.EndIndex + 1 );
                    buffer.Length = buffer.Length + 2;
                    GetRows( ref buffer, ref rows );
                    Progress( currentBytes, totalBytes, dataHandler, convertRowData, readProgressSize, rows, ref columnNames );

                    //如果还有剩余字符,说明格式错误
                    if ( buffer.Length > 0 )
                    {
                        throw new Exception( "The csv file format error!" );
                    }

                    //为下一块数据使用准备
                    subLine.Clear();
                }
            }

            //资料有不完整的行，或者读取错位导致剩余。
            if ( buffer.Length > 0 )
            {
                throw new Exception( "The csv file format error!" );
            }
        }

        /// <summary>
        /// 关闭流
        /// </summary>
        public void Close()
        {
            try
            {
                csvStream?.Close();
            }
            finally
            {
                csvStream = null;
            }
        }


        /// <summary>
        /// 组织数据, 发送通知.
        /// </summary>
        /// <typeparam name="T">通知中数据行的实体类型</typeparam>
        /// <param name="currentBytes">当前字节数</param>
        /// <param name="totalBytes">流字节总数</param>
        /// <param name="dataHandler">通知方法</param>
        /// <param name="convertRowData">转换类型方法</param>
        /// <param name="readProgressSize">多少条数据触发通知</param>
        /// <param name="rows">原始的字符串数据集合</param>
        /// <param name="columnNames">标题行</param>
        /// <param name="rowsData">转换后的数据</param>
        private void Progress<T>( long currentBytes, long totalBytes
            , Action<CsvReadProgressInfo<T>> dataHandler, Func<List<string>, T> convertRowData, int readProgressSize
            , List<List<string>> rows, ref List<string> columnNames ) where T : new()
        {
            //设置标题行.
            if ( rows.Count > 0 && columnNames.Count == 0 )
            {
                List<string> firstRow = rows[0];

                //如果第一行做为标题
                if ( FirstRowIsHead )
                {
                    columnNames = firstRow;
                    //从数据中移除第一行
                    rows.Remove( firstRow );
                }
                else
                {
                    //否则用字段索引做标题行
                    for ( int i = 0; i < firstRow.Count; i++ )
                    {
                        columnNames.Add( i.ToString() );
                    }
                }
            }

            //生成通知数据
            if ( rows.Count >= readProgressSize || (rows.Count > 0 && csvStream.EndOfStream) )
            {
                //通知数据
                CsvReadProgressInfo<T> info = new CsvReadProgressInfo<T>();
                info.ColumnNames = columnNames;

                //加入数据行
                for ( int i = 0; i < rows.Count; )
                {
                    info.CurrentRowsData.Add( convertRowData.Invoke( rows[i] ) );
                    rows.Remove( rows[i] );

                    if ( info.CurrentRowsData.Count == readProgressSize || (csvStream.EndOfStream && rows.Count == 0) )
                    {
                        info.IsComplete = csvStream.EndOfStream;
                        info.ReadBytes = currentBytes;
                        info.TotalBytes = totalBytes;
                        dataHandler( info );
                        info = new CsvReadProgressInfo<T>();
                        info.ColumnNames = columnNames;

                        //剩余数据不足一次返回行数，跳出，下次再发送
                        if ( rows.Count > 0 && rows.Count < readProgressSize && !csvStream.EndOfStream )
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取完整行数据，返回不足一行的数据。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">字符集合</param>
        /// <param name="index">起始索引</param>
        /// <param name="length">有效数据长度</param>
        /// <param name="rows">返回的完整行数据</param>
        /// <param name="subLine">不足一行的数据</param>
        private void GetRows( ref CharBuffer input, ref List<List<string>> rows )
        {
            if ( input != null && input.Buffer != null )
            {
                //完整行数据结束索引
                int endIndex = 0;
                int qualifierQty = 0; //每行限定符数量
                List<char> field = new List<char>(); //字段内容
                List<string> line = new List<string>(); //每行内容
                Span<char> data = new Span<char>( input.Buffer, input.StartIndex, input.Length );
                //找出完整的行
                for ( int i = 0; i < data.Length; i++ )
                {
                    char c = data[i];

                    //找出完整字段
                    if ( c == Flag.FieldQualifier )//如果是限定符,统计数量
                    {
                        qualifierQty++;
                    }
                    else if ( c == Flag.FieldSeparator ) //如果是分隔符，判断字段是否完整
                    {
                        if ( qualifierQty % 2 == 0 )//限定符成对,说明字段完整,加入到行中.
                        {
                            line.Add( DeserializeField( new string( field.ToArray() ) ) );
                            field.Clear(); //重新收集字段
                            qualifierQty = 0;
                        }
                        else//限定符不成对,属于字段内的字符,加入字段中.
                        {
                            field.Add( c );
                        }
                    }
                    else if ( c == '\n' ) //如果是换行符，判断一行是否结束
                    {
                        if ( qualifierQty % 2 == 0 )//如果限定符成对,说明是字段后换行, 即一行结束.
                        {
                            //一行结束时, 如果前一个字符是\r, 移除掉.
                            if ( i >= 1 && data[i - 1] == '\r' && field.Count > 0 )
                            {
                                field.RemoveAt( field.Count - 1 );//移除之前加入的\r 
                                line.Add( DeserializeField( new string( field.ToArray() ) ) );
                                field.Clear(); //重新收集字段
                            }

                            if ( line.Count > 0 )//将完整的行数据加入待处理缓存中
                            {
                                rows.Add( line );
                                line = new List<string>();
                                endIndex = i + 1;
                            }

                            qualifierQty = 0;
                        }
                        else//如果限定符不成对,说明是字段中的换行符,加入字段中.
                        {
                            field.Add( c );
                        }
                    }
                    else //如果是其他字符，加入待处理数据
                    {
                        field.Add( c );
                    }
                }

                //将最后不足一行的数据传出去
                var sub = data.Slice( endIndex ).ToArray();
                Array.Copy( sub, 0, input.Buffer, 0, sub.Length );
                input.StartIndex = 0;
                input.Length = sub.Length;
            }
        }

        /// <summary>
        /// 还原 CSV 字段值,将两个相邻限定符替换为一个限定符,去掉两边的限定符.
        /// </summary>
        /// <param name="field">待还原的字段</param>
        /// <param name="trimField">是否去除无限定符包围字段两端的空格，默认保留.</param>
        /// <returns>还原转义符后的字段</returns>
        private string DeserializeField( string field, bool trimField = false )
        {
            string result = field;

            if ( trimField )
            {
                result = result.Trim();
            }

            //当字段包含限定符时
            if ( result.Contains( Flag.Qualifier ) )
            {
                result = result.Trim();//先去除左右空格

                //当字符数小于2个(限定符不成对)或第1个和最后1个字符不是限定符时,说明字段格式错误,引发异常.
                if ( result.Length < 2 || (result[0] != Flag.FieldQualifier || result[result.Length - 1] != Flag.FieldQualifier) )
                {
                    throw new Exception( "The input field '" + field + "' is invalid!" );
                }
                else
                {
                    result = result.Substring( 1, result.Length - 2 ).Replace( Flag.DoubleQualifier, Flag.Qualifier );
                }
            }

            return result;
        }
    }
}
