# ITnmg.CsvHelper
![图标](https://raw.githubusercontent.com/jgh004/ITnmg.CsvHelper/master/Docs/logo.png)

  A library of asynchronous read and write large csv file.  
  简单易用的 csv 异步读写类库, 可读写大型 csv 文件. 

# Test Form
![实现效果](https://raw.githubusercontent.com/jgh004/ITnmg.CsvHelper/master/docs/test.png?s=200)

# Install

Run the following command in the Package Manager Console.  
在 nuget 包管理器控制台输入以下命令

    PM> Install-Package ITnmg.CsvHelper

# Getting Started

### Csv File IETF Standard
[IETF RFC4180](https://tools.ietf.org/html/rfc4180)

### Reading csv
    public async Task ReadCsv(...)
    {
        var csvReader = new CsvReadHelper( fileName or stream, encoding, flag, firstRowIsHead, readStreamBufferLength );
        
        await csvReader.ReadAsync( p => 
	{
	    SetColumnHeads( e.ColumnNames );
            ShowData( e.CurrentRowsData );
            SetProgress( Convert.ToInt32( e.ProgressValue ) );
	}, f =>
	{
	    return ConvertCsvRowToCustomModel( f );
	}, cancelToken, 1000 );
        csvReader.Close();
    }
    
### Writing csv
    public async Task WriteCsv(...)
    {
        //using delegate to get rows data. 使用委托获取数据
        Progress<CsvWriteProgressInfo> progress = new Progress<CsvWriteProgressInfo>( e =>
        {
            SetProgress( Convert.ToInt32( e.WirteRowCount / totalRowCount * 100 ) );
        } );
        
        var csvWriter = new CsvWriteHelper( fileName or stream, encoding, flag, cancelToken, progress, 1000 );
        
        //prevent ui thread blocking. 防止 ui 线程阻塞
        await Task.Run( async () =>
        {
            await csvWriter.WriteLineAsync( columnNames );
            await csvWriter.WriteAsync( modelList, f =>
            {
                return ConvertCustomModelToRowData( f );
            } );
            ...
            ...
            ...
            await csvWriter.WriteAsync(...);

            await csvWriter.FlushAsync();
            csvWriter.Close();
        }, cancelToken );
    }
