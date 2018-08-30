using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Models
{
    public class Tool
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa Narzędzia")]
        public string Name { get; set; }
        [Display(Name = "Status Narzędzia")]
        public int ToolsStatusId { get; set; }
        [Display(Name = "Nazwa Artykułu")]
        public int ArticlesId { get; set; }
        [Display(Name = "Numer Seryjny Narzędzia")]
        public string SerialNumber { get; set; }
    }
}