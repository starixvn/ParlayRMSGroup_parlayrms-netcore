using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ParlayRMS.Application
{
    [AllowAnonymous]
    [ApiController]
    public class ServerTimeController : ControllerBase
    {
        [Route("api/application/today"), HttpPost, HttpGet]
        public ActionResult Today() => Ok(DateTime.Today);

        [Route("api/application/now"), HttpPost, HttpGet]
        public ActionResult Now() => Ok(DateTime.Now);

    }
}
