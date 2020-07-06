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
using Microsoft.EntityFrameworkCore.Infrastructure;
using Library.Extensions;
using Model.Helper;
using Library.Resources;
using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private const string TEMPLATE_PATH = @"C:\Projects\MyVitae\Vitae\WordGenerator\Templates\";
        private const string LANG_CODE = "de";

        private readonly Document document;
        private readonly Repository repository;

        private Curriculum curriculum;
        private List<IndustryVM> industries;
        private List<HierarchyLevelVM> hierarchyLevels;

        public WordProcessor(string templateName)
        {
            var license = new License();
            license.SetLicense($@"{CodeHelper.AssemblyDirectory}\Libs\Aspose.Words.lic");
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this.document = new Document(Path.Combine(TEMPLATE_PATH, templateName));
            
            // Get DBContext (TEST)
            var optionsBuilder = new DbContextOptionsBuilder<VitaeContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=MyVitaeDebug;Trusted_Connection=True;MultipleActiveResultSets=true");
            var context = new VitaeContext(optionsBuilder.Options);
            this.repository = new Repository(context);
            this.curriculum = repository.GetCurriculumAsync(new Guid("a7e161f0-0ff5-45f8-851f-9b041f565abb")).Result;

            industries = repository.GetIndustries("de").ToList();
            hierarchyLevels = repository.GetHierarchyLevels("de").ToList();
        }

        public void ProcessDocument()
        {
            // Get the first table in the document.
            //var table = (Table)doc.GetChild(NodeType.Table, 0, true);

            //// Replace any instances of our string in the last cell of the table only.
            //var clonedRow = table.Rows[0].Clone(true);
            //table.Rows.Add(clonedRow);
            //DeleteTableElement<Row>("${CHILDREN}");
            //DeleteTableElement<Table>("${EXPERIENCE}");
            //table.Rows[0].Cells[0].Range.Replace("A", "B", true, true);



            ReplacePersonalDetails();
            ReplaceCVContent();






            document.Save(Path.Combine(TEMPLATE_PATH, "Out.docx"));
        }

        private void ReplacePersonalDetails()
        {
            var personalDetail = repository.GetPersonalDetail(curriculum);
 //           var about = repository.GetAbouts(curriculum, LANG_CODE).Single();
   //         var image = CodeHelper.Base64ToImage(about.Photo);

            // Personal Detail
            foreach (var property in personalDetail.GetType().GetProperties())
            {
                var name = property.Name;
                var propertyValue = property.GetValue(personalDetail)?.ToString();
                var value = ResolveValue(name, propertyValue);
                var variable = $"${{{name.ToUpper()}}}";

                ChangeText(variable, value);
            };

            // About

        }

        private void ReplaceCVContent()
        {
            var exp = repository.GetExperiences(curriculum, LANG_CODE);
            FillTable("Experience", exp);
        }

        private void FillTable<T>(string tableName, IEnumerable<T> elements) where T : BaseVM
        {
            var table = (Table)FindTableElement<Table>(tableName);
            if(table != null)
            {
                Row templateRow = (Row)table.Rows[table.Rows.Count - 1].Clone(true);
                table.Rows[table.Rows.Count - 1].Remove();

                foreach (T element in elements)
                {
                    var row = FillRow((Row)templateRow.Clone(true), element);
                    table.Rows.Add(row);
                }
            }
        }

        private Row FillRow<T>(Row row, T element)
        {
            var properties = element.GetType().GetProperties();
            foreach (var property in properties)
            {
                var name = property.Name;
                var propertyValue = property.GetValue(element)?.ToString();
                var value = ResolveValue(name, propertyValue);

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

        private string ResolveValue(string name, string value)
        {
            var result = value;

            switch(name)
            {
                case "IndustryCode":
                    {
                        result = industries.Single(i => i.IndustryCode.ToString() == value).Name;
                        break;
                    }
                case "HierarchyLevelCode":
                    {
                        result = hierarchyLevels.Single(i => i.HierarchyLevelCode.ToString() == value).Name;
                        break;
                    }
                case "Start_Date":
                    {
                        var date = DateTime.Parse(value);
                        result = date.ToShortDateCultureString();
                        break;
                    }
                case "End_Date":
                    {
                        if (DateTime.TryParse(value, out DateTime date))
                        {
                            result = date.ToShortDateCultureString();
                            break;
                        }
                        else
                        {
                            result = "-";
                            break;
                        }
                    }
                case "Difference_Date":
                    {
                        var dateDifference = new DateDifference(DateTime.Parse(value.Split(";").First()), DateTime.Parse(value.Split(";").Last()));
                        var years = dateDifference.Years == 0 ? string.Empty : dateDifference.Years == 1 ? $"{dateDifference.Years} {SharedResource.Year}" : $"{dateDifference.Years} {SharedResource.Years}";
                        var months = dateDifference.Months == 0 ? string.Empty : dateDifference.Months == 1 ? $"{dateDifference.Months} {SharedResource.Month}" : $"{dateDifference.Months} {SharedResource.Months}";
                        var days = dateDifference.Days == 0 ? string.Empty : dateDifference.Days == 1 ? $"{dateDifference.Days} {SharedResource.Day}" : $"{dateDifference.Days} {SharedResource.Days}";

                        result = (years != string.Empty ? years : string.Empty) +
                            (years != string.Empty && months != string.Empty ? ", " : string.Empty) +
                            (months != string.Empty ? months : string.Empty) +
                            (months != string.Empty && days != string.Empty ? ", " : string.Empty) +
                            (days != string.Empty ? days : string.Empty);
                        break;
                    }
            }

            return result;
        }

        public void Dispose() { }
    }
}
