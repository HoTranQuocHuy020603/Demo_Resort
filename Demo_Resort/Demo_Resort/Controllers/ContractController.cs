using Demo_Resort.Data.Model;
using Demo_Resort.Data;
using Demo_Resort.Validate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using MySqlX.XDevAPI.Common;

namespace Demo_Resort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        //injection db context
        private readonly ResortContext _context;

        public ContractController(ResortContext context)
        {
            _context = context;
        }

        //APIs Areas
        [HttpGet]
        [Route("get-Contract")] // api/Contract/*..get-Contract..* 
        public IActionResult GetContract()
        {
            //SELECT * FROM Contract 
            var Contracts = _context.Contracts.ToList();

            //return HTTP STATUS 200 with List Contracts
            return Ok(Contracts);

        }

        [HttpGet]
        [Route("get-by-id/{id}")] // api/Contracts/*..get-Contracts-by-id..* 
        public IActionResult GetContractsByID(int id)
        {
            //SELECT * FROM Contract where id = ?
            var Contract = _context.Contracts
                .Where(x => x.cid == id)
                .FirstOrDefault();

            if (Contract is null)
            {
                //HTTP Status 404
                return NotFound(new { msg = "Ko_Tim_Thay" });

            }

            return Ok(Contract);
        }

        [HttpPost]
        [Route("add-Contract")]
        public IActionResult AddContract([FromBody] ContractModel model)
        {
            //validate input
            if (ModelState.IsValid)
            {
                // valid employee id
                var employee = _context.Employees
                    .FirstOrDefault(x => x.id == model.id);

                if(employee is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Employee Not Found"
                    });
                }
                //TimeSpan datediff = (model.departuredate - model.arrivaldate);
                TimeSpan datediff = model.departuredate.Subtract(model.arrivaldate);
                int totaldays = datediff.Days;
                var entity = new Contract()
                {
                    id = model.id,
                    falname = model.falname?.Trim(),
                    email = model.email?.Trim(),
                    phonenumber = model.phonenumber?.Trim(),
                    gender = model.gender,
                    arrivaldate = model.arrivaldate,
                    departuredate = model.departuredate,
                    roomtype = model.roomtype?.Trim(),
                    totalprice = model.totalprice * totaldays,
                    caterogy = model.caterogy?.Trim()
                };
                _context.Contracts.Add(entity);
                try
                {
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(new { msg = "Add Success!" }); ;
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"
                    });
                }
                catch(Exception ex)
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
        [Route("edit-Contract/{id}")] // api/Contract/*..get-Contract..* 
        public IActionResult EditContracts(int id, [FromBody] ContractModel model)
        {

            //UPDATE Contracts SET ... where id = ...;

            //validate input
            if (ModelState.IsValid)
            {
                // valid contract
                var contract = _context.Contracts.FirstOrDefault(x => x.cid == id);
                if (contract is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Contract Not Found"
                    });
                }
                // valid employee id
                var employee = _context.Employees
                    .FirstOrDefault(x => x.id == model.id);

                if (employee is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Employee Not Found"
                    });
                }
                TimeSpan datediff = model.departuredate.Subtract(model.arrivaldate);
                int totaldays = datediff.Days;
                contract.id = model.id;
                contract.falname = model.falname?.Trim();
                contract.email = model.email?.Trim();
                contract.phonenumber = model.phonenumber?.Trim();
                contract.gender = model.gender;
                contract.arrivaldate = model.arrivaldate;
                contract.departuredate = model.departuredate;
                contract.roomtype = model.roomtype?.Trim();
                contract.totalprice = model.totalprice * totaldays;
                contract.caterogy = model.caterogy?.Trim();

                try
                {
                    _context.Update(contract);
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(new { msg = "Editing Success!" });
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"
                    });
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
        [Route("delete-Contracts/{id}")] // api/Contracts/*..get-Contracts..* 
        public IActionResult DeleteContracts(int id)
        {

            //delete from Contracts where id = ...;

            if (ModelState.IsValid)
            {
                // validate 
                var Contract = _context.Contracts.FirstOrDefault(x => x.cid == id);
                if (Contract is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Contract Not Found"
                    });
                }
                // delete
                try
                {
                    //_context.Remove(Contract);
                    _context.Entry(Contract).State = EntityState.Deleted;
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(Contract);
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
        [Route("search-contracts")] // api/contracts/*..get-contracts..* 
        public IActionResult SearchAccounts(string name)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //SELECT * FROM ...  where ? like '%...%';           
                var contracts = _context.Contracts
                    .Where(x => x.falname.Contains(name));

                if (contracts != null)
                {
                    return Ok(contracts);


                }
                //HTTP Status 404
                return NotFound(new { state = false, msg = "No data found" });


            }
            else if (ModelState.IsValid == false)
            {

                //SELECT * FROM account
                var contracts = _context.Contracts.ToList();

                //return HTTP STATUS 200 with List accounts
                return Ok(contracts);


            }
            return BadRequest(new
            {
                state = false,
                msg = "Error Search"
            }); ;


        }
    }
}
