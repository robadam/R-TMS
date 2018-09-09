using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rehau_TMS.Models
{
    public class Schedule
    {
        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Użytkownik")]
        public string ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "Rodzaj pracy")]
        public int WorkTypeId { get; set; }

        [Display(Name = "Narzędzie")]
        public int ToolId { get; set; }

        [Display(Name = "Artykuł")]
        public int ArticleId { get; set; }

        [Display(Name = "Opcja")]
        public int OptionsId { get; set; }

        [Display(Name = "Dodatkowa opcja")]
        public int OptionsAdditionalId { get; set; }

        [Required]
        [Display(Name = "Ilość godzin")]
        [Range(1, 20,
        ErrorMessage = "Wartość musi być pomiędzy {1} - {2}.")]
        public int ReportedHours { get; set; }

        [Display(Name = "Uwagi")]
        [StringLength(200, ErrorMessage = "Długość nie może być dłuższa niżeli {1} znaków")]
        public string Info { get; set; }

        //Lazy loading
        public virtual Article Article { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Options Options { get; set; }
        public virtual OptionsAdditional OptionsAdditional { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual WorkType WorkType { get; set; }


    }
}