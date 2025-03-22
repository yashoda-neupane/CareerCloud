using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyJobsDescriptionController : ControllerBase
    {
            private readonly IDataRepository<CompanyJobDescriptionPoco> _repo;
            private readonly CompanyJobDescriptionLogic _logicLayer;

            public CompanyJobsDescriptionController()
            {
                _repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
                _logicLayer = new CompanyJobDescriptionLogic(_repo);
            }
            
        
            
            [HttpGet]
            [Route("jobdescription/{id}")]
            [ProducesResponseType(200, Type = typeof(CompanyJobDescriptionPoco))]
            [ProducesResponseType(404)]
            public ActionResult GetCompanyJobsDescription(Guid id)
            {
                var poco = _logicLayer.Get(id);
                if (poco == null)
                {
                    return NotFound();
                }
                return Ok(poco);
            }

            
            [HttpGet]
            [Route("jobdescription")]
            [ProducesResponseType(200, Type = typeof(List<CompanyJobDescriptionPoco>))]
            public ActionResult<List<CompanyJobDescriptionPoco>> GetAllCompanyJobDescriptions()
            {
                var pocos = _logicLayer.GetAll();
                return Ok(pocos);
            }

           
            [HttpPost]
            [Route("jobdescription")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
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
            [Route("jobdescription")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public ActionResult PutCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
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
            [Route("jobdescription")]
            [ProducesResponseType(200)]
            [ProducesResponseType(400)]
            public ActionResult DeleteCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] pocos)
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
