using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rehau_TMS.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        [Display(Name = "Nazwa Artykułu")]
        public string Name { get; set; }
        [Display(Name = "Numer Seryjny Artykułu")]
        public string ArticleSerialNumber { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}