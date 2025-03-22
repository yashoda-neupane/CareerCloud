using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SystemCountryCodeController :ControllerBase
    {
        private readonly IDataRepository<SystemCountryCodePoco> _repo;
        private readonly SystemCountryCodeLogic _logicLayer;
        public SystemCountryCodeController()
        {
            _repo = new EFGenericRepository<SystemCountryCodePoco>();
           _logicLayer = new SystemCountryCodeLogic(_repo);
        }

       
        [HttpGet]
        [Route("country/{code}")]
        [ProducesResponseType(200, Type = typeof(SystemCountryCodePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetSystemCountryCode(string code)
        {
            var poco = _logicLayer.Get(code);
            if (poco == null)
            {
                return NotFound();
            }
            
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("country")]
        [ProducesResponseType(200, Type = typeof(List<SystemCountryCodePoco>))]
        public ActionResult<List<SystemCountryCodePoco>> GetAllSystemCountryCodes()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("country")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            try
            {
                _logicLayer.Add(pocos);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpPut]
        [Route("country")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            try
            {
                _logicLayer.Update(pocos);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
        [HttpDelete]
        [Route("country")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSystemCountryCode([FromBody] SystemCountryCodePoco[] pocos)
        {
            try
            {
                _logicLayer.Delete(pocos);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
