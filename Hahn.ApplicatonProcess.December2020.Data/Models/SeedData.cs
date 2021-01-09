using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hahn.ApplicatonProcess.December2020.Domain;
using System;
using System.Linq;

namespace Hahn.ApplicatonProcess.December2020.Data.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Applicants.
                if (context.Applicants.Any())
                {
                    return;   // DB has been seeded
                }

                context.Applicants.AddRange(
                    new Applicant
                    {
                        // ID = 0,
                        Name = "Philip",
                        FamilyName = "Ogunwobi",
                        Address = "6 Paul Odulaja Crescent, Ifako-Gbagada, Lagos",
                        CountryOfOrigin = "Nigeria",
                        EmailAddress = "paogunwobi@gmail.com",
                        Age = 27,
                        Hired = true
                    },

                    new Applicant
                    {
                        // ID = 1,
                        Name = "Olusola",
                        FamilyName = "Adeniyi",
                        Address = "6 Paul Odulaja Crescent, Ifako-Gbagada, Lagos",
                        CountryOfOrigin = "Nigeria",
                        EmailAddress = "oradeniyi@gmail.com",
                        Age = 24,
                        Hired = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}