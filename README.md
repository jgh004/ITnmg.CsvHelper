# ITnmg.CsvHelper
![图标](https://raw.githubusercontent.com/jgh004/ITnmg.CsvHelper/master/Docs/logo.png)

  A library of asynchronous read and write large csv file.  
  简单易用的 csv 异步读写类库, 可读写大型 csv 文件. 

# Test Form

  写入10万条记录（约127MB）
![写入10万条记录（约127MB）](https://raw.githubusercontent.com/jgh004/ITnmg.CsvHelper/master/Docs/write.png?s=200)

  读取10万条记录（约127MB）
![读取10万条记录（约127MB）](https://raw.githubusercontent.com/jgh004/ITnmg.CsvHelper/master/Docs/read.png?s=200)

# Install

Run the following command in the Package Manager Console.  
在 nuget 包管理器控制台输入以下命令

    PM> Install-Package ITnmg.CsvHelper

# Getting Started

### Csv File IETF Standard
[IETF RFC4180](https://tools.ietf.org/html/rfc4180)

### Read csv
    public async Task ReadCsvAsync( ... )
    {
        var csvReader = new CsvReadHelper( ... );
        
        await csvReader.ReadAsync( p => 
        {
            SetColumnHeads( e.ColumnNames );
            ShowData( e.CurrentRowsData );
            SetProgress( Convert.ToInt32( e.ProgressValue ) );
        }, f =>
        {
            return ConvertCsvRowToCustomModel( f );
        }, 1000 );
		
        csvReader.Close();
    }
    
### Write csv
    public async Task WriteCsvAsync( ... )
    {
        var csvWriter = new CsvWriteHelper( ..., f =>
        {
            SetProgressVal( f.WirteRowCount / TotalModelCount );
        }, ... );

        await csvWriter.WriteLineAsync( columnNames );
        await csvWriter.WriteAsync( modelList1, f =>
        {
            return ConvertModelToRowData( f );
        } );
        await csvWriter.WriteAsync( modelList2, f =>
        {
            return ConvertModelToRowData2( f );
        } );

        await csvWriter.FlushAsync();
        csvWriter.Close();
    }
