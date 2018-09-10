using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CreateEmployeeApp.Models
{
    public class EmployeeModel
    {
        [Required]
        public int id { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public String firstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public String lastName { get; set; }

        [DisplayName("DOB")]
        //[Required(ErrorMessage = "DOB is required (YYYY-MM-DD).")]
        public DateTime DOB { get; set; }

        [Required]
 
        public int employee_Id { get; set; }











    }
}