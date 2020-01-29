using System;

namespace GSAExam.Core.Domain.Entities
{
    public sealed class Students: Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? Dates { get; set; }
        public string Status { get; set; }
    }
}
