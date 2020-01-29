using GSAExam.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.Persistence
{
    public static class GSADbContextSeed
    {
        public static async Task SeedAsync(GSADbContext userManager)
        {
            var student = new List<Students>();
            if (student.Any())
            {
                await userManager.SaveChangesAsync(default);
            }
        }
    }
}
