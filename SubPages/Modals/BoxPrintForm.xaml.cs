using System.Windows;
using WpfApp.Domain;

namespace WpfApp.SubPages.Modals
{
	public partial class BoxPrintForm : Window
	{
		public BoxPrintForm(Box box)
		{
			InitializeComponent();

			BuildPdf(box);
		}

		private static void BuildPdf(Box box)
		{
//			var pdf = new PdfDocument();
//			pdf.Info.Title = "My First PDF";
//			var pdfPage = pdf.AddPage();
//			var graph = XGraphics.FromPdfPage(pdfPage);
//			var font = new XFont("Verdana", 20);
//			graph.DrawString("This is my first PDF document", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.Center);
//			const string pdfFilename = "firstpage.pdf";
//			pdf.Save(pdfFilename);
//			Process.Start(pdfFilename);
		}
	}
}
