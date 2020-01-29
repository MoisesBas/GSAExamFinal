using System.Threading.Tasks;

namespace GSAExam.Infrastructure.DropBox
{
  public  interface IDropBoxService
    {
        Task<bool> Process(string path);
    }
}