using System;
using System.Linq;
using System.Threading;
using WpfApp.DataProvider.Repository;
using WpfApp.Domain;
using WpfApp.Enum;
using WpfApp.Extensions;
using WpfApp.Service;
using WpfApp.Temp;
using Console = WpfApp.Service.Console;

namespace WpfApp.Scanning
{
	public static class ScanningCommand
	{
		public static bool IsWorking { get; private set; }

		/// <summary>
		/// Выполнение действия при сканировании. Добавление/удаление письма из коробки
		/// </summary>
		/// <param name="boxId">Идентификатор коробки</param>
		/// <param name="barCode">Просканированный код</param>
		/// <param name="action">Действие</param>
		public static void DoWork(string boxId, string barCode, ActionPerformed action)
		{
			IsWorking = true;
			var decodedBarCode = BarCodeDecoder.Reconstitute(barCode);
			var contractId = decodedBarCode.Key;
			var contractNumber = decodedBarCode.Value;

			if (action == ActionPerformed.PutInBox)
				AddContract(boxId, contractId, contractNumber);
			else
				DeleteContract(boxId, contractId);
			Thread.Sleep(50);
			UpdateBox(boxId);
			IsWorking = false;
		}

		/// <summary>
		/// Добавление договора в коробку
		/// </summary>
		/// <param name="boxId">Идентификатор коробки</param>
		/// <param name="contractId">Идентификатор коробки</param>
		/// <param name="contractNumber">Номер договора</param>
		private static void AddContract(string boxId, string contractId, string contractNumber)
		{
			var contract = ContractFromDb.Get(contractId, contractNumber);
			contract.BoxId = boxId;
			Contract.Repository.Add(contract);
			Console.Write($"Договор с номером {contract.Number} добавлен");
		}

		/// <summary>
		/// Удаление контракта из коробки
		/// </summary>
		/// <param name="boxId">Идентификатор коробки</param>
		/// <param name="id">Идентификатор коробки</param>
		private static void DeleteContract(string boxId, string id)
		{
			var contracts = Contract.Repository.GetByBoxId(boxId);
			if (!contracts.Any()) return;

			var lastContract = contracts.Last();
			Contract.Repository.DeleteById(lastContract.Id);
			Console.Write($"Договор с номером {lastContract.Number} удален");
		}

		/// <summary>
		/// Обновление информации о коробке
		/// </summary>
		/// <param name="boxId">Идентификатор коробки</param>
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