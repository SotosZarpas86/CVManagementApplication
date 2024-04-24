using AutoMapper;
using CVManagementApplication.Core.Domain;
using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;

namespace CVManagementApplication.Business.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository,
                                IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<IList<CandidateModel>> GetAll()
        {
            var candidates = new List<CandidateModel>();
            var candidateEntities = await _candidateRepository.GetAll();
            _mapper.Map(candidateEntities, candidates);
            return candidates;
        }


        public async Task<CandidateModel> Create(CandidateCreateModel candidate)
        {
            var candidateEntity = new Candidate();
            _mapper.Map(candidate, candidateEntity);

            var result = await _candidateRepository.Create(candidateEntity);

            var createdCandidate = new CandidateModel();
            _mapper.Map(result, createdCandidate);

            return createdCandidate;

        }

        public async Task<CandidateModel> Edit(int Id, CandidateModel model)
        {
            var candidateEntity = new Candidate
            {
                Id = Id
            };

            _mapper.Map(model, candidateEntity);

            var result = await _candidateRepository.Edit(candidateEntity);
            if (result == null)
                return null;

            return model;
        }

        public async Task<bool> Delete(int candidateId)
        {
            return await _candidateRepository.Delete(candidateId);
        }
    }
}
