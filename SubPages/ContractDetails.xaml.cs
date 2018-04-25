using System.Globalization;
using System.Windows.Controls;
using WpfApp.Domain;

namespace WpfApp.SubPages
{
	public partial class ContractDetails : UserControl
	{
		public ContractDetails(Contract contract)
		{
			InitializeComponent();
			ContractNumber.Text = contract.Number;
			LoanId.Text = contract.LoanId;
//			BoxId.Text = contract.BoxId;
			ClientFirstName.Text = contract.ClientFirstName;
			ClientLastName.Text = contract.ClientLastName;
			ClientPatronymic.Text = contract.ClientPatronymic;
			PhoneNumber.Text = contract.PhoneNumber;
			ContractDate.Text = contract.ContractDate.ToString(CultureInfo.InvariantCulture);
		}
	}
}