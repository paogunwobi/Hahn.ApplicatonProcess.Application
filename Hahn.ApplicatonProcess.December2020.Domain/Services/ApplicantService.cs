using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hahn.ApplicatonProcess.December2020.Domain
{
    public class ApplicantService
    {
        private readonly HttpClient _httpClient;
        public ApplicantService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> verifyCountryOfOrigin(String countryOfOrigin)
        {
            _httpClient.BaseAddress = new Uri("https://restcountries.eu/rest/v2/name/");
            var responseTask = await _httpClient.GetAsync(countryOfOrigin);
            var content = await responseTask.Content.ReadAsStringAsync();
            
            if (!responseTask.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return true;
        }
    }
}
