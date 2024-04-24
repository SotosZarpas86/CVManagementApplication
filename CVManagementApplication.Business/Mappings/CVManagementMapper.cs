using AutoMapper;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Entities;

namespace CVManagementApplication.Business.Mappings
{
    public class CVManagementMapper : Profile
    {
        public CVManagementMapper()
        {
            CreateMap<Candidate, CandidateModel>().ReverseMap();
            CreateMap<CandidateCreateModel, Candidate>();

            CreateMap<Degree, DegreeModel>().ReverseMap();
            CreateMap<DegreeCreateModel, Degree>();
        }
    }
}
