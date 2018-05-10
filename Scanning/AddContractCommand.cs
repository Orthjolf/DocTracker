using System;
using System.Linq;
using MongoDB.Bson;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Enum;
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
			UpdateBox(_boxId);
			ConsoleWriter.Write($"Договор с номером {contract["Number"]} добавлен");
			IsWorking = false;
		}

		private void UpdateBox(string boxId)
		{
			var contracts = Contract.Repository.GetByBoxId(boxId).ToList();
			var maxDate = contracts.Max(c => c.ContractDate);
			var minDate = contracts.Min(c => c.ContractDate);
			var count = contracts.Count;

			var box = Box.Repository.Get(boxId);

			var newBox = new Box
			{
				Id = box.Id,
				ContractsCount = count,
				Description = box.Description,
				MaxDate = maxDate,
				MinDate = minDate,
				Name = box.Name,
				StorageId = box.StorageId
			};

			Box.Repository.Update(newBox);
		}
	}
}