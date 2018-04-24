using System;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.SubPages;

namespace WpfApp
{
	public partial class MainWindow
	{
		private static MainWindow _instance;

		private static MainContent _mainContent;

		public MainWindow()
		{
			InitializeComponent();
			_instance = this;
			_mainContent = new MainContent();
			RootContent.Content = _mainContent;

//			for (var i = 0; i < 10; i++)
//			{
//				Contract.Repository.AddAndSave(new BsonDocument
//				{
//					{"Number", GetRandomString()},
//					{"BoxId", "5adf29f3c0196120580ebe5d"},
//					{"ClientFirstName", GetRandomString()},
//					{"ClientLastName", GetRandomString()},
//					{"ClientPatronymic", GetRandomString()},
//					{"PhoneNumber", GetRandomString()},
//					{"LoanId", GetRandomString()},
//					{"PrefixOfPlace", GetRandomString()}
//				});
//			}

//			showMsg();
		}

		static string GetRandomString()
		{
			var random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, 10)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

//		public async void showMsg()
//		{
//			await this.ShowMetroDialogAsync(MetroDialogSettings
//			{
//				
//			}); // MessageAsync("This is the title", "Some message");
//		}

		public static void SetContent(UserControl content)
		{
			_instance.Content = content;
		}

		public static void SetDefault(string selectedStorageId)
		{
			_instance.Content = _mainContent;
			_mainContent.SelectItem(selectedStorageId);
		}
	}
}