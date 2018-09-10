using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solutions.Data;
using Solutions.Data.DataModel;
using System.ComponentModel.DataAnnotations;

namespace SharedModels
{
    public class AccountViewModel
    {
        public User User { get; set; }
        public Employee Employee { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
