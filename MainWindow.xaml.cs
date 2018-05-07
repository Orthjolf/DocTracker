using System;
using System.Linq;
using System.Windows.Controls;
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
//
//			for (var i = 0; i < 10; i++)
//			{
//				Contract.Repository.AddAndSave(new BsonDocument
//				{
//					{"Number", GetRandomString()},
//					{"BoxId", "5ae01249c019612a845a32ee"},
//					{"ClientFirstName", GetRandomString()},
//					{"ClientLastName", GetRandomString()},
//					{"ClientPatronymic", GetRandomString()},
//					{"PhoneNumber", GetRandomString()},
//					{"LoanId", GetRandomString()},
//					{"PrefixOfPlace", GetRandomString()},
//					{"ContractDate", DateTime.Now }
//				});
//			}

			showMsg();
		}

		private static Random r = new Random();

		static string GetRandomString()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, 14)
				.Select(s => s[r.Next(s.Length)]).ToArray());
		}

		public async void showMsg()
		{
//			var diag_dialog = new ModalTest();
//			diag_dialog.Close_Button.Click += Close_Dialog;

//			await this.ShowMetroDialogAsync(diag_dialog);
		}


		/// <summary>
		/// Устанавливает содержимое главного окна
		/// </summary>
		/// <param name="content">Содержимое, которе будет отображаться</param>
		public static void SetContent(UserControl content)
		{
			_instance.Content = content;
		}

		/// <summary>
		/// Устанавливает содержимое главного окна панелью с ранее выбранным хранилищем
		/// </summary>
		/// <param name="selectedStorageId">Id ранее выбранного хранилища</param>
		public static void SetContentAsStoragesPage(string selectedStorageId)
		{
			_instance.Content = _mainContent;
			_mainContent.SelectItem(selectedStorageId);
		}
	}
}