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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp12.Model;
using EasyCaptcha;
using EasyCaptcha.Wpf;

namespace WpfApp12
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public static TestDemoContext context=new TestDemoContext();
		public MainWindow()
		{
			InitializeComponent();
			MyCaptcha.Visibility = Visibility.Hidden;
			cap1.Visibility = Visibility.Hidden;
			cap2.Visibility = Visibility.Hidden;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
				User user=context.Users.FirstOrDefault(a=>a.Login==Ulogin.Text && a.Password==UPas.Text);
				if (user!= null && user.RoleId==1)
				{
					UserAdmWindow userAdmWindow = new UserAdmWindow(this, user);
					userAdmWindow.Show();
					this.Close();
				}
			else if (user != null && user.RoleId == 2)
			{
				CorzinaTov userAdmWindow = new CorzinaTov(this, user);
				userAdmWindow.Show();
				this.Close();
			}
			else
			{
				Ulogin.Visibility = Visibility.Hidden;
				UPas.Visibility = Visibility.Hidden;
				MyCaptcha.Visibility = Visibility.Visible;
				cap1.Visibility = Visibility.Visible;
				cap2.Visibility = Visibility.Visible;
				MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
			}
				
		}
		
		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Corzina car = new Corzina() {};
			context.Corzinas.Add(car);
			context.SaveChanges();
			context.Users.Add(new User() { Login = Ulogin.Text, Password = UPas.Text, RoleId = 1, CorzinaId=car.Id});
			context.SaveChanges();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			if (cap2.Text == MyCaptcha.CaptchaText)
			{
				Ulogin.Visibility = Visibility.Visible;
				UPas.Visibility = Visibility.Visible;
				MyCaptcha.Visibility = Visibility.Hidden;
				cap1.Visibility = Visibility.Hidden;
				cap2.Visibility = Visibility.Hidden;
			}
			else
			{
				MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
			}
		}
	}
}
