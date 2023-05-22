using Microsoft.EntityFrameworkCore;
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

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для CorzinaTov.xaml
    /// </summary>
    public partial class CorzinaTov : Window
    {
		public static TestDemoContext asd = new TestDemoContext();
        MainWindow mainWindow;
        User user;
		int setquant;
		public CorzinaTov(MainWindow mainWindow, User a)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.user = a;
			foreach (var item in asd.Tovaris)
			{
				if (item.Photo == null)
				{
					item.Photo = "abc";
				}
			}
			ProdGrid.ItemsSource = asd.Tovaris.ToList();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Tovari a = ProdGrid.SelectedItem as Tovari;
			SelectQuant selectQuant = new SelectQuant(this,a);
			selectQuant.Show();


		}

		public void SetQuant(int a, Tovari aaa)
		{
			setquant = a;
		    aaa = ProdGrid.SelectedItem as Tovari;
			CorzinaOfTovari corzinaOfTovari = new CorzinaOfTovari();
			corzinaOfTovari.IdCorzina = user.CorzinaId;
			corzinaOfTovari.IdTovari = aaa.Id;
			corzinaOfTovari.Quntity = setquant;
			corzinaOfTovari.Price = aaa.Price * setquant;
			asd.Add(corzinaOfTovari);
			asd.SaveChanges();
			MessageBox.Show("Товар added!!!");
		}

		private async Task PrintOrderCard()
		{
			List<CorzinaOfTovari> corzinaOfTovari=asd.CorzinaOfTovaris.Where(x=>x.IdCorzina==user.CorzinaId).ToList();
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

			foreach (var item in corzinaOfTovari)
			{
				startrowIndex++;
				worksheet.Cells[1][startrowIndex] = item.IdTovariNavigation.Name;
				worksheet.Cells[2][startrowIndex] = item.IdTovariNavigation.Price;
				worksheet.Cells[3][startrowIndex] = item.Quntity;
				worksheet.Cells[4][startrowIndex] = item.Price;
			}

			worksheet.Columns.AutoFit();

			app.Visible = true;

			app.Application.ActiveWorkbook.SaveAs(@"C:\Users\azat\Desktop\test.xlsx");

			var excelDocument = app.Workbooks.Open(@"C:\Users\azat\Desktop\test.xlsx");

			excelDocument.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF, @"C:\Users\azat\Desktop\test.pdf");
			excelDocument.Close(false, "", false);
			app.Quit();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			PrintOrderCard();
		}
	}
}
