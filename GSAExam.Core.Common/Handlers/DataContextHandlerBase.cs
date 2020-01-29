using AutoMapper;
using GSAExam.Infrastructure.Interface;
using GSAExam.Infrastructure.Utility;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Handlers
{
    public abstract class DataContextHandlerBase<TGSADbContext, TRequest, TResponse>
       : RequestHandlerBase<TRequest, TResponse>
       where TGSADbContext : IGSADbContext
       where TRequest : IRequest<TResponse>
    {
        protected DataContextHandlerBase(ILoggerFactory loggerFactory, TGSADbContext dataContext, IMapper mapper)
            : base(loggerFactory)
        {
            Assert.NotNull(loggerFactory, nameof(loggerFactory));
            Assert.NotNull(mapper, nameof(mapper));
            DataContext = dataContext;
            Mapper = mapper;
        }
        protected TGSADbContext DataContext { get; }

        protected IMapper Mapper { get; }
    }
}
