using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase 
    {
        private readonly IDataRepository<ApplicantResumePoco> _repo;
        private readonly ApplicantResumeLogic _logicLayer;

        public ApplicantResumeController()
        {
            _repo = new EFGenericRepository<ApplicantResumePoco>();
            _logicLayer = new ApplicantResumeLogic(_repo);
        }

        
        [HttpGet]
        [Route("resume/{id}")]
        [ProducesResponseType(200, Type = typeof(ApplicantResumePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantResume(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

       
        [HttpGet]
        [Route("resume")]
        [ProducesResponseType(200, Type = typeof(List<ApplicantResumePoco>))]
        public ActionResult<List<ApplicantResumePoco>> GetAllApplicantResumes()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("resume")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantResume([FromBody] ApplicantResumePoco[] pocos)
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
        [Route("resume")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantResume([FromBody] ApplicantResumePoco[] pocos)
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
        [Route("resume")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteApplicantResume([FromBody] ApplicantResumePoco[] pocos)
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
