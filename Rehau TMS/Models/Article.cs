using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa Artykułu")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Numer Seryjny Artykułu")]
        [Required]
        public string ArticleSerialNumber { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}