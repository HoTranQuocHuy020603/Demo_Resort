using Demo_Resort.Data.Model;
using Demo_Resort.Data;
using Demo_Resort.Validate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Resort.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //injection db context
        private readonly ResortContext _context;

        public AccountController(ResortContext context)
        {
            _context = context;
        }

        //APIs Areas
        [HttpGet]
        [Route("get-account")] // api/account/*..get-account..* 
        public IActionResult GetAccount()
        {
            //SELECT * FROM account
            var accounts = _context.Accounts.ToList();

            //return HTTP STATUS 200 with List accounts
            return Ok(accounts);

        }

        [HttpGet]
        [Route("get-by-id/{id}")] // api/accounts/*..get-accounts-by-id..* 
        public IActionResult GetAccountsByID(int id)
        {
            //SELECT * FROM account where id = ?
            var account = _context.Accounts
                .Where(x => x.id == id)
                .FirstOrDefault();

            if (account is null)
            {
                //HTTP Status 404
                return NotFound(new { msg = "Ko_Tim_Thay" });

            }

            return Ok(account);
        }

        //login
        [HttpPost]
        [Route("login-authenticate")]
        public IActionResult Login([FromBody] AccountModel model)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //check having account
                var existingAccount = _context.Accounts
                    .FirstOrDefault(x => x.username == model.username 
                    && x.password == model.password);

                if (existingAccount is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "User Not Found!"
                    });
                }
                return Ok(existingAccount);
                    
            }
            return BadRequest(model);
        }

        //SignUP
        [HttpPost]
        [Route("signup-register")]
        public IActionResult SignUp([FromBody] AccountModel model)
        {
            
            //validate input
            if (ModelState.IsValid)
            {

                // check if having account in db
                var existingAccount = _context.Accounts
                    .FirstOrDefault(x => x.username == model.username);

                if (existingAccount != null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Having Account"
                    });
                }
                
                var entity = new Account()
                {
                    username = model.username?.Trim(),
                    password = model.password?.Trim()
                    //only input username and password because
                    //when sign up you are a customer
                    //so if you don`t input 2 value isAdmin & isEmployee
                    //it will auto return to db value false.

                };
                _context.Accounts.Add(entity);

                try
                {
                    var result = _context.SaveChanges();

                    if (result > 0)
                    {
                        return Ok(new {msg = "Sign Up Success!"});;
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


        [HttpPost]
        [Route("add-account")]
        public IActionResult AddAccount([FromBody] AccountModel model)
        {
            //validate input
            if (ModelState.IsValid)
            {
                // check if having account in db
                var existingAccount = _context.Accounts
                    .FirstOrDefault(x => x.username == model.username);

                if (existingAccount != null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Having Account"
                    });
                }

                //save to db
                var entity = new Account()
                {
                    username = model.username?.Trim(),
                    password = model.password?.Trim(),
                    isAdmin = model.isAdmin,
                    isEmployee = model.isEmployee

                };
                _context.Accounts.Add(entity);

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
        [Route("edit-account/{id}")] // api/account/*..get-account..* 
        public IActionResult EditAccounts(int id, [FromBody] AccountModel model)
        {

            //UPDATE accounts SET ... where id = ...;

            //validate input
            if (ModelState.IsValid)
            {
                var existingAccount = _context.Accounts.Where(x => x.id == id)
                                                    .FirstOrDefault<Account>();
                if (existingAccount is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Account Not Found"
                    });
                }

                
                existingAccount.username = model.username?.Trim();
                existingAccount.password = model.password?.Trim();
                existingAccount.isAdmin = model.isAdmin;
                existingAccount.isEmployee = model.isEmployee;

                try
                {
                    // _context.Update(Account);
                    _context.Entry(existingAccount).State = EntityState.Modified;
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
        [Route("delete-accounts/{id}")] // api/accounts/*..get-accounts..* 
        public IActionResult DeleteAccounts(int id)
        {

            //delete from accounts where id = ...;

            if (ModelState.IsValid)
            {
                // validate 
                var account = _context.Accounts.FirstOrDefault(x => x.id == id);
                if (account is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Account Not Found"
                    });
                }
                // delete
                try
                {
                    //_context.Remove(account);
                    _context.Entry(account).State = EntityState.Deleted;
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(account);
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
            }) ; ;
        }


        //Search
        [HttpGet]
        [Route("search-accounts")] // api/accounts/*..get-accounts..* 
        public IActionResult SearchAccounts(string username)
        {
            //validate input
            if (ModelState.IsValid)
            {
                //SELECT * FROM ...  where ? like '%...%';           
                var accounts = _context.Accounts
                    .Where(x => x.username.Contains(username));

                if (accounts != null)
                {
                    return Ok(accounts);
                    

                }
                //HTTP Status 404
                return NotFound(new { state = false, msg = "No data found" });


            }
            else if(ModelState.IsValid == false)
            {

                //SELECT * FROM account
                var accounts = _context.Accounts.ToList();

                //return HTTP STATUS 200 with List accounts
                return Ok(accounts);


            }
            return BadRequest(new
            {
                state = false,
                msg = "Error Search"
            }); ;


        }
    }
}

