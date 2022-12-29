using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Demo_Resort.Validate
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractModel
    {
/*        [Required]
        [Range(1, int.MaxValue)]
        //primary key
        public int cid { get; set; }*/

        [Required]
        [Range(1, int.MaxValue)]
        //forgein key
        public int id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string? falname { get; set; }

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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime arrivaldate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime departuredate { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string? roomtype { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int totalprice { get; set; }

        [StringLength(255, MinimumLength = 0)]
        public string? caterogy { get; set; }
    }
}
