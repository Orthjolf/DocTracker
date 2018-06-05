using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class Administraion : UserControl
	{
		public List<User> Users;

		public Administraion()
		{
			InitializeComponent();
			Users = User.Repository.GetAll().ToList();

			var marginTop = 0;
			Users.ForEach(user =>
			{
				UsersTable.Children.Add(new Label
				{
					Name = user.Name,
					Content = user.Name,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness
					{
						Top = marginTop
					}
				});
				UsersTable.Children.Add(new Label
				{
					Name = user.Name,
					Content = user.Login,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness
					{
						Top = marginTop,
						Left = 100
					}
				});
				UsersTable.Children.Add(new Label
				{
					Name = user.Name,
					Content = user.Role,
					VerticalAlignment = VerticalAlignment.Top,
					Margin = new Thickness
					{
						Top = marginTop,
						Left = 200
					}
				});
				marginTop += 30;
			});
		}
	}
}