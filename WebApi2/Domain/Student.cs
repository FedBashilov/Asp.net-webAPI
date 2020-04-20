using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi2.Domain
{
	public class Student
	{
		public int Id { get; set; } = 0;
		public string First_name { get; set; } = "";
		public string Last_name { get; set; } = "";
		public int Department { get; set; } = 0;
	}
}
