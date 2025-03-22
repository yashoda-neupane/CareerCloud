using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly IDataRepository<SecurityLoginsRolePoco> _repo;
        private readonly SecurityLoginsRoleLogic _logicLayer;

        public SecurityLoginsRoleController()
        {
             _repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logicLayer = new SecurityLoginsRoleLogic(_repo);
        }

        
        [HttpGet]
        [Route("loginsrole/{id}")]
        [ProducesResponseType(200, Type = typeof(SecurityLoginsRolePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

       
        [HttpGet]
        [Route("loginsrole")]
        [ProducesResponseType(200, Type = typeof(List<SecurityLoginsRolePoco>))]
        public ActionResult<List<SecurityLoginsRolePoco>> GetAllSecurityLoginsRoles()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("loginsrole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
        [Route("loginsrole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityLoginsRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
        [Route("loginsrole")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] pocos)
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
