using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.Model
{
	public class Student
	{
		public int Id { get; set; } = 0;
		public string First_name { get; set; } = "";
		public string Last_name { get; set; } = "";
		public int Department { get; set; } = 0;
	}
}
