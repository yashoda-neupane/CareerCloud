using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly IDataRepository<ApplicantWorkHistoryPoco> _repo;
        private readonly ApplicantWorkHistoryLogic _logicLayer;
        public ApplicantWorkHistoryController()
        {
            _repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logicLayer = new ApplicantWorkHistoryLogic(_repo);
        }
                        
        
        [HttpGet]
        [Route("workhistory/{id}")]
        [ProducesResponseType(200, Type = typeof(ApplicantWorkHistoryPoco))]
        [ProducesResponseType(404)]
        public ActionResult GetApplicantWorkHistory(Guid id)
        {
            var poco = _logicLayer.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            return Ok(poco);
        }

        
        [HttpGet]
        [Route("workhistory")]
        [ProducesResponseType(200, Type = typeof(List<ApplicantWorkHistoryPoco>))]
        public ActionResult<List<ApplicantWorkHistoryPoco>> GetAllApplicantWorkHistories()
        {
            var pocos = _logicLayer.GetAll();
            return Ok(pocos);
        }

        
        [HttpPost]
        [Route("workhistory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PostApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
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
        [Route("workhistory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult PutApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
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
        [Route("workhistory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteApplicantWorkHistory([FromBody] ApplicantWorkHistoryPoco[] pocos)
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
