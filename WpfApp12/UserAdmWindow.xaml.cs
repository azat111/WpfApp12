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
    /// Логика взаимодействия для UserAdmWindow.xaml
    /// </summary>
    public partial class UserAdmWindow : Window
    {
        public static TestDemoContext asd = new TestDemoContext();
        public UserAdmWindow(MainWindow mainWindow,User a)
        {
            InitializeComponent();
			foreach (var item in asd.Tovaris)
			{
				if (item.Photo==null)
				{
					item.Photo = "abc";
				}
			}
            ProdGrid.ItemsSource = asd.Tovaris.ToList();
		}

		private void DeleteBtn(object sender, RoutedEventArgs e)
		{
			Tovari a = ProdGrid.SelectedItem as Tovari;
			if (MessageBox.Show("Delete tovar?", "DELETING", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
			{	
				asd.Remove(a);
				asd.SaveChanges();
				ProdGrid.ItemsSource = asd.Tovaris.ToList();
			}

		}

		private void EditBtn(object sender, RoutedEventArgs e)
		{
			Tovari a = ProdGrid.SelectedItem as Tovari;
			EditTovari tovari = new EditTovari(this,a);
			tovari.Show();
		}

		public void UpdateGrid(TestDemoContext context)
		{
			ProdGrid.ItemsSource = context.Tovaris.ToList();
			asd = context;
		}
	}
}
