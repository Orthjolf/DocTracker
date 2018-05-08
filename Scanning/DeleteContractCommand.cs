using System;
using System.Linq;
using System.Threading;
using WpfApp.Domain;
using WpfApp.Service;

namespace WpfApp.Scanning
{
	/// <summary>
	/// Команда удаления документа при сканировании
	/// </summary>
	public class DeleteContractCommand : IScanningCommand
	{
		public bool IsWorking { get; private set; }

		public DeleteContractCommand()
		{
			IsWorking = false;
		}

		public void DoWork(string barCode)
		{
			IsWorking = true;
			var contracts = Contract.Repository.GetAll().ToList();

			if (contracts.Any())
			{
				var random = new Random();
				var randomContract = contracts[random.Next(0, contracts.Count - 1)];
				ConsoleWriter.Write($"Договор с номером {randomContract.Number} удален");
				Contract.Repository.DeleteById(randomContract.Id);
			}

			IsWorking = false;
		}
	}
}