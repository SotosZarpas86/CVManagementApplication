using CVManagementApplication.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CVManagementApplication.Infrastructure.Context
{
    public class CVManagementContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public CVManagementContext(DbContextOptions<CVManagementContext> options) : base(options)
        {

        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Degree> Degrees { get; set; }

    }
}
