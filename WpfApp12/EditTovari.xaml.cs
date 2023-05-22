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
    /// Логика взаимодействия для EditTovari.xaml
    /// </summary>
    public partial class EditTovari : Window
    {
        Tovari tovari1;
        UserAdmWindow admWindow;
		public static TestDemoContext context = new TestDemoContext();
		public EditTovari(UserAdmWindow userAdmWindow, Tovari tovari)
        {
            InitializeComponent();
            this.admWindow = userAdmWindow;
            tovari1 = context.Tovaris.FirstOrDefault(x => x.Id == tovari.Id);
            TovName.Text = tovari1.Name;
            TovPrice.Text= tovari1.Price.ToString();
            TovQuan.Text = tovari1.Quantity.ToString();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            tovari1.Name = TovName.Text;

            tovari1.Price = Convert.ToInt32(TovPrice.Text);

            tovari1.Quantity = Convert.ToInt32(TovQuan.Text);

            context.Update(tovari1);
            context.SaveChanges();
            admWindow.UpdateGrid(context);
		}
	}
}
