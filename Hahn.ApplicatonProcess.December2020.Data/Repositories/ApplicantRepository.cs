using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hahn.ApplicatonProcess.December2020.Domain;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class ApplicantRepository
    {    
        public ApplicantRepository(ApplicationDbContext context)    
        {      
            DbContext = context;
        }
        
        private ApplicationDbContext DbContext { get; set; }
        
        public async Task<IEnumerable<Applicant>> GetApplicants()
        {
            return await DbContext.Applicants.ToListAsync();
        }

        public async Task<Applicant> GetApplicant(int id)
        {
            try{
                var applicant = await DbContext.Applicants.FindAsync(id);
                return applicant;
            }
            catch {
                throw new Exception();
            }
        }

        public async Task<Applicant> PutApplicant(int id, Applicant applicant)
        {
            try {
                DbContext.Entry(applicant).State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
                var updatedApplicant = await DbContext.Applicants.FindAsync(id);
                return updatedApplicant;
            }
            catch {
                throw new Exception();
            }
        }

        public async Task<Applicant> PostApplicant(Applicant applicant)
        {
            
            try {
                DbContext.Applicants.Add(applicant);
                await DbContext.SaveChangesAsync();
                return applicant;
            }
            catch {
                throw new Exception();
            }
        }

        public async Task<Applicant> DeleteApplicant(int id)
        {
            try {
                var applicant = await DbContext.Applicants.FindAsync(id);
                DbContext.Applicants.Remove(applicant);
                await DbContext.SaveChangesAsync();
                return applicant;
            }
            catch {
                throw new Exception();
            }
        }

        public bool ApplicantExists(int id)
        {
            return DbContext.Applicants.Any(e => e.ID == id);
        }
    }
}
