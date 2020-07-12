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
using System.Linq;
using System.Reflection;

namespace Processing
{
    public class WordProcessor : IDisposable
    {
        private const string TEMPLATE_PATH = @"C:\Projects\MyVitae\Vitae\WordGenerator\Templates\";
        private const string LANG_CODE = "de";

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

        private readonly Curriculum curriculum;
        private readonly List<IndustryVM> industries;
        private readonly List<HierarchyLevelVM> hierarchyLevels;
        private readonly List<MaritalStatusVM> maritalStatuses;
        private readonly List<CountryVM> countries;
        private readonly List<LanguageVM> languages;

        public WordProcessor(string templateName)
        {
            this.asposeHandler = new AsposeHandler(TEMPLATE_PATH, templateName);

            #region PREPARATION
            var optionsBuilder = new DbContextOptionsBuilder<VitaeContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=MyVitaeDebug;Trusted_Connection=True;MultipleActiveResultSets=true");
            var context = new VitaeContext(optionsBuilder.Options);
            this.repository = new Repository(context);
            this.curriculum = repository.GetCurriculumAsync(new Guid("a7e161f0-0ff5-45f8-851f-9b041f565abb")).Result;

            industries = repository.GetIndustries(LANG_CODE).ToList();
            hierarchyLevels = repository.GetHierarchyLevels(LANG_CODE).ToList();
            maritalStatuses = repository.GetMaritalStatuses(LANG_CODE).ToList();
            countries = repository.GetCountries(LANG_CODE).ToList();
            languages = repository.GetLanguages(LANG_CODE).ToList();
            #endregion PREPARATION
        }

        #region Api

        public void ProcessDocument()
        {
            ReplaceQRCode();
            ReplacePersonalDetails();
            ReplaceAbout();
            ReplaceCVContent();
            ReplaceLabels();

            asposeHandler.Save();
        }

        public void Dispose() { }

        #endregion

        #region Helper

        private void ReplaceQRCode()
        {
            var table = asposeHandler.FindTableElement<Table>(VM_PASSWORD);
            var variable1 = $"${{{VM_QRCODE.ToUpper()}}}";
            var variable2 = $"${{{VM_PASSWORD.ToUpper()}}}";
            //asposeHandler.ChangeImage(variable1, null);
            asposeHandler.ReplaceTextOrDeleteRow(table, variable2, "Password" ?? "-");
        }

        private void ReplaceLabels()
        {
            foreach (var property in typeof(SharedResource).GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.PropertyType == typeof(string)))
            {
                var name = property.Name;
                var value = property.GetValue(null).ToString();

                var variable = $"${{LABEL_{name.ToUpper()}}}";
                asposeHandler.ChangeText(variable, value);
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

        private void ReplacePersonalDetails()
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

        private void ReplaceCVContent()
        {
            // Experience
            var experiences = repository.GetExperiences(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_EXPERIENCES}", experiences);

            // Educations
            var educations = repository.GetEducations(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_EDUCATIONS}", educations);

            // Courses
            var courses = repository.GetCourses(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_COURSES}", courses);

            // Abroads
            var abroads = repository.GetAbroads(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_ABROADS}", abroads);

            // Languages
            var languages = repository.GetLanguageSkills(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_LANGUAGES}", languages);

            // Interests
            var interests = repository.GetInterests(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_INTERESTS}", interests);

            // Awards
            var awards = repository.GetAwards(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_AWARDS}", awards);

            // Skills
            var skills = repository.GetSkills(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_SKILLS}", skills);

            // Certificates
            var certificates = repository.GetCertificates(curriculum, LANG_CODE);
            FillTableOrDelete("${LABEL_CERTIFICATES}", certificates);

            // References
            var references = repository.GetReferences(curriculum, LANG_CODE);
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