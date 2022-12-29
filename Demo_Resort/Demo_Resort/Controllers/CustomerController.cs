using Demo_Resort.Data.Model;
using Demo_Resort.Data;
using Demo_Resort.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;

namespace Demo_Resort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //injection db context
        private readonly ResortContext _context;

        public CustomerController(ResortContext context)
        {
            _context = context;
        }

        //APIs Areas
        [HttpGet]
        [Route("get-Customer")] // api/Customer/*..get-Customer..* 
        public IActionResult GetCustomer()
        {
            //SELECT * FROM Customer
            var Customers = _context.Customers.ToList();

            //return HTTP STATUS 200 with List Customers
            return Ok(Customers);

        }

        [HttpGet]
        [Route("get-by-id/{id}")] // api/Customers/*..get-Customers-by-id..* 
        public IActionResult GetCustomersByID(int id)
        {
            //SELECT * FROM Customer where id = ?
            var Customer = _context.Customers
                .Where(x => x.id == id)
                .FirstOrDefault();

            if (Customer is null)
            {
                //HTTP Status 404
                return NotFound(new { msg = "Ko_Tim_Thay" });

            }

            return Ok(Customer);
        }

        [HttpPost]
        [Route("add-Customer")]
        public IActionResult AddCustomer([FromBody] CustomerModel model)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //save to db
                var entity = new Customer()
                {
                    firstname = model.firstname?.Trim(),
                    lastname = model.lastname?.Trim(),
                    email = model.email?.Trim(),
                    phonenumber = model.phonenumber?.Trim(),
                    gender = model.gender
                };
                _context.Customers.Add(entity);

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
        [Route("edit-Customer/{id}")] // api/Customer/*..get-Customer..* 
        public IActionResult EditCustomers(int id, [FromBody] CustomerModel model)
        {

            //UPDATE Customers SET ... where id = ...;

            //validate input
            if (ModelState.IsValid)
            {
                var existingCustomer = _context.Customers.Where(x => x.id == id)
                                                    .FirstOrDefault<Customer>();
                if (existingCustomer is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Customer Not Found"
                    });
                }


                existingCustomer.firstname = model.firstname?.Trim();
                existingCustomer.lastname = model.lastname?.Trim();
                existingCustomer.email = model.email?.Trim();
                existingCustomer.phonenumber = model.phonenumber?.Trim();
                existingCustomer.gender = model.gender;

                try
                {
                    // _context.Update(Customer);
                    _context.Entry(existingCustomer).State = EntityState.Modified;
                    var result = _context.SaveChanges();

                    if (result > 0)
                    {
                        return Ok(new { msg = "Edditing Success!" }); ;
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
        [Route("delete-Customers/{id}")] // api/Customers/*..get-Customers..* 
        public IActionResult DeleteCustomers(int id)
        {

            //delete from Customers where id = ...;

            if (ModelState.IsValid)
            {
                // validate 
                var Customer = _context.Customers.FirstOrDefault(x => x.id == id);
                if (Customer is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Customer Not Found"
                    });
                }
                // delete
                try
                {
                    //_context.Remove(Customer);
                    _context.Entry(Customer).State = EntityState.Deleted;
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(Customer);
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
        [Route("search-customers")] // api/customers/*..get-customers..* 
        public IActionResult SearchAccounts(string name)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //SELECT * FROM ...  where ? like '%...%';           
                var customers = _context.Customers
                    .Where(x => x.firstname.Contains(name) ||
                    x.lastname.Contains(name));

                if (customers != null)
                {
                    return Ok(customers);


                }
                //HTTP Status 404
                return NotFound(new { state = false, msg = "No data found" });


            }
            else if (ModelState.IsValid == false)
            {

                //SELECT * FROM ..
                var customers = _context.Customers.ToList();

                //return HTTP STATUS 200 with List ...
                return Ok(customers);


            }
            return BadRequest(new
            {
                state = false,
                msg = "Error Search"
            }); ;


        }
    }
}
