using GSAExam.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Infrastructure.Model
{
    public class EntityIdentifierModel<TKey> : IHaveIdentifier<TKey>
    {
        public TKey Id { get; set; }
    }
}
