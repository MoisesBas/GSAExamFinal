using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Queries
{
    public class EntityPagedModelQuery<TFilterModel, TReadModel> : IRequest<EntityPagedResult<TReadModel>>
    {
        public EntityPagedModelQuery(TFilterModel filterModel)           
        {
            FilterModel = filterModel;
        }
        public TFilterModel FilterModel { get; }
    }
}
