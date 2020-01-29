using AutoMapper;
using GSAExam.Core.Common.Commands;
using GSAExam.Core.Common.Handlers;
using GSAExam.Core.Domain.Entities;
using GSAExam.Core.Domain.Interface;
using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSAExam.Core.CQRS.Handlers
{
    public class EntityCreateCommandHandler<TGSADbContext, TEntity, TKey, TCreateModel, TReadModel>
        : DataContextHandlerBase<TGSADbContext, EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>
        where TGSADbContext : IGSADbContext
        where TEntity : class, Domain.Interface.IHaveIdentifier<TKey>, new()
    {
        public EntityCreateCommandHandler(ILoggerFactory loggerFactory, IMapper mapper, TGSADbContext dataContext)
           : base(loggerFactory, dataContext, mapper)
        {
          
        }
        protected override async Task<EntityResponseModel<TReadModel>> ProcessAsync(EntityCreateCommand<TCreateModel, EntityResponseModel<TReadModel>> request, CancellationToken cancellationToken)
        {
            var EntityResponse = new EntityResponseModel<TReadModel>();
            var dbSet = DataContext.Set<TEntity>();
            var entiy = Mapper.Map<TEntity>(request.Model);
            dbSet.Add(entiy);           
            await DataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
            var readModel = Mapper.Map<TReadModel>(entiy);
            EntityResponse.ReturnStatus = true;
            EntityResponse.Data = readModel;
            return EntityResponse;
        }
    }
}
