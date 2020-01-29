using GSAExam.Infrastructure.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GSAExam.Core.Common.Commands
{
    public abstract class EntityModelCommand<TEntityModel, TReadModel> : IRequest<TReadModel>
    {
        protected EntityModelCommand(TEntityModel model) 
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            Model = model;
        }
        public TEntityModel Model { get; set; }

    }
}
