using GSAExam.Core.Common.Commands;
using GSAExam.Core.Common.Queries;
using GSAExam.Core.CQRS.Handlers;
using GSAExam.Core.Domain.Interface;
using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Model;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace GSAExam.Core.CQRS
{
    public static class CQRSDependencyExtensions
    {
        public static IServiceCollection AddEntityQueries<TUnitOfWork, TKey, TReadModel>(this IServiceCollection services)
          where TUnitOfWork : IGSADbContext         
        {
            services.TryAddScoped<IRequestHandler<EntityListQuery<EntityResponseListModel<TReadModel>>, EntityResponseListModel<TReadModel>>, EntityListQueryHandler<TUnitOfWork, TReadModel>>();
            return services;
        }
        public static IServiceCollection AddEntitCommand<TUnitOfWork, TKey,TEntity,TCreateModel, TReadModel>(this IServiceCollection services)
        where TUnitOfWork : IGSADbContext
        where TEntity:class, Domain.Interface.IHaveIdentifier<TKey>,new()
        {
            services.TryAddScoped<IRequestHandler<EntityCreateCommand<TCreateModel,EntityResponseModel<TReadModel>>, EntityResponseModel<TReadModel>>, EntityCreateCommandHandler<TUnitOfWork,TEntity,TKey,TCreateModel, TReadModel>>();
            return services;
        }
    }
}
