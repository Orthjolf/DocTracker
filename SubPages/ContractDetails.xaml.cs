using System.Globalization;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class ContractDetails
	{
		public ContractDetails(Contract contract)
		{
			InitializeComponent();
			ContractNumber.Text = contract.Number;
			LoanId.Text = contract.LoanId;
			ClientFirstName.Text = contract.ClientFirstName;
			ClientLastName.Text = contract.ClientLastName;
			ClientPatronymic.Text = contract.ClientPatronymic;
			PhoneNumber.Text = contract.PhoneNumber;
			ContractDate.Text = contract.ContractDate.ToString(CultureInfo.InvariantCulture);
		}
	}
}