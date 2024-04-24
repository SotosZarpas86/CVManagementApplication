using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;
using CVManagementApplication.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CVManagementApplication.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CVManagementContext _context;

        public CandidateRepository(CVManagementContext context)
        {
            _context = context;
        }

        public async Task<IList<Candidate>> GetAll()
        {
            return await _context.Candidates.ToListAsync();
        }


        public async Task<Candidate> Create(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> Edit(Candidate candidate)
        {
            var candidateForUpdate = await _context.Candidates.FirstOrDefaultAsync(n => n.Id == candidate.Id);
            if (candidateForUpdate != null)
            {
                candidateForUpdate.CVblob = candidate.CVblob;
                candidateForUpdate.Email = candidate.Email;
                candidateForUpdate.CreationTime = candidate.CreationTime;
                candidateForUpdate.LastName = candidate.LastName;
                candidateForUpdate.FirstName = candidate.FirstName;
                candidateForUpdate.DegreeID = candidate.DegreeID;
                candidateForUpdate.Mobile = candidate.Mobile;
                candidateForUpdate.CreationTime = DateTime.Now;
                _context.Candidates.Update(candidateForUpdate);
                await _context.SaveChangesAsync();
                return candidate;
            }
            return null;
        }

        public async Task<bool> Delete(int candidateId)
        {
            var candidate = await _context.Candidates.FirstOrDefaultAsync(x => x.Id == candidateId);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
