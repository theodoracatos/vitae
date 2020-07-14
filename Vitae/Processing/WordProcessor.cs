using Aspose.Words;
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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private const string PERSONALDETAIL_ACADEMICTITLE = "AcademicTitle";
        private const string PERSONALDETAIL_CHILDREN = "Children";
        private const string PERSONALDETAIL_INDUSTRYCODE = "IndustryCode";
        private const string PERSONALDETAIL_HIERARCHYLEVELCODE = "HierarchyLevelCode";
        private const string PERSONALDETAIL_BIRTHDAY_DATE = "Birthday_Date";
        private const string PERSONALDETAIL_MARITALSTATUSCODE = "MaritalStatusCode";
        private const string PERSONALDETAIL_NATIONALITY = "Nationalities";
        private const string VM_START_DATE = "Start_Date";
        private const string VM_END_DATE = "End_Date";
        private const string VM_START_DATE_LONG = "Start_Date_Long";
        private const string VM_END_DATE_LONG = "End_Date_Long";
        private const string VM_DIFFERENCE_DATE = "Difference_Date";
        private const string VM_DIFFERENCE_DATE_LONG = "Difference_Date_Long";
        private const string VM_LANGUAGECODE = "LanguageCode";
        private const string VM_END_DATE_OPT = "End_Date_Opt";
        private const string VM_GENDER = "Gender";
        private const string VM_PASSWORD = "Password";
        private const string VM_QRCODE = "QrCode";

        private const string ABOUT_PHOTO = "Photo";
        private const string LANG_RATE = "Rate";

        private readonly AsposeHandler asposeHandler;

        private readonly Repository repository;

        private List<IndustryVM> industries;
        private List<HierarchyLevelVM> hierarchyLevels;
        private List<MaritalStatusVM> maritalStatuses;
        private List<CountryVM> countries;
        private List<LanguageVM> languages;

        public WordProcessor(Repository repository, string templateName)
        {
            this.asposeHandler = new AsposeHandler(@$"{CodeHelper.AssemblyDirectory}/Templates", templateName);
            this.repository = repository;
        }

        #region Api

        public async Task<MemoryStream> ProcessDocument(Guid curriculumID, string languageCode, string baseUrl, string password)
        {
            var curriculum = await repository.GetCurriculumAsync(curriculumID);

            industries = repository.GetIndustries(languageCode).ToList();
            hierarchyLevels = repository.GetHierarchyLevels(languageCode).ToList();
            maritalStatuses = repository.GetMaritalStatuses(languageCode).ToList();
            countries = repository.GetCountries(languageCode).ToList();
            languages = repository.GetLanguages(languageCode).ToList();

            ReplaceQRCode(curriculum, languageCode, baseUrl, password);
            ReplacePersonalDetails(curriculum, languageCode);
            ReplaceAbout(curriculum, languageCode);
            ReplaceCVContent(curriculum, languageCode);
            ReplaceLabels(languageCode);

            return asposeHandler.Save();
        }

        public void Dispose() { }

        #endregion

        #region Helper

        private void ReplaceQRCode(Curriculum curriculum, string languageCode, string baseUrl, string password)
        {
            var table = asposeHandler.FindTableElement<Table>(VM_PASSWORD);
            var variable_qrcode = $"${{{VM_QRCODE.ToUpper()}}}";
            var variable_password = $"${{{VM_PASSWORD.ToUpper()}}}";

            var qrCode = CodeHelper.CreateQRCode($"{baseUrl}/CV/{curriculum.CurriculumID}?langCode={languageCode}");
            asposeHandler.ChangeImage(variable_qrcode, CodeHelper.Base64ToImage(qrCode));
            asposeHandler.ReplaceTextOrDeleteRow(table, variable_password, string.IsNullOrEmpty(password) ? " " : password);
        }

        private void ReplaceLabels(string languageCode)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);

            foreach (var property in typeof(SharedResource).GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType == typeof(string)))
            {
                var name = property.Name;
                var value = property.GetValue(null).ToString();

                var variable = $"${{LABEL_{name.ToUpper()}}}";
                asposeHandler.ChangeText(variable, value);
            }
        }

        private void ReplacePersonalDetails(Curriculum curriculum, string languageCode)
        {
            var personalDetail = repository.GetPersonalDetail(curriculum);
            var table1 = asposeHandler.FindTableElement<Table>(PERSONALDETAIL_ACADEMICTITLE);
            var table2 = asposeHandler.FindTableElement<Table>(PERSONALDETAIL_CHILDREN);

            // Personal Detail
            foreach (var property in personalDetail.GetType().GetProperties())
            {
                var name = property.Name;
                var variable = $"${{{name.ToUpper()}}}";
                var value = string.Empty;

                switch (name)
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
                            value = ResolveValue(asposeHandler.Document, name, propertyValue);
                            break;
                        }
                }
                asposeHandler.ReplaceTextOrDeleteRow(table1, variable, value);
                asposeHandler.ReplaceTextOrDeleteRow(table2, variable, value);
            };
        }

        private void ReplaceAbout(Curriculum curriculum, string languageCode)
        {
            var about = repository.GetAbouts(curriculum, languageCode).Single();

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
                            asposeHandler.ChangeImage(variable, image);
                            break;
                        }
                    default:
                        {
                            var value = ResolveValue(asposeHandler.Document, name, propertyValue);
                            asposeHandler.ReplaceTextOrDeleteRow(asposeHandler.Document, variable, value);
                            break;
                        }
                }
            };
        }

        private void ReplaceCVContent(Curriculum curriculum, string languageCode)
        {
            // Experience
            var experiences = repository.GetExperiences(curriculum, languageCode);
            FillTableOrDelete("${LABEL_EXPERIENCES}", experiences);

            // Educations
            var educations = repository.GetEducations(curriculum, languageCode);
            FillTableOrDelete("${LABEL_EDUCATIONS}", educations);

            // Courses
            var courses = repository.GetCourses(curriculum, languageCode);
            FillTableOrDelete("${LABEL_COURSES}", courses);

            // Abroads
            var abroads = repository.GetAbroads(curriculum, languageCode);
            FillTableOrDelete("${LABEL_ABROADS}", abroads);

            // Languages
            var languages = repository.GetLanguageSkills(curriculum, languageCode);
            FillTableOrDelete("${LABEL_LANGUAGES}", languages);

            // Interests
            var interests = repository.GetInterests(curriculum, languageCode);
            FillTableOrDelete("${LABEL_INTERESTS}", interests);

            // Awards
            var awards = repository.GetAwards(curriculum, languageCode);
            FillTableOrDelete("${LABEL_AWARDS}", awards);

            // Skills
            var skills = repository.GetSkills(curriculum, languageCode);
            FillTableOrDelete("${LABEL_SKILLS}", skills);

            // Certificates
            var certificates = repository.GetCertificates(curriculum, languageCode);
            FillTableOrDelete("${LABEL_CERTIFICATES}", certificates);

            // References
            var references = repository.GetReferences(curriculum, languageCode);
            FillTableOrDelete("${LABEL_REFERENCES}", references.Where(r => !r.Hide));
        }

        private void FillTableOrDelete(string variable, IEnumerable<BaseVM> viewModels)
        {
            if (viewModels.Count() > 0)
            {
                asposeHandler.FillTable(variable, viewModels, ResolveValue);
            }
            else
            {
                asposeHandler.DeleteTableElement<Table>(variable);
            }
        }

        private string ResolveValue(CompositeNode element, string name, string value)
        {
            var result = value;

            switch (name)
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
                        }
                        else
                        {
                            result = SharedResource.UntilNow;
                        }
                        break;
                    }
                case VM_START_DATE_LONG:
                case VM_END_DATE_LONG:
                    {
                        var date = DateTime.Parse(value);
                        result = date.ToLongDateCultureString();
                        break;
                    }
                case VM_DIFFERENCE_DATE:
                    {
                        var dateDifference = new DateDifference(DateTime.Parse(value.Split(";").First()), DateTime.Parse(value.Split(";").Last()));
                        var years = dateDifference.Years == 0 ? string.Empty : dateDifference.Years == 1 ? $"{dateDifference.Years} {SharedResource.Year}" : $"{dateDifference.Years} {SharedResource.Years}";
                        var months = dateDifference.Months == 0 ? string.Empty : dateDifference.Months == 1 ? $"{dateDifference.Months} {SharedResource.Month}" : $"{dateDifference.Months} {SharedResource.Months}";

                        result = (years != string.Empty ? years : string.Empty) +
                            (years != string.Empty && months != string.Empty ? ", " : string.Empty) +
                            (months != string.Empty ? months : string.Empty);
                        break;
                    }
                case VM_DIFFERENCE_DATE_LONG:
                    {
                        var dateDifference = new DateDifference(DateTime.Parse(value.Split(";").First()), DateTime.Parse(value.Split(";").Last()));
                        var years = dateDifference.Years == 0 ? string.Empty : dateDifference.Years == 1 ? $"{dateDifference.Years} {SharedResource.Year}" : $"{dateDifference.Years} {SharedResource.Years}";
                        var months = dateDifference.Months == 0 ? string.Empty : dateDifference.Months == 1 ? $"{dateDifference.Months} {SharedResource.Month}" : $"{dateDifference.Months} {SharedResource.Months}";
                        var days = dateDifference.Days == 0 ? $"{dateDifference.Days + 1} {SharedResource.Day}" : $"{dateDifference.Days + 1} {SharedResource.Days}";

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
                case VM_LANGUAGECODE:
                    {
                        result = languages.Single(l => l.LanguageCode == value).Name;
                        break;
                    }
                case LANG_RATE:
                    {
                        var variable = $"${{{name.ToUpper()}}}";
                        var variable_star = $"${{STAR}}";
                        var cell = asposeHandler.FindCell(variable, (Row)element);

                        switch (int.Parse(value))
                        {
                            case 1:
                                result = SharedResource.KnowledgeBasic;
                                asposeHandler.ReplaceTextOrDeleteRow(((Cell)cell.NextSibling), variable_star, "★");
                                break;
                            case 2:
                                result = SharedResource.KnowledgeBusinessFluent;
                                asposeHandler.ReplaceTextOrDeleteRow(((Cell)cell.NextSibling), variable_star, "★★");
                                break;
                            case 3:
                                result = SharedResource.KnowledgeFluent;
                                asposeHandler.ReplaceTextOrDeleteRow(((Cell)cell.NextSibling), variable_star, "★★★");
                                break;
                            case 4:
                                result = SharedResource.KnowledgeNative;
                                asposeHandler.ReplaceTextOrDeleteRow(((Cell)cell.NextSibling), variable_star, "★★★★");
                                break;
                        }

                        break;
                    }
                case VM_END_DATE_OPT:
                    {
                        if (DateTime.TryParse(value, out DateTime date))
                        {
                            result = date.ToShortDateCultureString();
                        }
                        else
                        {
                            result = SharedResource.NeverExpires;
                        }
                        break;
                    }
                case VM_GENDER:
                    {
                        if (bool.Parse(value))
                        {
                            result = SharedResource.Mr;
                        }
                        else
                        {
                            result = SharedResource.Ms;
                        }
                        break;
                    }
            }

            return result;
        }

        #endregion

    }
}