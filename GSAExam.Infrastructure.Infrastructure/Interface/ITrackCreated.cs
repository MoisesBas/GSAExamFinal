using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Infrastructure.Interface
{
    public interface ITrackCreated
    {
        DateTimeOffset Created { get; set; }
        string CreatedBy { get; set; }
    }
}
