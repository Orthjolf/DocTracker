using System;
using System.Linq;
using System.Threading;
using MongoDB.Bson;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Service;

namespace WpfApp.Scanning
{
	/// <summary>
	/// Команда удаления документа при сканировании
	/// </summary>
	public class DeleteContractCommand : IScanningCommand
	{
		public bool IsWorking { get; private set; }

		private string _boxId;

		public DeleteContractCommand(string boxId)
		{
			_boxId = boxId;
			IsWorking = false;
		}

		public void DoWork(string barCode)
		{
			IsWorking = true;
			var contracts = Contract.Repository.GetByBoxId(_boxId).ToList();

			if (contracts.Any())
			{
				var random = new Random();
				var randomContract = contracts[random.Next(0, contracts.Count - 1)];
				ConsoleWriter.Write($"Договор с номером {randomContract.Number} удален");
				Contract.Repository.DeleteById(randomContract.Id);
				UpdateBox(_boxId);
			}

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