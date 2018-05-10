using System;
using System.Linq;
using WpfApp.Debug;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.Scanning
{
	public static class ScanningCommand
	{
		public static bool IsWorking { get; private set; }

		public static void DoWork(string boxId, string barCode, ActionPerformed action)
		{
			IsWorking = true;
			var decodedBarCode = BarCodeDecoder.Reconstitute(barCode);
			var id = decodedBarCode.Key;
			var contractNumber = decodedBarCode.Value;
			if (action == ActionPerformed.PutInBox)
			{
				AddContract(boxId, id, contractNumber);
			}
			else
			{
				DeleteContract(boxId, id);
			}

			UpdateBox(boxId);
			IsWorking = false;
		}

		private static void AddContract(string boxId, string id, string contractNumber)
		{
			var contract = ContractFromDb.Get(id, contractNumber);
			contract["BoxId"] = boxId;
			Contract.Repository.AddAndSave(contract);
			ConsoleWriter.Write($"Договор с номером {contract["Number"]} добавлен");
		}

		private static void DeleteContract(string boxId, string id)
		{
			var contracts = Contract.Repository.GetByBoxId(boxId).ToList();
			if (!contracts.Any()) return;
			var random = new Random();
			var randomContract = contracts[random.Next(0, contracts.Count - 1)];
			Contract.Repository.DeleteById(randomContract.Id);
			ConsoleWriter.Write($"Договор с номером {randomContract.Number} удален");
		}

		private static void UpdateBox(string boxId)
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