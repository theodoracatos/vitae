using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Library.Helper;

using Model.Poco;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private const string TEMPLATE_PATH = @"C:\Projects\MyVitae\Vitae\WordGenerator\Templates\";
        private readonly Document document;

        public WordProcessor(string templateName)
        {
            var license = new License();
            license.SetLicense($@"{CodeHelper.AssemblyDirectory}\Libs\Aspose.Words.lic");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.document = new Document(Path.Combine(TEMPLATE_PATH, templateName));
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
            ChangeImage("${PICTURE}", image);

            ChangeText("${FIRSTNAME}", "Alexandros");

            DeleteTableElement<Row>("${CHILDREN}");
            DeleteTableElement<Table>("${EXPERIENCE}");

            // TODO: Copy row and fill table

            document.Save(Path.Combine(TEMPLATE_PATH, "Out.docx"));
        }

        private void FillTable<T>(string tableName, List<T> values) where T : Base
        {
            var table = FindTableElement<Table>(tableName);
            if(table != null)
            {

            }
        }

        private void ChangeImage(string variable, Image image)
        {
            var drawings = document.GetChildNodes(NodeType.DrawingML, true);
            var drawing = (DrawingML)drawings.ToArray().Where(d => ((DrawingML)d).AlternativeText == variable).Single();
            drawing.ImageData.SetImage(image);
        }

        private void ChangeText(string variable, string text)
        {
            document.Range.Replace(variable, text, true, false);
        }

        private void DeleteTableElement<T>(string variable) where T : CompositeNode
        {
            var element = FindTableElement<T>(variable);
            element?.Remove();
        }

        private CompositeNode FindTableElement<T>(string variable) where T : CompositeNode
        {
            var tables = document.GetChildNodes(NodeType.Table, true);
            foreach (Table table in tables)
            {
                foreach (Row row in table.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.GetText().Contains(variable))
                        {
                            if (typeof(T) == typeof(Table))
                            {
                                return table;
                            }
                            else if (typeof(T) == typeof(Row))
                            {
                                return row;
                            }
                            else if (typeof(T) == typeof(Cell))
                            {
                                return cell;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public void Dispose() { }
    }
}
