using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Queries
{
    public class EntityListQuery<TReadModel> : IRequest<TReadModel>
    {      

        public EntityListQuery(EntityFilter filter)            
        {
            Filter = filter;
        }
        public EntityFilter Filter { get; set; }
       

    }
}
