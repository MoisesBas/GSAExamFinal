using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSAExam.Infrastructure.ExchangeServer.Model
{
   public class StudentModel
    {
        [Name(Constants.CsvHeaders.Email)]
        public string Email { get; set; }
        [Name(Constants.CsvHeaders.Name)]
        public string Name { get; set; }
        [Name(Constants.CsvHeaders.Status)]
        public string Status { get; set; }
       
        [Name(Constants.CsvHeaders.Dates)]
        public string Dates { get; set; }      
    }
}
