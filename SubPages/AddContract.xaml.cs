using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.SubPages
{
	public partial class AddContract
	{
		private readonly string _boxId;

		private bool _working = false;

		private readonly ManualResetEvent _resetEvent = new ManualResetEvent(false);

		public AddContract(string boxId)
		{
			InitializeComponent();
			_boxId = boxId;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var window = Window.GetWindow(this);
			if (window == null) return;
			window.KeyDown += HandleKeyPress;
		}

		private void HandleKeyPress(object sender, KeyEventArgs e)
		{
			if (_working)
			{
				ConsoleWriter.Write("Уже работает");
				return;
			}

			Task.Factory.StartNew(SaveNewContract)
				.ContinueWith(result => UpdateUi());
		}

		private void SaveNewContract()
		{
			ConsoleWriter.Write("Начал работу");

			_working = true;
			Dispatcher.Invoke(() =>
			{
				Success.Visibility = Visibility.Hidden;
				Processing.Visibility = Visibility.Visible;
				LoadingIndicator.Visibility = Visibility.Visible;
			});

			var barCode = RandomBarCodeGenerator.GetRandomBarCode();
			var decoded = BarCodeDecoder.Reconstitute(barCode);
			var id = decoded.Key;
			var contractNumber = decoded.Value;

			var contract = ContractFromDb.Get(id, contractNumber);
			contract["BoxId"] = _boxId;
//			Contract.Repository.AddAndSave(contract);
			Thread.Sleep(2000);
		}

		private void UpdateUi()
		{
			Dispatcher.Invoke(() =>
			{
				Success.Visibility = Visibility.Visible;
				Processing.Visibility = Visibility.Hidden;
				LoadingIndicator.Visibility = Visibility.Hidden;
			});
			_working = false;

			ConsoleWriter.Write("Закончил работу");
		}
	}
}