using System;
using Microsoft.AspNetCore.Mvc;

namespace ParlayRMS.Application
{
    [ApiController]
    public class ServerTimeController : ControllerBase
    {
        [Route("api/application/today"), HttpPost]
        public ActionResult Today() => Ok(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day));
    }
}
