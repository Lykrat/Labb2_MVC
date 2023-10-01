using Microsoft.EntityFrameworkCore;

namespace Labb2_CallAPI_ASP.Data
{
    public class DataContext:DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{

		}
		public DbSet<Books> Books { get; set; }
	}
}
