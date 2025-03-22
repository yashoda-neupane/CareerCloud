using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly IDataRepository<ApplicantProfilePoco> _repo;
        private readonly ApplicantProfileLogic _logicLayer;
        public ApplicantProfileController()
        {
            _repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logicLayer = new ApplicantProfileLogic(_repo);
        }

        
        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(200, Type = typeof(ApplicantProfilePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantProfile(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(200, Type = typeof(List<ApplicantProfilePoco>))]
        public ActionResult<List<ApplicantProfilePoco>> GetAllApplicantProfiles()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
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

        
        [HttpPut]
        [Route("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            try
            {
                _logicLayer.Update(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpDelete]
        [Route("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteApplicantProfile([FromBody] ApplicantProfilePoco[] pocos)
        {
            try
            {
                _logicLayer.Delete(pocos);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
