

namespace Demo_Resort.Data.Model
{
    public class Account 
    {

        //primary key
        public int id { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public bool isAdmin { get; set; }

        public bool isEmployee { get; set; }
    }
}
