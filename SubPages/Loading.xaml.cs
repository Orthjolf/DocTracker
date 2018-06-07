using System.Windows.Controls;

namespace WpfApp.SubPages
{
	public partial class Loading : UserControl
	{
		public Loading()
		{
			InitializeComponent();
		}

		public Loading(string text)
		{
			InitializeComponent();
			LoadingText.Content = text;
		}
	}
}