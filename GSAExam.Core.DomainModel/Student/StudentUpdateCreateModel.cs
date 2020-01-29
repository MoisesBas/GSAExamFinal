using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.DomainModel.Student
{
    //you need to create separate model for insert or update if you want to track rowversion, insert and update datetime and updated by someone
    public class StudentUpdateCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? Dates { get; set; }
        public string Status { get; set; }
    }
}
