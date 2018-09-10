using System;

namespace SharedModels
{
    public class EmployeeUserDescriptionViewModel
    {
          
        public int userId { get; set; }
        public string userName { get; set; }
        public Byte[] password { get; set; }
        public int empId { get; set; }
        public string employeeFirstName { get; set; }
        public string employeeLastName { get; set; }
        public DateTime employeeDOB { get; set; }

    }
}
