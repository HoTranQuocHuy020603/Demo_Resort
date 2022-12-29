using Demo_Resort.Data.Model;
using Demo_Resort.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Security;
using Demo_Resort.Data;
using MySqlX.XDevAPI.Common;

namespace Demo_Resort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //injection db context
        private readonly ResortContext _context;

        public EmployeeController(ResortContext context)
        {
            _context = context;
        }

        //APIs Areas
        [HttpGet]
        [Route("get-employee")] // api/Employee/*..get-Employee..* 
        public IActionResult GetEmployee()
        {
            //SELECT * FROM employee
            var Employees = _context.Employees.ToList();

            //return HTTP STATUS 200 with List Employees
            return Ok(Employees);

        }

        [HttpGet]
        [Route("get-by-id/{id}")] // api/Employees/*..get-Employees-by-id..* 
        public IActionResult GetEmployeesByID(int id)
        {
            //SELECT * FROM Employee where id = ?
            var Employee = _context.Employees
                .Where(x => x.id == id)
                .FirstOrDefault();

            if (Employee is null)
            {
                //HTTP Status 404
                return NotFound(new { msg = "Ko_Tim_Thay" });

            }

            return Ok(Employee);
        }

        [HttpPost]
        [Route("add-Employee")]
        public IActionResult AddEmployee([FromBody] EmployeeModel model)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //save to db
                var entity = new Employee()
                {
                    firstname = model.firstname?.Trim(),
                    lastname = model.lastname?.Trim(),
                    email = model.email?.Trim(),
                    phonenumber = model.phonenumber?.Trim(),
                    gender = model.gender,
                    position = model.position
                };
                _context.Employees.Add(entity);

                try
                {
                    var results = _context.SaveChanges();
                    if (results > 0)
                    {
                        return Ok(new { msg = "Add Success!" }); ;
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"

                    });//400

                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(model);
        }
        [HttpPut]
        [Route("edit-Employee/{id}")] // api/Employee/*..get-Employee..* 
        public IActionResult EditEmployees(int id, [FromBody] EmployeeModel model)
        {

            //UPDATE Employees SET ... where id = ...;

            //validate input
            if (ModelState.IsValid)
            {
                var existingEmployee = _context.Employees.Where(x => x.id == id)
                                                    .FirstOrDefault<Employee>();
                if (existingEmployee is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Employee Not Found"
                    });
                }


                existingEmployee.firstname = model.firstname?.Trim();
                existingEmployee.lastname = model.lastname?.Trim();
                existingEmployee.email = model.email?.Trim();
                existingEmployee.phonenumber = model.phonenumber?.Trim();
                existingEmployee.gender = model.gender;
                existingEmployee.position = model.position;

                try
                {
                    // _context.Update(Employee);
                    _context.Entry(existingEmployee).State = EntityState.Modified;
                    var result = _context.SaveChanges();

                    if (result > 0)
                    {
                        return Ok(new { msg = "Editing Success!" }); ;
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(model);
        }
        //Delete
        [HttpDelete]
        [Route("delete-Employees/{id}")] // api/Employees/*..get-Employees..* 
        public IActionResult DeleteEmployees(int id)
        {

            //delete from Employees where id = ...;

            if (ModelState.IsValid)
            {
                // validate 
                var Employee = _context.Employees.FirstOrDefault(x => x.id == id);
                if (Employee is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Employee Not Found"
                    });
                }
                // delete
                try
                {
                    //_context.Remove(Employee);
                    _context.Entry(Employee).State = EntityState.Deleted;
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(Employee);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(new
            {
                state = false,
                msg = "Cannot Delete"
            }); ;
        }

        //Search
        [HttpGet]
        [Route("search-employees")] // api/employees/*..get-employees..* 
        public IActionResult SearchAccounts(string name)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //SELECT * FROM ...  where ? like '%...%';           
                var employees = _context.Employees
                    .Where(x => x.firstname.Contains(name) ||
                    x.lastname.Contains(name));

                if (employees != null)
                {
                    return Ok(employees);


                }
                //HTTP Status 404
                return NotFound(new { state = false, msg = "No data found" });


            }
            else if (ModelState.IsValid == false)
            {

                //SELECT * FROM ...
                var employees = _context.Employees.ToList();

                //return HTTP STATUS 200 with List ...
                return Ok(employees);


            }
            return BadRequest(new
            {
                state = false,
                msg = "Error Search"
            }); ;


        }
    }
}

