using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ITnmg.CsvHelper;

namespace Example
{
    public partial class Main : Form
    {
        SynchronizationContext Sync = null;
        CancellationTokenSource CancelSource = new CancellationTokenSource();
        //保存csv数据
        List<string> ColumnsName = null;
        List<TestProductModel> ModelCsvData = null;

        public Main()
        {
            InitializeComponent();


            var properInfo = dgv_Data.GetType().GetProperty( "DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic );
            properInfo.SetValue( dgv_Data, true, null );

            //dgv_Data.RowPostPaint += ExtLibrary_DataGridView_RowPostPaint;

            dgv_Data.VirtualMode = true;
            dgv_Data.AutoGenerateColumns = false;
            dgv_Data.CellValueNeeded += dgv_Data_CellValueNeeded;
        }


        private void Main_Load( object sender, EventArgs e )
        {
            cob_separator.SelectedIndex = 0;
            cob_FieldEnclosed.SelectedIndex = 0;
            cob_FirstIsHead.SelectedIndex = 0;
            Sync = SynchronizationContext.Current;
            CancelSource = new CancellationTokenSource();

            Init();
        }

        private void bt_Open_Click( object sender, EventArgs e )
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                bt_Open.Enabled = false;
                bt_Save.Enabled = false;
                pb_Progress.Value = 0;
                RefreshDataGridView( 0 );

                OpenFileDialog f = new OpenFileDialog();
                f.Filter = "CSV Files|*.csv|TxtFile|*.txt";
                f.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory );

