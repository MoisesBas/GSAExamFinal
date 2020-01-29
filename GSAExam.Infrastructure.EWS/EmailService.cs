using Microsoft.Exchange.WebServices.Data;
using System;
using System.Net;

namespace GSAExam.Infrastructure.EWS
{
    public class EmailService : IEmailService
    {
        private readonly ExchangeService ExchangeService;
        public EmailService()
        {
            ExchangeService = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
        }
        public void Connect(string userName, string password)
        {
            ExchangeService.Credentials = new NetworkCredential(userName, password);
            ExchangeService.AutodiscoverUrl(userName, RedirectionUrlValidationCallback);
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        public void DownloadAttachment(string path)
        {
            if (ExchangeService != null)
            {
                FindItemsResults<Item> findResults = ExchangeService.FindItems(
              WellKnownFolderName.Inbox,
              new ItemView(10)).Result;



                foreach (Item item in findResults.Items)
                {
                    ProcessItem(item, path);
                }


            }
        }
        private bool ProcessItem(Item item, string path)
        {
            try
            {
                if (item.HasAttachments)
                    DownloadItem(item, path);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        private void DownloadItem(Item item, string path)
        {
            EmailMessage message = EmailMessage.Bind(ExchangeService, item.Id, new PropertySet(BasePropertySet.IdOnly, ItemSchema.Attachments, ItemSchema.HasAttachments)).GetAwaiter().GetResult();
            foreach (Attachment attachment in message.Attachments)
            {
                if (attachment is FileAttachment)
                {

                    if (attachment is FileAttachment)
                    {
                        FileAttachment attach = attachment as FileAttachment;
                        attach.Load(path + "\\" + attach.Name);
                    }

                }

            }
            
        }
    }
    }

