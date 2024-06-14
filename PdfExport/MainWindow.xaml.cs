using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace PdfExport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string filepath = "C:\\Temp\\ViewModel\\";
            string filename = filepath + "Tickets.pdf";

            iTextSharp.text.Document document = new iTextSharp.text.Document();
            //PdfWriter.GetInstance(document, new FileStream("MyPDF.pdf", FileMode.Create));
            PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();


            //DirectoryInfo dir = new DirectoryInfo(folderpath);

            //FileInfo[] files = dir.GetFiles().OrderByDescending(p => p.CreationTime).ToArray();

            DirectoryInfo d = new DirectoryInfo(filepath);

            foreach (var file in d.GetFiles("*.PNG").OrderBy(p => p.CreationTime))
            {
                //Directory.Move(file.FullName, filepath + "\\TextFiles\\" + file.Name);
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(file.FullName);
                image.Alignment = Element.ALIGN_CENTER;
                image.ScaleToFit(document.PageSize.Width - 10, document.PageSize.Height / 4 - 18);
                document.Add(image);
            }

            document.Close();
        }

    }
}