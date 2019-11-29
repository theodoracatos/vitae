using Microsoft.EntityFrameworkCore;
using Persistency.Poco;

namespace Persistency.Data
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Person> Persons { get; set; }
    }
}
