using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;

using Library.Extensions;
using Library.Helper;
using Library.Resources;

using Microsoft.EntityFrameworkCore;

using Model.Helper;
using Model.Poco;
using Model.ViewModels;

using Persistency.Data;
using Persistency.Repository;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private const string TEMPLATE_PATH = @"C:\Projects\MyVitae\Vitae\WordGenerator\Templates\";
        private const string LANG_CODE = "de";

        private const string PERSONALDETAIL_CHILDREN = "Children";
        private const string PERSONALDETAIL_INDUSTRYCODE = "IndustryCode";
        private const string PERSONALDETAIL_HIERARCHYLEVELCODE = "HierarchyLevelCode";
        private const string PERSONALDETAIL_BIRTHDAY_DATE = "Birthday_Date";
        private const string PERSONALDETAIL_MARITALSTATUSCODE = "MaritalStatusCode";
        private const string PERSONALDETAIL_NATIONALITY = "Nationalities";
        private const string VM_START_DATE = "Start_Date";
        private const string VM_END_DATE = "End_Date";
        private const string VM_DIFFERENCE_DATE = "Difference_Date";
        private const string VM_COUNTRYCODE = "CountryCode";
        private const string ABOUT_PHOTO = "Photo";


        private readonly Document document;
        private readonly Repository repository;

        private readonly Curriculum curriculum;
        private readonly List<IndustryVM> industries;
        private readonly List<HierarchyLevelVM> hierarchyLevels;
        private readonly List<MaritalStatusVM> maritalStatuses;
        private readonly List<CountryVM> countries;

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

            industries = repository.GetIndustries(LANG_CODE).ToList();
            hierarchyLevels = repository.GetHierarchyLevels(LANG_CODE).ToList();
            maritalStatuses = repository.GetMaritalStatuses(LANG_CODE).ToList();
            countries = repository.GetCountries(LANG_CODE).ToList();
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
            ReplaceAbout();
            ReplaceCVContent();
            ReplaceLabels();


            document.Save(Path.Combine(TEMPLATE_PATH, "Out.docx"));
        }

        private void ReplaceLabels()
        {
            foreach (var property in typeof(SharedResource).GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType == typeof(string)))
            {
                var name = property.Name;
                var value = property.GetValue(null).ToString();
     
                var variable = $"${{LABEL_{name.ToUpper()}}}";
                ChangeText(variable, value);
            }
        }

        private void ReplaceAbout()
        {
            var about = repository.GetAbouts(curriculum, LANG_CODE).Single();

            foreach (var property in about.GetType().GetProperties())
            {
                var name = property.Name;
                var variable = $"${{{name.ToUpper()}}}";
                var propertyValue = property.GetValue(about)?.ToString();

                switch (name)
                {
                    case ABOUT_PHOTO:
                        {
                            var image = CodeHelper.Base64ToImage(propertyValue);
                            ChangeImage(variable, image);
                            break;
                        }
                    default:
                        {
                            var value = ResolveValue(name, propertyValue);
                            ReplaceTextOrDeleteRow(variable, value);
                            break;
                        }
                }
            };
        }

        private void ReplacePersonalDetails()
        {
            var personalDetail = repository.GetPersonalDetail(curriculum);

            // Personal Detail
            foreach (var property in personalDetail.GetType().GetProperties())
            {
                var name = property.Name;
                var variable = $"${{{name.ToUpper()}}}";
                var value = string.Empty;

                switch(name)
                {
                    case PERSONALDETAIL_CHILDREN:
                        {
                            value = string.Join(", ", personalDetail.Children.Select(c => $"{c.Firstname} ({CodeHelper.GetAge(c.Birthday_Date)} {(CodeHelper.GetAge(c.Birthday_Date) == 1 ? SharedResource.YearOld : SharedResource.YearsOld)})"));
                            break;
                        }
                    case PERSONALDETAIL_NATIONALITY:
                        {
                            value = string.Join(", ", personalDetail.Nationalities.Select(n => $"{countries.Single(c => c.CountryCode == n.CountryCode).Name}"));
                            break;
                        }
                    default:
                        {
                            var propertyValue = property.GetValue(personalDetail)?.ToString();
                            value = ResolveValue(name, propertyValue);
                            break;
                        }
                }

                ReplaceTextOrDeleteRow(variable, value);
            };
        }

        private void ReplaceTextOrDeleteRow(string variable, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                ChangeText(variable, value);
            }
            else
            {
                DeleteTableElement<Row>(variable);
            }
        }

        private void ReplaceCVContent()
        {
            // Experience
            var experience = "${LABEL_EXPERIENCES}";
            var exp = repository.GetExperiences(curriculum, LANG_CODE);
            if (exp.Count > 0)
            {
                FillTable(experience, exp);
            }
            else
            {
                DeleteTableElement<Table>(experience);
            }

            // Educations
            var educations = "${LABEL_EDUCATIONS}";
            var edu = repository.GetEducations(curriculum, LANG_CODE);
            if(edu.Count > 0)
            {
                FillTable(educations, edu);
            }
            else
            {
                DeleteTableElement<Table>(educations);
            }
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
                case PERSONALDETAIL_INDUSTRYCODE:
                    {
                        result = industries.Single(i => i.IndustryCode.ToString() == value).Name;
                        break;
                    }
                case PERSONALDETAIL_HIERARCHYLEVELCODE:
                    {
                        result = hierarchyLevels.Single(i => i.HierarchyLevelCode.ToString() == value).Name;
                        break;
                    }
                case VM_START_DATE:
                    {
                        var date = DateTime.Parse(value);
                        result = date.ToShortDateCultureString();
                        break;
                    }
                case VM_END_DATE:
                    {
                        if (DateTime.TryParse(value, out DateTime date))
                        {
                            result = date.ToShortDateCultureString();
                            break;
                        }
                        else
                        {
                            result = SharedResource.UntilNow;
                            break;
                        }
                    }
                case VM_DIFFERENCE_DATE:
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
                case PERSONALDETAIL_BIRTHDAY_DATE:
                    {
                        var date = DateTime.Parse(value);
                        result = date.ToLongDateCultureString();
                        break;
                    }
                case PERSONALDETAIL_MARITALSTATUSCODE:
                    {
                        result = maritalStatuses.Single(m => m.MaritalStatusCode.ToString() == value).Name;
                        break;
                    }

            }

            return result;
        }

        public void Dispose() { }
    }
}
