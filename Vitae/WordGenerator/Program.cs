using Processing;
using System.Text;

namespace WordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var processor = new WordProcessor("Template1.docx"))
            {
                processor.ProcessDocument();
            }
        }
    }
}
