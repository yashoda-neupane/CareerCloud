using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly IDataRepository<CompanyJobSkillPoco> _repo;
        private readonly CompanyJobSkillLogic _logicLayer;
        public CompanyJobSkillController()
        {
             _repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logicLayer = new CompanyJobSkillLogic(_repo);
        }

       
        [HttpGet]
        [Route("jobskill/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyJobSkillPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobSkill(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("jobskill")]
        [ProducesResponseType(200, Type = typeof(List<CompanyJobSkillPoco>))]
        public ActionResult<List<CompanyJobSkillPoco>> GetAllCompanyJobSkills()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

       
        [HttpPost]
        [Route("jobskill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
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
        [Route("jobskill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
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
        [Route("jobskill")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] pocos)
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
