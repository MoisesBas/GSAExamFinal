using CsvHelper.Configuration;
using GSAExam.Infrastructure.EWS.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Infrastructure.EWS.Mappers
{
    public sealed class StudentMap: ClassMap<StudentModel>
    {
        public StudentMap()
        {
            Map(m => m.Email).Name(Constants.CsvHeaders.Email);
            Map(m => m.Name).Name(Constants.CsvHeaders.Name);
            Map(m => m.Status).Name(Constants.CsvHeaders.Status);
            Map(m => m.Dates).Name(Constants.CsvHeaders.Dates);           
        }
    }
}
