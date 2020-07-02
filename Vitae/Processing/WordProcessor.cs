using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Library.Helper;

using Model.Poco;

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Model.ViewModels;
using Persistency.Repository;
using Persistency.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

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
            //DeleteTableElement<Table>("${EXPERIENCE}");

            // TODO: Copy row and fill table
            var optionsBuilder = new DbContextOptionsBuilder<VitaeContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=MyVitaeDebug;Trusted_Connection=True;MultipleActiveResultSets=true");
            var context = new VitaeContext(optionsBuilder.Options);
            var repo = new Repository(context);
            var curriculum = repo.GetCurriculumAsync(new Guid("a7e161f0-0ff5-45f8-851f-9b041f565abb")).Result;
            var exp = repo.GetExperiences(curriculum, "de").ToList();
            FillTable<ExperienceVM>("Experience", exp);

            document.Save(Path.Combine(TEMPLATE_PATH, "Out.docx"));
        }

        private void FillTable<T>(string tableName, List<T> elements) where T : BaseVM
        {
            var table = (Table)FindTableElement<Table>(tableName);
            if(table != null)
            {
                Row templateRow = (Row)table.Rows[table.Rows.Count - 1].Clone(true);
                table.Rows[table.Rows.Count - 1].Remove();

                foreach (T element in elements)
                {
                    var row = FillTemplateRow<T>((Row)templateRow.Clone(true), element);
                    table.Rows.Add(row);
                }
            }
        }

        private Row FillTemplateRow<T>(Row row, T element)
        {
            FindProperties(row);
            var properties = element.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                var value = property.GetValue(element)?.ToString();

                var variable = $"${{{name.ToUpper()}}}";
                var cell = FindCell(variable, row);
                if (cell != null)
                {
                    cell.Range.Replace(variable, value, true, false);
                }
            };

            return row;
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

        private List<string> FindProperties(Row row)
        {
            var x = row.Range.Text;

            return null;
        }

        private Cell FindCell(string variable, Row row)
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

        private CompositeNode FindTableElement<T>(string variable) where T : CompositeNode
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

        public void Dispose() { }
    }
}
