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
                        // ID = 1,
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
                        // ID = 2,
                        Name = "Ayodeji",
                        FamilyName = "Ajansare",
                        Address = "6 Paul Odulaja Crescent, Ifako-Gbagada",
                        CountryOfOrigin = "Singapore",
                        EmailAddress = "ayodeji2@gmail.com",
                        Age = 57,
                        Hired = true
                    },

                    new Applicant
                    {
                        // ID = 3,
                        Name = "Lookman",
                        FamilyName = "Penalty",
                        Address = "6 Paul Odulaja Crescent, Ifako-Gbagada",
                        CountryOfOrigin = "Germany",
                        EmailAddress = "lookman22@gmail.com",
                        Age = 21,
                        Hired = false
                    },

                    new Applicant
                    {
                        // ID = 4,
                        Name = "Olusola",
                        FamilyName = "Adeniyi",
                        Address = "6 Paul Odulaja Crescent, Ifako-Gbagada, Lagos",
                        CountryOfOrigin = "Nigeria",
                        EmailAddress = "oradeniyi@gmail.com",
                        Age = 24,
                        Hired = true
                    },

                    new Applicant
                    {
                        // ID = 5,
                        Name = "Adejare",
                        FamilyName = "Akinfenwa",
                        Address = "1a Redemption Crescent, Gbagada, Lagos",
                        CountryOfOrigin = "Nigeria",
                        EmailAddress = "adejare2@gmail.com",
                        Age = 24,
                        Hired = true
                    },

                    new Applicant
                    {
                        // ID = 6,
                        Name = "Hahn",
                        FamilyName = "dotnetcore",
                        Address = "1a dotnetcore 5.0 web api,",
                        CountryOfOrigin = "Britain",
                        EmailAddress = "hahndotnet2021@gmail.com",
                        Age = 34,
                        Hired = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}