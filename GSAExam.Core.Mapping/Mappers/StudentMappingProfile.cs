using AutoMapper;
using GSAExam.Core.DomainModel.Student;

namespace GSAExam.Core.Mapping.Mappers
{
    public class StudentMappingProfile: Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<StudentUpdateCreateModel, GSAExam.Core.Domain.Entities.Students>();
            CreateMap<GSAExam.Core.Domain.Entities.Students, StudentReadModel>();
        }
    }
}
