using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo_Resort.Validate
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountModel 
    {
/*        [Required]
        [Range(1, int.MaxValue)]
        //primary key
        public int id { get; set; }*/

        [Required(ErrorMessage = "Please enter username!")]
        [StringLength(255, ErrorMessage = "Please enter more than 3 characters!", MinimumLength = 3)]
        public string? username { get; set; }

        [Required(ErrorMessage = "Please enter password!")]
        [StringLength(255, ErrorMessage = "Please enter more than 3 characters!", MinimumLength = 3)]
        public string? password { get; set; }


        public bool isAdmin { get; set; }


        public bool isEmployee { get; set; }
    }
}
