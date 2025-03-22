using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly IDataRepository<CompanyDescriptionPoco> _repo;
        private readonly CompanyDescriptionLogic _logicLayer;
        public CompanyDescriptionController()
        {
            _repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logicLayer = new CompanyDescriptionLogic(_repo);
        }

        
        [HttpGet]
        [Route("description/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyDescriptionPoco))]
        [ProducesResponseType(404)]
        public ActionResult  GetCompanyDescription(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("description")]
        [ProducesResponseType(200, Type = typeof(List<CompanyDescriptionPoco>))]
        public ActionResult<List<CompanyDescriptionPoco>> GetAllCompanyDescriptions()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }


        [HttpPost]
        [Route("description")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
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
        [Route("description")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
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
        [Route("description")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] pocos)
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
