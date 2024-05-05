using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AuthDataAccess.Models
{
    public class ReturnHashSaltModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HashPassword {  get; set; }
        public string Salt{ get; set; }

    }
}
