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
        [Required]
        public string Name { get; set; }
        [Display(Name = "Status Narzędzia")]
        public int ToolStatusId { get; set; }
        [Display(Name = "Nazwa Artykułu")]
        public int ArticleId { get; set; }
        [Display(Name = "Numer Seryjny WA")]
        public string SerialNumber { get; set; }
        public virtual ToolStatus ToolStatus { get; set; }
        public virtual Article Articles { get; set; }
    }
}