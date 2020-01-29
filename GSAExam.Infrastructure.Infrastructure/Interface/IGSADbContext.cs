using GSAExam.Core.Domain.Entities;
using GSAExam.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.Interface
{
   public interface IGSADbContext<T>: IGSADbContext where T : DbContext
    {

    }
   public interface IGSADbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
