using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Core.Domain.Interface
{
    public interface IHaveIdentifier<TKey>
    {
        TKey Id { get; set; }
    }
}
