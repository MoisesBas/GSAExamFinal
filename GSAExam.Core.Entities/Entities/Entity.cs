using GSAExam.Core.Domain.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSAExam.Core.Domain.Entities
{
    public abstract class Entity<TKey> : IHaveIdentifier<TKey>
    {

        [Column(Order = 1)]
        public TKey Id { get; set; }

    }
}
