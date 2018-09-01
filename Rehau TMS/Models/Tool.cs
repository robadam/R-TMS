using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Models
{
    public class Tool
    {
        public int ToolId { get; set; }
        [Display(Name = "Nazwa Narzędzia")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Status Narzędzia")]
        [Required]
        public int ToolsStatusId { get; set; }
        [Display(Name = "Nazwa Artykułu")]
        public int ArticlesId { get; set; }
        [Required]
        [Display(Name = "Numer Seryjny Narzędzia")]
        public string SerialNumber { get; set; }
    }
}