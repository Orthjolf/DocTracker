using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using WpfApp.Domain;

namespace WpfApp.SubPages.Modals
{
	public partial class BoxPrintForm
	{
		public BoxPrintForm(Box box)
		{
			InitializeComponent();
			var info = GetBoxInfo(box);
			var document = BuildDocument(info);
			DocumentViewer.Document = document;
		}

		private void PrintSimpleTextButton_Click(object sender, RoutedEventArgs e)
		{
//			var printDlg = new PrintDialog();
//			var doc = BuildPrintForm();
//			doc.Name = "FlowDoc";
//			IDocumentPaginatorSource idpSource = doc;
//			printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
		}

		private List<string> GetBoxInfo(Box box)
		{
			var contracts = Contract.Repository.GetByBoxId(box.Id).ToList();
			return new List<string>
			{
				box.Name,
				FormatDate(box.MinDate) + " " + FormatDate(box.MaxDate),
				string.Join(", ", contracts.Select(c => c.PrefixOfPlace).Distinct()),
//				string.Join(",", contracts.Select(c => c.ToString()))
			};
		}

		private static string FormatDate(DateTime? date)
		{
			return date == null ? "" : date.GetValueOrDefault().ToString("dd/mm/yyyy");
		}

		private static FlowDocument BuildDocument(List<string> info)
		{
			var doc = new FlowDocument();
			var sec = new Section();

			var sizes = new List<int>() {50, 50, 30, 20};
			var p1 = new Paragraph
			{
				FontSize = 50,
				TextAlignment = TextAlignment.Center
			};
			info.ForEach(s => p1.Inlines.Add(s + "\n"));
			sec.Blocks.Add(p1);
			doc.Blocks.Add(sec);
			return doc;
		}

		public static void SaveAsXps(string path, FlowDocument document)
		{
			using (var package = Package.Open(path, FileMode.Create))
			{
				using (var xpsDoc = new XpsDocument(package, CompressionOption.Maximum))
				{
					var xpsSm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
					var dp = ((IDocumentPaginatorSource) document).DocumentPaginator;
					xpsSm.SaveAsXaml(dp);
				}
			}
		}
	}
}