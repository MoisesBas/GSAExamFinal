using GSAExam.Core.Domain.Entities;
using GSAExam.Infrastructure.Configurations;
using GSAExam.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.Persistence
{
    public class GSADbContext : DbContext, IGSADbContext
    {
        public GSADbContext(DbContextOptions opptions) : base(opptions)
        {

        }
        public DbSet<Students> Students { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StudentConfigurations());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

#if DEBUG
            
#endif
            base.OnConfiguring(optionsBuilder);
        }
    }
}
