using AutoMapper;
using AutoMapper.QueryableExtensions;
using GSAExam.Core.Common.Handlers;
using GSAExam.Core.Common.Queries;
using GSAExam.Core.Domain.Entities;
using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSAExam.Core.CQRS.Handlers
{
    public class EntityListQueryHandler<TGSADbContext,TReadModel>
        : DataContextHandlerBase<TGSADbContext, EntityListQuery<EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>       
        where TGSADbContext : IGSADbContext
    {
        public EntityListQueryHandler(ILoggerFactory loggerFactory, TGSADbContext dataContext, IMapper mapper)
            : base(loggerFactory, dataContext, mapper)
        {
        }

        protected override async Task<EntityResponseListModel<TReadModel>> ProcessAsync(EntityListQuery<EntityResponseListModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var entityResponse = new EntityResponseListModel<TReadModel>();
            try
            {
                var query = DataContext.Set<Students>().AsNoTracking();
                entityResponse.Data = query.Any() ? await query                
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false) : new List<TReadModel>();
                entityResponse.ReturnStatus = true;
                return entityResponse;
            }
            catch (Exception ex)
            {
                entityResponse.ReturnMessage.Add("Record not found");
                entityResponse.ReturnStatus = false;
            }
            return entityResponse;
        }
    }
}
