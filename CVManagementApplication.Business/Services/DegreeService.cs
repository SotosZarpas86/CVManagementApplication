using AutoMapper;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;

namespace CVManagementApplication.Business.Services
{
    public class DegreeService : IDegreeService
    {
        private readonly IDegreeRepository _degreeRepository;
        private readonly IMapper _mapper;

        public DegreeService(IDegreeRepository degreeRepository, IMapper mapper)
        {
            _degreeRepository = degreeRepository;
            _mapper = mapper;
        }

        public async Task<IList<DegreeModel>> GetAll()
        {
            var degreeEntities = await _degreeRepository.GetAll();
            var degreeModels = new List<DegreeModel>();
            _mapper.Map(degreeEntities, degreeModels);
            return degreeModels;
        }

        public async Task<DegreeModel> Create(DegreeCreateModel model)
        {
            var degreeEntity = new Degree();
            _mapper.Map(model, degreeEntity);

            var result = await _degreeRepository.Create(degreeEntity);

            var createDegree = new DegreeModel();
            _mapper.Map(result, createDegree);

            return createDegree;
        }


        public async Task<DegreeModel> Update(int Id, DegreeModel model)
        {
            var degreeEntity = new Degree();
            degreeEntity.Id = Id;
            _mapper.Map(model, degreeEntity);

            var result = await _degreeRepository.Update(degreeEntity);

            if (result == null)
                return null;

            return model;
        }

        public async Task<bool> Delete(int candidateId)
        {
            var response = await _degreeRepository.Delete(candidateId);
            return response;
        }
    }
}
