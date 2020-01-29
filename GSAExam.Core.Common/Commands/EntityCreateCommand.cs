using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GSAExam.Core.Common.Commands
{
    public class EntityCreateCommand<TCreateModel, TReadModel>
         : EntityModelCommand<TCreateModel, TReadModel>
    {
        public EntityCreateCommand( TCreateModel model) : base(model)
        {
            
        }
    }
}
