using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers.Model;

namespace WebApi2
{
	public class MyDBContext : DbContext
	{
		public DbSet<Student> Students { get; set; }


		public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MyDBDatabase"].ConnectionString);
		}
	}
}
