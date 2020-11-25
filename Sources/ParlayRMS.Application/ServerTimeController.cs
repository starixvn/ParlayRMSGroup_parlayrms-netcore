using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ParlayRMS.Application
{
    [AllowAnonymous]
    [ApiController]
    public class ServerTimeController : ControllerBase
    {
        [Route("api/application/today"), HttpPost]
        public ActionResult Today() => Ok(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
    }
}
