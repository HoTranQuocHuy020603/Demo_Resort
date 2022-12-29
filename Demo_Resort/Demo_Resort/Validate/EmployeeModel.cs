using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo_Resort.Validate
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeModel
    {
/*        [Required]
        [Range(1, int.MaxValue)]
        //primary key
        public int id { get; set; }*/

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string? firstname { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string? lastname { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string? email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 0)]
        public string? phonenumber { get; set; }

        [Required]
        [Range(0, 1)]
        public int gender { get; set; }

        [Required]
        [Range(0, 1)]
        public int position { get; set; }
    }
}
