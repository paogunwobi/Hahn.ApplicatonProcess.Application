using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    [Table("Applicants")] 
    public class Applicant  
    {    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ID { get; set; }
        public string Name { get; set; }  
        public string FamilyName { get; set; }
        public string Address { get; set; }
        public string CountryOfOrigin { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool Hired { get; set; }
        public Applicant()
        {
            Hired = false;
        }   
    }

    public class ApplicantValidator : AbstractValidator<Applicant> {
	public ApplicantValidator() {
		RuleFor(x => x.ID).NotNull();
		RuleFor(x => x.Name).Length(5, 15).WithMessage("Name is required, with at least 5 characters");
		RuleFor(x => x.FamilyName).Length(5, 15).WithMessage("FamilyName is required, with at least 5 characters");
		RuleFor(x => x.Address).Length(10, 60).WithMessage("Address Length cannot be less than 10 characters");
		RuleFor(x => x.CountryOfOrigin).Length(3, 56).WithMessage("CountryOfOrigin must have least 3 characters");
		RuleFor(x => x.EmailAddress).EmailAddress().Matches(@"([\w-]+\.)+[\w-]{2,4}$").WithMessage("Invalid Email Address");
		RuleFor(x => x.Age).InclusiveBetween(20, 60).WithMessage("Age range is 20 and 60");
        // RuleFor(x => x.CountryOfOrigin).MustAsync(async(CountryOfOrigin, cancellation) => await verifyCountryOfOrigin(CountryOfOrigin)).WithMessage("Enter a valid CountryOfOrigin");
        }

        // private async Task<bool> verifyCountryOfOrigin(String countryOfOrigin)
        // {
        //     ApplicantService applicantService = new ApplicantService();
        //     return await applicantService.verifyCountryOfOrigin(countryOfOrigin);
        // }
    }
}