                if ( f.ShowDialog() == DialogResult.OK )
                {
                    tb_FileName.Text = f.FileName;

                    ReadData().ContinueWith( k =>
                    {
                        Cursor = Cursors.Default;
                        bt_Open.Enabled = true;
                        bt_Save.Enabled = true;
                        RefreshDataGridView( ModelCsvData.Count );

                        if ( k.IsCanceled )
                        {
                            MessageBox.Show( "Read operation has been canceled." );
                        }

                        if ( k.Exception != null )
                        {
                            MessageBox.Show( k.Exception.GetBaseException().Message );
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext() );
                }
                else
                {
                    Cursor = Cursors.Default;
                    bt_Open.Enabled = true;
                    bt_Save.Enabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void bt_Save_Click( object sender, EventArgs e )
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                bt_Open.Enabled = false;
                bt_Save.Enabled = false;
                pb_Progress.Value = 0;
                SaveFileDialog f = new SaveFileDialog();
                f.Filter = "CSV File|*.csv";
                f.FileName = string.IsNullOrWhiteSpace( tb_FileName.Text ) ? "test" : Path.GetFileNameWithoutExtension( tb_FileName.Text.Trim() ) + "-after.csv";
                f.InitialDirectory = Environment.GetFolderPath( Environment.SpecialFolder.Desktop );

                if ( f.ShowDialog( this ) == DialogResult.OK )
                {
                    WriteData( f.FileName ).ContinueWith( k =>
                    {
                        Cursor = Cursors.Default;
                        bt_Open.Enabled = true;
                        bt_Save.Enabled = true;
                        RefreshDataGridView( ModelCsvData.Count );

                        if ( k.IsCanceled )
                        {
                            MessageBox.Show( "Write operation has been canceled." );
                        }
                        if ( k.Exception != null )
                        {
                            MessageBox.Show( k.Exception.GetBaseException().Message );
                        }
                    }, TaskScheduler.FromCurrentSynchronizationContext() );
                }
                else
                {
                    Cursor = Cursors.Default;
                    bt_Open.Enabled = true;
                    bt_Save.Enabled = true;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        }

        private void bt_Cancel_Click( object sender, EventArgs e )
        {
            if ( CancelSource != null && !CancelSource.IsCancellationRequested )
            {
                CancelSource.Cancel();
                CancelSource = new CancellationTokenSource();
                RefreshDataGridView( ModelCsvData == null ? 0 : ModelCsvData.Count );
            }
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExtLibrary_DataGridView_RowPostPaint( object sender, DataGridViewRowPostPaintEventArgs e )
        {
            DataGridView dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle( e.RowBounds.Location.X
                , e.RowBounds.Location.Y
                , dgv.RowHeadersWidth - 4
                , e.RowBounds.Height );

            TextRenderer.DrawText( e.Graphics, (e.RowIndex + 1).ToString(),
                dgv.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right );
        }

        /// <summary>
        /// 虚拟模式绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_Data_CellValueNeeded( object sender, DataGridViewCellValueEventArgs e )
        {
            if ( ModelCsvData != null && e.RowIndex < ModelCsvData.Count && e.ColumnIndex > -1 && e.RowIndex > -1 )
            {
                List<string> row = ConvertModelToRowData( ModelCsvData[e.RowIndex] );
                e.Value = row[e.ColumnIndex];
            }
        }


        private void Init()
        {
            GenerateTestData();
            InitDataGridView( ColumnsName );
            RefreshDataGridView( ModelCsvData.Count );
        }

        /// <summary>
        /// 异步读取csv
        /// </summary>
        /// <returns></returns>
        private async Task ReadData()
        {
            if ( !string.IsNullOrWhiteSpace( tb_FileName.Text ) )
            {
                ModelCsvData = null;
                Stopwatch sc = null;
                CsvReadHelper csv = null;

                try
                {
                    sc = Stopwatch.StartNew();
                    CsvFlag flag = new CsvFlag( Convert.ToChar( cob_separator.Text ), Convert.ToChar( cob_FieldEnclosed.Text ) );
                    csv = new CsvReadHelper( tb_FileName.Text, Encoding.UTF8, flag, CancelSource.Token, !Convert.ToBoolean( cob_FirstIsHead.SelectedIndex ), 40960 );

                    await csv.ReadAsync( k =>
                    {
                        Sync.Post( f =>
                        {
                            var eve = f as CsvReadProgressInfo<TestProductModel>;

                            if ( eve.CurrentRowsData != null )
                            {
                                if ( ModelCsvData == null )
                                {
                                    InitDataGridView( eve.ColumnNames );
                                    ModelCsvData = eve.CurrentRowsData;
                                }
                                else
                                {
                                    ModelCsvData.AddRange( eve.CurrentRowsData );
                                }

                                if ( eve.ProgressValue - pb_Progress.Value >= 10 || eve.ProgressValue == 100 )
                                {
                                    RefreshDataGridView( ModelCsvData.Count );
                                }
                            }

                            pb_Progress.Value = Convert.ToInt32( eve.ProgressValue );
                        }, k );
                    }, f => ConvertCsvRowToTestProductData( f ), 1000 );
                }
                finally
                {
                    csv?.Close();
                    sc?.Stop();
                    Sync.Post( k =>
                    {
                        tb_Times.Text = k.ToString();
                    }, sc.Elapsed.TotalSeconds.ToString() );
                }
            }
        }

        /// <summary>
        /// 异步写入csv
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private async Task WriteData( string fileName )
        {
            if ( ModelCsvData != null )
            {
                Stopwatch sc = null;
                CsvWriteHelper csv = null;

                try
                {
                    sc = Stopwatch.StartNew();

                    CsvFlag flag = new CsvFlag( Convert.ToChar( cob_separator.Text ), Convert.ToChar( cob_FieldEnclosed.Text ) );
                    csv = new CsvWriteHelper( fileName, Encoding.UTF8, flag, f =>
                    {
                        Sync.Post( t =>
                        {
                            double val = (t as CsvWriteProgressInfo).WirteRowCount / (double)(ModelCsvData.Count + (cob_FirstIsHead.SelectedIndex == 0 ? 1 : 0));
                            pb_Progress.Value = Convert.ToInt32( val * 100 );
                        }, f );
                    }, CancelSource.Token, 1000 );

                    if ( ColumnsName != null )
                    {
                        await csv.WriteLineAsync( ColumnsName );
                    }

                    await csv.WriteAsync( ModelCsvData, f => ConvertModelToRowData( f ) );
                    await csv.FlushAsync();
                }
                finally
                {
                    csv?.Close();
                    sc?.Stop();
                    Sync.Post( k =>
                    {
                        tb_Times.Text = k.ToString();
                    }, sc.Elapsed.TotalSeconds.ToString() );
                }
            }
        }

        /// <summary>
        /// 生成测试csv数据
        /// </summary>
        private void GenerateTestData()
        {
            ColumnsName = new List<string>();
            ColumnsName.Add( "ID" );
            ColumnsName.Add( "Name" );
            ColumnsName.Add( "Description" );
            ColumnsName.Add( "Size" );
            ColumnsName.Add( "Price" );
            ColumnsName.Add( "Html" );
            ColumnsName.Add( "Url" );
            ColumnsName.Add( "CreateDate" );

            ModelCsvData = new List<TestProductModel>();

            for ( int i = 0; i < 100000; i++ )
            {
                TestProductModel model = new TestProductModel();
                model.ID = i + 1;
                model.Name = "name " + (i + 1).ToString();
                model.Price = i / 3m;
                model.Url = "http://www.test.com/product/" + i.ToString();
                model.Html = @"<div class=""description-div""><table class=""description-table""><tr>
            <td colspan=""2"" class=""description-td-title"">Details:</td>
        </tr>
        <tr>
            <td  style=""color:#ffff"">Color Type:</td>
            <td  class=""description-right-td"">Black/Brown/Red</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">MATERIAL:</td>
            <td  class=""description-right-td"">Vinyl</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">Fabric:</td>
            <td  class=""description-right-td"">Vinyl</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">Height:</td>
            <td  class=""description-right-td"">-</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">Length:</td>
            <td  class=""description-right-td"">5</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">Width:</td>
            <td  class=""description-right-td"">3</td>
        </tr>
        <tr>
            <td  class=""description-left-td"">Weight:</td>
            <td  class=""description-right-td"">3.40g</td>
        </tr>
    
</table></div>";
                model.CreateDate = DateTime.Now;

                ModelCsvData.Add( model );
            }
        }

        /// <summary>
        /// 将模型转为一行csv数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private List<string> ConvertModelToRowData( TestProductModel model )
        {
            List<string> result = new List<string>();

            if ( model != null )
            {
                result.Add( model.ID.ToString() );
                result.Add( model.Name );
                result.Add( model.Description );
                result.Add( model.Size );
                result.Add( model.Price.ToString() );
                result.Add( model.Html );
                result.Add( model.Url );
                result.Add( model.CreateDate.GetDateTimeFormats( 'r' )[0].ToString() );
            }

            return result;
        }

        /// <summary>
        /// 将一行csv数据转为模型实例
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TestProductModel ConvertCsvRowToTestProductData( List<string> data )
        {
            TestProductModel result = new TestProductModel();

            if ( data != null )
            {
                for ( int i = 0; i < data.Count; i++ )
                {
                    switch ( i )
                    {
                        case 0:
                            result.ID = Convert.ToInt32( data[i] );
                            break;
                        case 1:
                            result.Name = data[i];
                            break;
                        case 2:
                            result.Description = data[i];
                            break;
                        case 3:
                            result.Size = data[i];
                            break;
                        case 4:
                            result.Price = Convert.ToDecimal( data[i] );
                            break;
                        case 5:
                            result.Html = data[i];
                            break;
                        case 6:
                            result.Url = data[i];
                            break;
                        case 7:
                            result.CreateDate = DateTime.Parse( data[i] );
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="columns"></param>
        private void InitDataGridView( List<string> columns )
        {
            dgv_Data.Rows.Clear();
            dgv_Data.Columns.Clear();

            if ( columns != null )
            {
                foreach ( var c in columns )
                {
                    var column = new DataGridViewTextBoxColumn()
                    {
                        Name = c,
                        HeaderText = c,
                        DataPropertyName = c
                    };

                    dgv_Data.Columns.Add( column );
                }
            }
        }

        /// <summary>
        /// 刷新表格显示
        /// </summary>
        /// <param name="rowCount"></param>
        private void RefreshDataGridView( int rowCount )
        {
            dgv_Data.RowCount = rowCount;

            if ( dgv_Data.RowCount > 0 )
            {
                dgv_Data.FirstDisplayedScrollingRowIndex = dgv_Data.RowCount - 1;
            }
        }
    }
}
