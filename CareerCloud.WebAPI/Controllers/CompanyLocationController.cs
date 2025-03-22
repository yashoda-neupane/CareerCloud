using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{

    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly IDataRepository<CompanyLocationPoco> _repo;
        private readonly CompanyLocationLogic _logicLayer;
        public CompanyLocationController()
        {
           _repo = new EFGenericRepository<CompanyLocationPoco>();
           _logicLayer = new CompanyLocationLogic(_repo);
        }

       
        [HttpGet]
        [Route("location/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyLocationPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyLocation(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("location")]
        [ProducesResponseType(200, Type = typeof(List<CompanyLocationPoco>))]
        public ActionResult<List<CompanyLocationPoco>> GetAllCompanyLocations()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
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
        [Route("location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
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
        [Route("location")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] pocos)
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
