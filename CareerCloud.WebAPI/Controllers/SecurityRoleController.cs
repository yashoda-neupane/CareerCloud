using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly IDataRepository<SecurityRolePoco> _repo;
        private readonly SecurityRoleLogic _logicLayer;

        public SecurityRoleController()
        {
             _repo = new EFGenericRepository<SecurityRolePoco>();
            _logicLayer = new SecurityRoleLogic(_repo);
        }

        
        [HttpGet]
        [Route("role/{id}")]
        [ProducesResponseType(200, Type = typeof(SecurityRolePoco))]
        [ProducesResponseType(404)]
        public ActionResult GetSecurityRole(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("role")]
        [ProducesResponseType(200, Type = typeof(List<SecurityRolePoco>))]
        public ActionResult<List<SecurityRolePoco>> GetAllSecurityRoles()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }


        [HttpPost]
        [Route("role")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] pocos)
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
        [Route("role")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] pocos)
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
        [Route("role")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] pocos)
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
