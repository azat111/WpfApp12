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
    /// Логика взаимодействия для SelectQuant.xaml
    /// </summary>
    public partial class SelectQuant : Window
    {
        CorzinaTov corzinaTov;
        Tovari tovar;
		public SelectQuant(CorzinaTov corzinaTov, Tovari a)
        {
            InitializeComponent();
            this.corzinaTov = corzinaTov;
            this.tovar = a;
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            int a = Convert.ToInt32(QuTxt.Text);
            corzinaTov.SetQuant(a, tovar);
            corzinaTov.Show();
            this.Close();
		}
	}
}
