using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly IDataRepository<ApplicantSkillPoco> _repo;
        private readonly ApplicantSkillLogic _logicLayer;
        public ApplicantSkillController()
        {
            _repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logicLayer = new ApplicantSkillLogic(_repo);
        }

        
        [HttpGet]
        [Route("skill/{id}")]
        [ProducesResponseType(200, Type = typeof(ApplicantSkillPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantSkill(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("skill")]
        [ProducesResponseType(200, Type = typeof(List<ApplicantSkillPoco>))]
        public ActionResult<List<ApplicantSkillPoco>> GetAllApplicantSkills()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

       
        [HttpPost]
        [Route("skill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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
        [Route("skill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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
        [Route("skill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] pocos)
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
