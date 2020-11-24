using System.Collections.Generic;

namespace ParlayRMS.Application.Models
{
    public class Header
    {
        public string Username { get; set; }

        public List<Menu> Menus { get; set; }
    }
}