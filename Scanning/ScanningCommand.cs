using System;
using System.Linq;
using System.Threading;
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
				AddContract(boxId, id, contractNumber);
			else
				DeleteContract(boxId, id);
			Thread.Sleep(50);
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
			var randomContract = contracts.Last();
			Contract.Repository.DeleteById(randomContract.Id);
			ConsoleWriter.Write($"Договор с номером {randomContract.Number} удален");
		}

		private static void UpdateBox(string boxId)
		{
			var contracts = Contract.Repository.GetByBoxId(boxId).ToList();

			var box = Box.Repository.Get(boxId);
			if (contracts.Any())
			{
				box.MaxDate = contracts.Max(c => c.ContractDate);
				box.MinDate = contracts.Min(c => c.ContractDate);
				box.ContractsCount = contracts.Count;
			}
			else
			{
				box.MaxDate = DateTime.MinValue;
				box.MinDate = DateTime.MinValue;
				box.ContractsCount = 0;
			}

			Box.Repository.Update(box);
		}
	}
}