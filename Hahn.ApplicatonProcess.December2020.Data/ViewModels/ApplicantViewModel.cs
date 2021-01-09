using System.Collections.Generic;
using System.Threading.Tasks;
using Hahn.ApplicatonProcess.December2020.Domain;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class ApplicantViewModel  
    {
        private ApplicantRepository _repository { get; set; }
        public ApplicantViewModel(ApplicantRepository repository)    
        {      
            _repository = repository; 
        }
        
        public Task<IEnumerable<Applicant>> GetAll()    
        {      
            return LoadApplicants();    
        }
        
        protected Task<IEnumerable<Applicant>> LoadApplicants()    
        { 
            return _repository.GetApplicants();
        }

        public Task<Applicant> GetApplicantByID(int id)    
        {      
            return LoadApplicantByID(id);    
        }

        protected Task<Applicant> LoadApplicantByID(int id)    
        { 
            return _repository.GetApplicant(id);
        }

        public Task<Applicant> PutApplicant(int id, Applicant applicant)    
        {      
            return UpdateApplicant(id, applicant);    
        }

        protected Task<Applicant> UpdateApplicant(int id, Applicant applicant)    
        { 
            return _repository.PutApplicant(id, applicant);
        }

        public Task<Applicant> PostApplicant(Applicant applicant)    
        {      
            return CreateApplicant(applicant);
        }

        protected Task<Applicant> CreateApplicant(Applicant applicant)    
        { 
            return _repository.PostApplicant(applicant);
        }

        public Task<Applicant> DeleteApplicant(int id)    
        {      
            return removeApplicant(id);
        }

        protected Task<Applicant> removeApplicant(int id)    
        { 
            return _repository.DeleteApplicant(id);
        }

        public bool ApplicantExists(int id)    
        {      
            return _repository.ApplicantExists(id);
        }

    }
}
