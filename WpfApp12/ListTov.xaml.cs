using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp12.Model;
using System.Windows.Media;
using IronBarCode;
using System.IO;
using System.Drawing;
using System.Windows.Media.Imaging;
using IronSoftware.Drawing;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для ListTov.xaml
    /// </summary>
    public partial class ListTov : Window
    {
        public static TestDemoContext context=new TestDemoContext();
        public ListTov()
        {
			InitializeComponent();
			JopaEja.ItemsSource = context.Tovaris.ToList();

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{


			//PrintOrderCard();
		}

		private async Task PrintOrderCard()
		{
			List<Tovari> TovarPichat = context.Tovaris.ToList();
			var app = new Microsoft.Office.Interop.Excel.Application
			{
				SheetsInNewWorkbook = 1
			};

			var workbook = app.Workbooks.Add(Type.Missing);

			Microsoft.Office.Interop.Excel.Worksheet worksheet = app.Worksheets.Item[1];
			worksheet.Name = "Tovari";

			int startrowIndex = 1;
			worksheet.Cells[1][startrowIndex] = "Name";
			worksheet.Cells[2][startrowIndex] = "Price";
			worksheet.Cells[3][startrowIndex] = "Quantity";
			worksheet.Cells[4][startrowIndex] = "TotalSum";

			foreach (var item in TovarPichat)
			{
				startrowIndex++;
				worksheet.Cells[1][startrowIndex] = item.Name;
				worksheet.Cells[2][startrowIndex] = item.Price;
				worksheet.Cells[3][startrowIndex] = item.Quantity;
				worksheet.Cells[4][startrowIndex] = item.Quantity * item.Price;
			}

			worksheet.Columns.AutoFit();

			app.Visible = true;

			app.Application.ActiveWorkbook.SaveAs(@"C:\Users\azat\Desktop\test.xlsx");

			var excelDocument = app.Workbooks.Open(@"C:\Users\azat\Desktop\test.xlsx");

			excelDocument.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, @"C:\Users\azat\Desktop\test.pdf");
			excelDocument.Close(false, "", false);
			app.Quit();
		}
	}
}
