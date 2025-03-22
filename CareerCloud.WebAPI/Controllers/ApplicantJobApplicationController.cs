using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{

    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly IDataRepository<ApplicantJobApplicationPoco> _repo;
        private readonly ApplicantJobApplicationLogic _logicLayer;

        public ApplicantJobApplicationController()
        {
             _repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logicLayer = new ApplicantJobApplicationLogic(_repo);
        }

        // Get all Applicant Job applications records
        [HttpGet]
        [Route("job")]
        [ProducesResponseType(typeof(List<ApplicantJobApplicationPoco>), 200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<ApplicantJobApplicationPoco>> GetAllApplicantEducations()
        {
            var result = _logicLayer.GetAll();
            if (result == null || !result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

        // Get single item
        [HttpGet]
        [Route("job/{id}")]
        [ProducesResponseType(typeof(ApplicantJobApplicationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            var applicantJobApplicationPoco = _logicLayer.Get(id);
            if (applicantJobApplicationPoco == null)
            {
                return NotFound();
            }
            return Ok(applicantJobApplicationPoco);
        }

        // Create a new Applicant Job application record
        [HttpPost]
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            try
            {
                _logicLayer.Add(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update an existing Applicant Job Application record 
        [HttpPut]
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            try
            {
                _logicLayer.Update(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }

        }

        // Delete an Applicant Job application record
        [HttpDelete]
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteApplicantJobApplication([FromBody] ApplicantJobApplicationPoco[] pocos)
        {
            try
            {
                _logicLayer.Delete(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

