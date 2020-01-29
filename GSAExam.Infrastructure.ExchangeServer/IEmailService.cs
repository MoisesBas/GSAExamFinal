using GSAExam.Infrastructure.ExchangeServer.Model;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSAExam.Infrastructure.ExchangeServer
{
   public interface IEmailService
    {
        void Connect(string userName, string password);
        void DownloadAttachment(string path);


    }
}
