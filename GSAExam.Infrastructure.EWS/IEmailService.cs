namespace GSAExam.Infrastructure.EWS
{
    public interface IEmailService
    {
        void Connect(string userName, string password);
        void DownloadAttachment(string path);


    }
}
