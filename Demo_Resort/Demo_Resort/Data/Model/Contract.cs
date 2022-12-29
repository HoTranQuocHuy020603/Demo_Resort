

namespace Demo_Resort.Data.Model
{

    public class Contract 
    {

        //primary key
        public int cid { get; set; }

        //forgein key
        public int id { get; set; }

        public string? falname { get; set; }

        public string? email { get; set; }

        public string? phonenumber { get; set; }

        public int gender { get; set; }

        public DateTime arrivaldate { get; set; }

        public DateTime departuredate { get; set; }

        public string? roomtype { get; set; }

        public int totalprice { get; set; }

        public string? caterogy { get; set; }
    }
}
