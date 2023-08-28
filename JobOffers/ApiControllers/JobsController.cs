using Microsoft.AspNetCore.Mvc;
using Bl;
using Domains;
using JobOffers.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobOffers.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        IJobs _oClsJobs;
        public JobsController(IJobs oClsJobs)
        {
            _oClsJobs = oClsJobs;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public ApiResponse Get()
        {
            try
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = _oClsJobs.GetAllData().Take(4);
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = "200";

                return oApiResponse;
            }
            catch (Exception ex)
            {
                ApiResponse oApiResponse = new ApiResponse();
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = "500";

                return oApiResponse;
            }
        }

        //// GET api/<JobsController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<JobsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<JobsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<JobsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
