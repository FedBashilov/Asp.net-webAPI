using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private MyDBContext _context;
        public DepartmentsController(MyDBContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult Gets()
        {
            var oDepartments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (oDepartments.Count == 0)
            {
                return NotFound("No list found");
            }
            return Ok(oDepartments);
        }

        [HttpGet("GetDepartment")]
        public IActionResult Get(int id)
        {
            var oDepartment = _context.Departments.SingleOrDefault(x => x.Id == id);

            if (oDepartment == null)
            {
                return NotFound("No Department found");
            }
            return Ok(oDepartment);
        }

        [HttpPost]
        public IActionResult Save(Department oDepartment)
        {
            _context.Departments.Add(oDepartment);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Department with that index already exists");
            }

            var Departments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (Departments.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(Departments);
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department oDepartment)
        {

            var oldDepartment = _context.Departments.SingleOrDefault(x => x.Id == oDepartment.Id);

            if (oldDepartment != null)
            {
                _context.Departments.Attach(oldDepartment);
                _context.Departments.Remove(oldDepartment);
            }
            _context.Departments.Add(oDepartment);
            _context.SaveChanges();

            var Departments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (Departments.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(Departments);
        }

        [HttpDelete]
        public IActionResult DeleteDepartment(int id)
        {
            var oDepartment = _context.Departments.SingleOrDefault(x => x.Id == id);

            if (oDepartment == null)
            {
                return NotFound("No Department found");
            }

            _context.Departments.Attach(oDepartment);
            _context.Departments.Remove(oDepartment);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                return BadRequest("Cannot delete department. There are students in it");
            }

            var Departments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (Departments.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(Departments);
        }

        [HttpDelete("DeleteEmptyDepartments")]
        public IActionResult DeleteEmptyDepartments()
        {
            List<Department> oDepartments = _context.Departments.ToList();

            if (oDepartments.Count == 0)
            {
                return NotFound("No departments found");
            }

            oDepartments.ForEach((elem) =>
            {
                if (_context.Students.First(x => x.Department == elem.Id) == null)
                {
                    _context.Departments.Attach(elem);
                    _context.Departments.Remove(elem);
                    _context.SaveChanges();
                }
            });

            oDepartments = _context.Departments.ToList();

            return Ok(oDepartments);
        }

    }
}
