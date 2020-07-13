using Microsoft.EntityFrameworkCore;

using Persistency.Data;
using Persistency.Repository;

using Processing;

using System;
using System.Threading.Tasks;

namespace WordGenerator
{
    class Program
    {
        static void Main(string[] args)
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