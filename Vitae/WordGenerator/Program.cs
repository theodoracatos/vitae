using Processing;
using System.Text;

namespace WordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Projects\MyVitae\Vitae\WordGenerator\Templates\Template1.docx";

            using(var processor = new WordProcessor(path))
            {
                processor.ProcessDocument();
            }
        }
    }
}
