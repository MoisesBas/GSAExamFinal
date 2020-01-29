using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Queries
{
    public class EntityPagedQuery<TReadModel> : IRequest<TReadModel>
    {
        public EntityPagedQuery(EntityQuery query)            
        {
            Query = query;
        }
        public EntityPagedQuery( EntityQuery query, string includeProperties)          
        {
            Query = query;
            IncludeProperties = includeProperties;
        }
        public EntityQuery Query { get; }
        public string IncludeProperties { get; set; }
    }
}
