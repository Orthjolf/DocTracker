using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class ContractDetails : UserControl
	{
		public string Number { get; set; }
		public string BoxId { get; set; }
		public string ClientFirstName { get; set; }
		public string ClientLastName { get; set; }
		public string ClientPatronymic { get; set; }
		public string PhoneNumber { get; set; }
		public string LoanId { get; set; }
		public string PrefixOfPlace { get; set; }

		public ContractDetails(Contract contract)
		{
			InitializeComponent();
			Number = contract.Number;
			BoxId = contract.BoxId;
			ClientFirstName = contract.ClientFirstName;
			ClientLastName = contract.ClientLastName;
			ClientPatronymic = contract.ClientPatronymic;
			PhoneNumber = contract.PhoneNumber;
			LoanId = contract.LoanId;
			PrefixOfPlace = contract.PrefixOfPlace;
		}
	}
}