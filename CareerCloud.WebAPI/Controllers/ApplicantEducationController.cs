using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantEducationController : ControllerBase

    {
        private readonly IDataRepository<ApplicantEducationPoco> _repo;
        private readonly ApplicantEducationLogic _logicLayer;

        public ApplicantEducationController()
        {
            _repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logicLayer = new ApplicantEducationLogic(_repo);
        }

        // Get All Applicant Education records
        [HttpGet]
        [Route("education")]
        [ProducesResponseType(typeof(List<ApplicantEducationPoco>), 200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<ApplicantEducationPoco>> GetAllApplicantEducations()
        {
            var result = _logicLayer.GetAll();
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        
        // Get single Applicant education record
        [HttpGet]
        [Route("education/{applicantEducationiId}")]
        [ProducesResponseType(typeof(ApplicantEducationPoco), 200)]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantEducation(Guid applicantEducationiId)
        {
            var applicantEducationPoco = _logicLayer.Get(applicantEducationiId);
            if (applicantEducationPoco == null)
            {
                return NotFound();
            }
            return Ok(applicantEducationPoco);
        }

        
        // Create a new Applicant Education record
        [HttpPost]
        [Route("education")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            try
            {
                _logicLayer.Add(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest("No education records provided.");
            }
        }

        
        // Update a record
        [HttpPut]
        [Route("education")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
        {
            try
            {
                _logicLayer.Update(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return NotFound("No education records provided.");
            }
        }

        // Delete a record
        
        [HttpDelete]
        [Route("education")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public ActionResult DeleteApplicantEducation([FromBody] ApplicantEducationPoco[] pocos)
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
