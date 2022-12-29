
namespace Demo_Resort.Data.Model
{ 
    public class Customer
    {

        //primary key
        public int id { get; set; }

        public string? firstname { get; set; }

        public string? lastname { get; set; }

        public string? email { get; set; }

        public string? phonenumber { get; set; }

        public int gender { get; set; }
    }
}
