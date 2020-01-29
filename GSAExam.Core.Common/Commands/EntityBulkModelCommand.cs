using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Commands
{
    public abstract class EntityBulkModelCommand<TEntityModel, TReadModel> : IRequest<TReadModel>
    {
        protected EntityBulkModelCommand(IEnumerable<TEntityModel> model) 
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }
        public IEnumerable<TEntityModel> Model { get; set; }
    }
}
