using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Library.Helper;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Processing
{
    class AsposeHandler : IDisposable
    {
        private readonly Document document;
        private readonly string path;

        public Document Document { get { return document; } }

        public AsposeHandler(string path, string name)
        {
            var license = new License();
            license.SetLicense($@"{CodeHelper.AssemblyDirectory}\Libs\Aspose.Words.lic");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            this.path = path;
            this.document = new Document(Path.Combine(path, name));
        }

        #region Api

        public void ChangeText(string variable, string text)
        {
            ChangeText(document, variable, text);
        }

        public void ChangeText(CompositeNode node, string variable, string text)
        {
            node.Range.Replace(variable, text, true, false);
        }

        public void FillTable<T>(string tableName, IEnumerable<T> elements, Func<CompositeNode, string, string, string> ValueResolver)
        {
            var table = (Table)FindTableElement<Table>(tableName);
            if (table != null)
            {
                Row templateRow = (Row)table.Rows[table.Rows.Count - 1].Clone(true);
                table.Rows[table.Rows.Count - 1].Remove();

                foreach (T element in elements)
                {
                    var row = FillRow((Row)templateRow.Clone(true), element, ValueResolver);
                    table.Rows.Add(row);
                }
            }
        }

        public void ReplaceTextOrDeleteRow(CompositeNode node, string variable, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ChangeText(node, variable, value);
            }
            else
            {
                DeleteTableElement<Row>(variable);
            }
        }

        public void ChangeImage(string variable, Image image)
        {
            var drawings = document.GetChildNodes(NodeType.DrawingML, true);
            var drawing = (DrawingML)drawings.ToArray().Where(d => ((DrawingML)d).AlternativeText == variable).Single();
            drawing.ImageData.SetImage(image);
        }

        public void DeleteTableElement<T>(string variable) where T : CompositeNode
        {
            var element = FindTableElement<T>(variable);
            element.PreviousSibling.Remove();
            element?.Remove();
        }

        public CompositeNode FindTableElement<T>(string variable) where T : CompositeNode
        {
            var tables = document.GetChildNodes(NodeType.Table, true);
            foreach (Table table in tables)
            {
                foreach (Row row in table.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.GetText().Contains(variable.ToUpper()))
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

        public Cell FindCell(string variable, Row row)
        {
            foreach (Cell cell in row.Cells)
            {
                if (cell.GetText().Contains(variable.ToUpper()))
                {
                    return cell;
                }
            }

            return null;
        }

        public void Dispose() { }

        public MemoryStream Save()
        {
            var fileStream = new MemoryStream();
            document.Save(fileStream, SaveFormat.Pdf);

            return fileStream;
        }

        #endregion


        #region Helper
        private Row FillRow<T>(Row row, T element, Func<CompositeNode, string, string, string> ValueResolver)
        {
            var properties = element.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                var propertyValue = property.GetValue(element)?.ToString();
                var value = ValueResolver(row, name, propertyValue);

                var variable = $"${{{name.ToUpper()}}}";
                var cell = FindCell(variable, row);
                if (cell != null)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        cell.Range.Replace(variable, value, true, false);
                    }
                    else
                    {
                        cell.ChildNodes.ToArray().Single(c => c.Range.Text.Contains(variable)).Remove();
                    }
                }
            };

            return row;
        }

        #endregion

    }

}
