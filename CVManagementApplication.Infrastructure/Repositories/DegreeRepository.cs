using CVManagementApplication.Core.Entities;
using CVManagementApplication.Core.Interfaces;
using CVManagementApplication.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CVManagementApplication.Infrastructure.Repositories
{
    public class DegreeRepository : IDegreeRepository
    {
        private readonly CVManagementContext _context;

        public DegreeRepository(CVManagementContext context)
        {
            _context = context;
            SeedDummyData();
        }

        private void SeedDummyData()
        {
            var response = _context.Degrees.ToListAsync().Result;
            bool containsString = response.Any(item => item.Name == "Master" || item.Name == "PHD");
            if (!containsString)
            {
                var degreeList = new List<Degree>
            {
                new Degree
                {
                    CreationTime = DateTime.Now,
                    Name = "Master"

                 },
                new Degree
                {
                     CreationTime = DateTime.Now,
                    Name = "PHD"

                }
            };

                _context.Degrees.AddRangeAsync(degreeList);
                _context.SaveChangesAsync();
            }

        }

        public async Task<IList<Degree>> GetAll()
        {
            return await _context.Degrees.ToListAsync();
        }

        public async Task<Degree> GetDegreeByName(string name)
        {
            var result = await _context.Degrees.FirstOrDefaultAsync(c => c.Name == name);
            if (result == null)
            {
                return result;
            }
            return result;
        }

        public async Task<Degree> Create(Degree degree)
        {
            _context.Degrees.Add(degree);
            await _context.SaveChangesAsync();
            return degree;
        }

        public async Task<Degree> Update(Degree degree)
        {
            var degreeForUpdate = await _context.Degrees.FirstOrDefaultAsync(x => x.Id == degree.Id);
            if (degreeForUpdate != null)
            {
                degreeForUpdate.Name = degree.Name;
                _context.Degrees.Update(degreeForUpdate);
                await _context.SaveChangesAsync();
                return degree;
            }
            return null;
        }

        public async Task<bool> Delete(int degreeId)
        {
            var degree = await _context.Degrees.FirstOrDefaultAsync(x => x.Id == degreeId);
            if (degree != null)
            {
                var test = _context.Candidates.Any(v => v.DegreeID == degreeId);
                if (!test)
                {
                    _context.Degrees.Remove(degree);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
