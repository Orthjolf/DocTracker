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

		public void DoWork(string barCode)
		{
			IsWorking = true;
			var decodedBarCode = BarCodeDecoder.Reconstitute(barCode);
			var id = decodedBarCode.Key;
			var contractNumber = decodedBarCode.Value;

			var contract = ContractFromDb.Get(id, contractNumber);
			contract["BoxId"] = _boxId;
			Contract.Repository.AddAndSave(contract);
			ConsoleWriter.Write($"Договор с номером {contract["Number"]} добавлен");
			IsWorking = false;
		}
	}
}