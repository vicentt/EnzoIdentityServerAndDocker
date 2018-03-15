using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnzoAPI.Controllers
{
 
    [Route("identity")]
    [Authorize]
    public class IndentityController : ControllerBase
    {
        
        [HttpGet]
        public ActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
