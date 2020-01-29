using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Commands
{
    public abstract class EntityIdentifierCommand<TKey, TReadModel>
          : IRequest<TReadModel>
    {       
        protected EntityIdentifierCommand(TKey id)           
        {
            Id = id;
        }
        public TKey Id { get; }
    }
}
