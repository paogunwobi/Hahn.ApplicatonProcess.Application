using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.December2020.Domain;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public partial class ApplicationDbContext : DbContext  
    {    
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Applicant> Applicants { get; set; }  
    }
}
