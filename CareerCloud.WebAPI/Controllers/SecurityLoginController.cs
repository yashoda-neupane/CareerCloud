using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly IDataRepository<SecurityLoginPoco> _repo;
        private readonly SecurityLoginLogic _logicLayer;
        public SecurityLoginController()
        {
            _repo  = new EFGenericRepository<SecurityLoginPoco>();
            _logicLayer = new SecurityLoginLogic(_repo);
        }

       
        [HttpGet]
        [Route("login/{id}")]
        [ProducesResponseType(200, Type = typeof(SecurityLoginPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLogin(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("login")]
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginPoco>))]
        public ActionResult<List<SecurityLoginPoco>> GetAllSecurityLogins()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
        [Route("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] pocos)
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
