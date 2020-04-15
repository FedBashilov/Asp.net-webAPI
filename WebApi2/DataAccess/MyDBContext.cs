using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers.Model;
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
	}
}
