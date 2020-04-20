using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi2.Domain;

namespace WebApi2.Controllers
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

        [HttpGet]
        public IActionResult Gets()
        {
            List<Student> oStudents = _context.Students.ToList();

            if (oStudents.Count == 0)
            {
                return NotFound("No list found");
            }
            return Ok(oStudents);
        }

        [HttpGet("GetStudent")]
        public IActionResult Get(int id)
        {
            var oStudent = _context.Students.SingleOrDefault(x => x.Id == id);

            if (oStudent == null)
            {
                return NotFound("No student found");
            }
            return Ok(oStudent);
        }

        [HttpPost]
        public IActionResult Save([FromForm] Student oStudent)
        {
            _context.Students.Add(oStudent);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Student with that index already exists");
            }
            
            var students = _context.Students.OrderBy(s => s.Last_name).ToList();

            if (students.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(students);
        }


        [HttpPut]
        public IActionResult Update([FromForm] Student oStudent)
        {

            var oldStudent = _context.Students.SingleOrDefault(x => x.Id == oStudent.Id);

            if (oldStudent != null)
            {
                _context.Students.Attach(oldStudent);
                _context.Students.Remove(oldStudent);
            }
            _context.Students.Add(oStudent);
            try
            {
                _context.SaveChanges();
            }
            catch(DbUpdateException e)
            {
                return BadRequest("Department does not exist");
            }

            var students = _context.Students.OrderBy(s => s.Last_name).ToList();

            if (students.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(students);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var oStudent = _context.Students.SingleOrDefault(x => x.Id == id);

            if (oStudent == null)
            {
                return NotFound("No student found");
            }

            _context.Students.Attach(oStudent);
            _context.Students.Remove(oStudent);
            _context.SaveChanges();

            var students = _context.Students.OrderBy(s => s.Last_name).ToList();

            if (students.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(students);
        }
    }
}