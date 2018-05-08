namespace WpfApp.Scanning
{
	/// <summary>
	/// Команда, выполняемая при сканировании документа
	/// </summary>
	public interface IScanningCommand
	{
		bool IsWorking { get; }
		void DoWork(string barCode);
	}
}