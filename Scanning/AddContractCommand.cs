using System.Threading;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.Scanning
{
	/// <summary>
	/// Команда добавления документа в коробку при сканировании
	/// </summary>
	public class AddContractCommand : IScanningCommand
	{
		public bool IsWorking { get; private set; }

		private readonly string _boxId;

		public AddContractCommand(string boxId)
		{
			IsWorking = false;
			_boxId = boxId;
		}

		public void DoWork()
		{
			IsWorking = true;

			var barCode = RandomBarCodeGenerator.GetRandomBarCode();
			var decoded = BarCodeDecoder.Reconstitute(barCode);
			var id = decoded.Key;
			var contractNumber = decoded.Value;

			var contract = ContractFromDb.Get(id, contractNumber);
			contract["BoxId"] = _boxId;
			Contract.Repository.AddAndSave(contract);
			Thread.Sleep(2000);
			IsWorking = false;
		}
	}
}