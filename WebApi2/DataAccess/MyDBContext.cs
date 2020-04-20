using Microsoft.EntityFrameworkCore;
using WebApi2.Domain;

namespace WebApi2
{
	public class MyDBContext : DbContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Department> Departments { get; set; }


		public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		public void MarkAsModified(Student item)
		{
			Entry(item).State = EntityState.Modified;
		}
	}
}
