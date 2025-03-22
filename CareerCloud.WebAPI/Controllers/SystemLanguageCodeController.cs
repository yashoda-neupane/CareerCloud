using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly IDataRepository<SystemLanguageCodePoco> _repo;
        private readonly SystemLanguageCodeLogic _logicLayer;
        public SystemLanguageCodeController()
        {
             _repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logicLayer = new SystemLanguageCodeLogic(_repo);
        }


        [HttpGet]
        [Route("language/{languageID}")]
        [ProducesResponseType(200, Type = typeof(SystemLanguageCodePoco))]
        [ProducesResponseType(404)]
        public ActionResult  GetSystemLanguageCode(string languageID)
        {
            var poco = _logicLayer.Get(languageID);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("language")]
        [ProducesResponseType(200, Type = typeof(List<SystemLanguageCodePoco>))]
        public ActionResult<List<SystemLanguageCodePoco>> GetAllSystemLanguageCodes()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        [HttpPost]
        [Route("language")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
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
        [Route("language")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
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
        [Route("language")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] pocos)
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
