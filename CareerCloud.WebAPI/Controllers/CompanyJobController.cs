using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly IDataRepository<CompanyJobPoco> _repo;
        private readonly CompanyJobLogic _logicLayer;
        public CompanyJobController()
        {
            _repo = new EFGenericRepository<CompanyJobPoco>();
            _logicLayer = new CompanyJobLogic(_repo);
        }

        
        [HttpGet]
        [Route("job/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyJobPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyJob(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("job")]
        [ProducesResponseType(200, Type = typeof(List<CompanyJobPoco>))]
        public ActionResult<List<CompanyJobPoco>> GetAllCompanyJobs()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
        [Route("job")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] pocos)
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
