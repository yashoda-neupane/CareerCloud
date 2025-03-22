using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly IDataRepository<CompanyProfilePoco> _repo;
        private readonly CompanyProfileLogic _logicLayer;
        public CompanyProfileController()
        {
             _repo = new EFGenericRepository<CompanyProfilePoco>();
            _logicLayer = new CompanyProfileLogic(_repo);
        }

        
        [HttpGet]
        [Route("profile/{id}")]
        [ProducesResponseType(200, Type = typeof(CompanyProfilePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetCompanyProfile(Guid id)
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
        [ProducesResponseType(200, Type = typeof(List<CompanyProfilePoco>))]
        public ActionResult<List<CompanyProfilePoco>> GetAllCompanyProfiles()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

       
        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] pocos)
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
