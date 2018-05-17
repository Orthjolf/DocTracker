using System;
using System.Linq;
using System.Windows.Controls;
using WpfApp.SubPages;
using WpfApp.Temp;

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
//			_mainContent = new MainContent();
//			RootContent.Content = _mainContent;
			RootContent.Content = new TestDb();

			showMsg();
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