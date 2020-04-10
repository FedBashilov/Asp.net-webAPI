using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Model;
using WebApi2;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private MyDBContext _context;
        public StudentsController(MyDBContext context)
        {
            this._context = context;
        }

        List<Student> _oStudents = new List<Student>() {
            new Student(){ Id=1, Name="Fedor", Roll=1001},
            new Student(){ Id=2, Name="Dima", Roll=1225},
            new Student(){ Id=3, Name="Nikolay", Roll=11},
        };


        [HttpGet]
        public IActionResult Gets()
        {
            return Ok(this._context.Students.First());
            if (_oStudents.Count == 0)
            {
                return NotFound("No list found");
            }
            return Ok(_oStudents);
        }

        [HttpGet("GetStudent")]
        public IActionResult Get(int id)
        {
            
            var oStudent = _oStudents.SingleOrDefault(x => x.Id == id);
            if (oStudent == null)
            {
                return NotFound("No student found");
            }
            return Ok(oStudent);
        }

        [HttpPost]
        public IActionResult Save([FromForm] Student oStudent)
        {

            Student ss = new Student();
            ss.Id = oStudent.Id;
            ss.Name = oStudent.Name;
            ss.Roll = oStudent.Roll;
            this._context.Students.Add(ss);
            Console.WriteLine("Calling SaveChanges.");
            this._context.SaveChanges();
            Console.WriteLine("SaveChanges completed.");
            // Query for all blogs ordered by name
            Console.WriteLine("Executing query.");
            /* var students = (from s in _context.Students
                          orderby s.Id
                          select s).ToList();

             // Write all blogs out to Console
             Console.WriteLine("Query completed with following results:");
             foreach (var student in students)
             {
                 Console.WriteLine(" " + student.Name);
             }
         */
            _oStudents.Add(oStudent);
            if (_oStudents.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(_oStudents);
        }

        [HttpPut]
        public IActionResult UpdateStudent([FromForm] Student oStudent)
        {
            var oldStudent = _oStudents.SingleOrDefault(x => x.Id == oStudent.Id);
            _oStudents.Remove(oldStudent);
            _oStudents.Add(oStudent);
            if (_oStudents.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(_oStudents);
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var oStudent = _oStudents.SingleOrDefault(x => x.Id == id);
            if (oStudent == null)
            {
                return NotFound("No student found");
            }
            _oStudents.Remove(oStudent);

            if (_oStudents.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(_oStudents);
        }
    }
}