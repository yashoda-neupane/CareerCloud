using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly IDataRepository<CompanyJobEducationPoco> _repo;
        private readonly CompanyJobEducationLogic _logicLayer;
        public CompanyJobEducationController()
        {
            _repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logicLayer = new CompanyJobEducationLogic(_repo);
        }

       
        [HttpGet]
        [Route("jobeducation/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyJobEducationPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJobEducation(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("jobeducation")]
        [ProducesResponseType(200, Type = typeof(List<CompanyJobEducationPoco>))]
        public ActionResult<List<CompanyJobEducationPoco>> GetAllCompanyJobEducations()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }


        [HttpPost]
        [Route("jobeducation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
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
        [Route("jobeducation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
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
        [Route("jobeducation")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyJobEducation([FromBody] CompanyJobEducationPoco[] pocos)
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
