using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Domain;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ApplicantController : ControllerBase  
    {   
        private readonly ILogger<ApplicantController> _logger;  
        private readonly ApplicantViewModel _viewModel;
        private ApplicantService _verifyOrigin;
        
        public ApplicantController( ApplicantViewModel vm, ApplicantService verifyOrigin, ILogger<ApplicantController> logger)    
        {            
            _viewModel = vm;
            _verifyOrigin = verifyOrigin;
            _logger = logger;
        }

        // GET: api/Applicant
        [HttpGet]
        public async Task<IEnumerable<Applicant>> GetApplicants()
        {
            return await _viewModel.GetAll();
        }

        // GET: api/Applicant/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
            try
            {
                var applicant = await _viewModel.GetApplicantByID(id);
                if (applicant == null)
                {
                    return NotFound();
                }
                return applicant;
            }
            catch
            {
                if (!_viewModel.ApplicantExists(id))
                {
                    _logger.LogInformation($"Applicant with ID: {id} does not Exist::: @ {DateTimeOffset.Now}");
                    return NotFound("The ID is Invalid");
                }
                else
                {
                    throw;
                }
            }
        }

        // PUT: api/Applicant/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Applicant>> PutApplicant(int id, Applicant applicant)
        {
            if (id != applicant.ID)
            {
                return BadRequest("Data not Matching");
            }

            try
            {
                _logger.LogInformation($"Verifying {applicant.CountryOfOrigin} is a valid CountryOfOrigin::: @ {DateTimeOffset.Now}");
                await _verifyOrigin.verifyCountryOfOrigin(applicant.CountryOfOrigin);
            }
            catch
            {
                _logger.LogError($"Invalid CountryOfOrigin: Applicant with ID: {id} tried to use \"{applicant.CountryOfOrigin}\" as CountryOfOrigin::: @ {DateTimeOffset.Now}");
                return BadRequest("Invalid CountryOfOrigin");
            }

            try
            {
                _logger.LogInformation($"Updating Applicant with ID: {id}::: @ {DateTimeOffset.Now}");
                await _viewModel.PutApplicant(id, applicant);
                _logger.LogInformation($"Applicant Update Successful::: @ {DateTimeOffset.Now}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_viewModel.ApplicantExists(id))
                {
                    _logger.LogError($"Applicant with ID: {id} does not Exist::: @ {DateTimeOffset.Now}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await GetApplicant(id);

        }

        // POST: api/Applicant
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            try
            {
                _logger.LogInformation($"Verifying {applicant.CountryOfOrigin} is a valid CountryOfOrigin::: @ {DateTimeOffset.Now}");
                await _verifyOrigin.verifyCountryOfOrigin(applicant.CountryOfOrigin);
            }
            catch
            {
                _logger.LogError($"Invalid CountryOfOrigin: Applicant with ID: {applicant.ID} tried to use \"{applicant.CountryOfOrigin}\" as CountryOfOrigin::: @ {DateTimeOffset.Now}");
                return BadRequest("Invalid CountryOfOrigin");
            }
            await _viewModel.PostApplicant(applicant);
            _logger.LogInformation($"Created an Applicant with Details: {applicant}::: @ {DateTimeOffset.Now}");
            return CreatedAtAction(nameof(GetApplicant), new { id = applicant.ID }, applicant);
        }

        // DELETE: api/Applicant/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApplicant(int id)
        {
            var applicant = await _viewModel.GetApplicantByID(id);
            if (applicant == null)
            {
                return NotFound();
            }

            await _viewModel.DeleteApplicant(id);
            _logger.LogInformation($"Deleted Applicant with ID: {id}::: @ {DateTimeOffset.Now}");
            return NoContent();
        }
    }  

}
