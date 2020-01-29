using GSAExam.Infrastructure.Model;
using System;

namespace GSAExam.Core.DomainModel.Student
{
  
        public class StudentReadModel 
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public DateTime? Dates { get; set; }
            public string Status { get; set; }
        }
    
}
