using Library.Constants;
using Library.Helper;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Model.Helper;

using Persistency.Data;
using Persistency.Repository;

using Processing;

using System;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CodeHelperTest
    {
        [TestMethod]
        public void MonthYearTest1()
        {
            var startDate = new DateTime(2000, 1, 1);
            var endDate = new DateTime(2001, 1, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 1);
            Assert.IsTrue(dateDifference.Months == 0);
        }

        [TestMethod]
        public void MonthYearTest2()
        {
            var startDate = new DateTime(2000, 2, 1);
            var endDate = new DateTime(2001, 1, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 0);
            Assert.IsTrue(dateDifference.Months == 11);
        }

        [TestMethod]
        public void MonthYearTest3()
        {
            var startDate = new DateTime(2000, 2, 1);
            var endDate = new DateTime(2001, 2, 1);

            var dateDifference = new DateDifference(startDate, endDate);

            Assert.IsTrue(dateDifference.Years == 1);
            Assert.IsTrue(dateDifference.Months == 0);
        }

        [TestMethod]
        public void EncoderTest()
        {
            var salt = Globals.APPLICATION_NAME;

            var message1 = AesHandler.Encrypt("6LcGpfcUAAAAAIEmHWQQ1wkzunv_WnMGeUj54zOj", salt);
            var message2 = AesHandler.Encrypt("tQMwhRHI3sKVMriSOrLJjoAB", salt);
            var message3 = AesHandler.Encrypt("06f2da524c578cf1ed1745a23f1ac45c", salt);
            var message4 = AesHandler.Encrypt("HH69tOPtc419rk._YkbsUU60D_-Y35_0S1", salt);
            var message5 = AesHandler.Encrypt("czyxEqXtqiS4kYiehmm0Ut0e7zkoVnTYflgrq9aKRkrE7gVeJD", salt);
        }

        [TestMethod]
        public void CVDownloadTest()
        {
            #region PREPARATION
            var optionsBuilder = new DbContextOptionsBuilder<VitaeContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=MyVitaeDebug;Trusted_Connection=True;MultipleActiveResultSets=true");
            var context = new VitaeContext(optionsBuilder.Options);
            var repository = new Repository(context);

            #endregion PREPARATION

            using (var processor = new WordProcessor(repository, "Template1.docx"))
            {
                Task.FromResult(processor.ProcessDocument(new Guid("a7e161f0-0ff5-45f8-851f-9b041f565abb"), "de", "https://localhost", "Test"));
            }
        }
    }
}