using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.Application.Models;

namespace ParlayRMS.Application
{
    [ApiController]
    public class HeaderController : ControllerBase
    {
        [Route("api/application/header"), HttpPost]
        public ActionResult Index()
        {
            var header = new Header {
                Username = "Kaden",
                Menus = new List<Menu>(),
            };
            
            return Ok(header);
        }
    }
}
