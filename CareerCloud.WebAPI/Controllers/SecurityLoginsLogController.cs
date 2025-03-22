using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly IDataRepository<SecurityLoginsLogPoco> _repo;
        private readonly SecurityLoginsLogLogic _logicLayer;
        public SecurityLoginsLogController()
        {
            _repo = new EFGenericRepository<SecurityLoginsLogPoco>();
           _logicLayer = new SecurityLoginsLogLogic(_repo);
        }

       
        [HttpGet]
        [Route("loginsLog/{id}")]
        [ProducesResponseType(200, Type = typeof(SecurityLoginsLogPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("loginsLog")]
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginsLogPoco>))]
        public ActionResult<List<SecurityLoginsLogPoco>> GetAllSecurityLoginsLog()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("loginsLog")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
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
        [Route("loginsLog")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
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
        [Route("loginsLog")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] pocos)
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
