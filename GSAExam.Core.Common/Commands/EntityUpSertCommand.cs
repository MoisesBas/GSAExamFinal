using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Common.Commands
{
    public class EntityUpSertCommand<TKey, TUpdateModel, TReadModel>
          : EntityModelCommand<TUpdateModel, TReadModel>
    {
        public EntityUpSertCommand(TKey id, TUpdateModel model) : base(model)
        {
            Id = id;
        }
        public TKey Id { get; }
    }
}
