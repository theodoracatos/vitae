using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Library.Helper;

using System;
using System.Drawing;
using System.Text;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private readonly string filePath;
        private readonly Document document;

        public WordProcessor(string filePath)
        {
            var license = new License();
            license.SetLicense($@"{CodeHelper.AssemblyDirectory}\Libs\Aspose.Words.lic");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.filePath = filePath;
            this.document = new Document(filePath);
        }

        public void ProcessDocument()
        {
            // Get the first table in the document.
            //var table = (Table)doc.GetChild(NodeType.Table, 0, true);

            //// Replace any instances of our string in the last cell of the table only.
            //var clonedRow = table.Rows[0].Clone(true);
            //table.Rows.Add(clonedRow);

            //table.Rows[0].Cells[0].Range.Replace("A", "B", true, true);

            var image = Image.FromFile(@"C:\Temp\ATH.jpg");
            ChangeImage(image);

            document.Save(filePath + "_out.docx");
        }

        private void ChangeImage(Image image, int position = 0)
        {
            var drawings = document.GetChildNodes(NodeType.DrawingML, true);
            var drawing = (DrawingML)drawings[position];
            drawing.ImageData.SetImage(image);
        }

        public void Dispose() { }
    }
}
