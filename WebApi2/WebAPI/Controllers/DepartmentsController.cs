using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi2.Domain;

namespace WebApi2.WebAPI.Controllers
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
        public IActionResult Save([FromForm] Department oDepartment)
        {
            _context.Departments.Add(oDepartment);
            _context.SaveChanges();

            var Departments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (Departments.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(Departments);
        }

        [HttpPut]
        public IActionResult UpdateDepartment([FromForm] Department oDepartment)
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
            _context.SaveChanges();

            var Departments = _context.Departments.OrderBy(s => s.Name).ToList();

            if (Departments.Count == 0)
            {
                return NotFound("No list found.");
            }
            return Ok(Departments);
        }
    }
}
