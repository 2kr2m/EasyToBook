using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EasyToBook.Domain.Entities
{
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name="Room Number")]
        public int Villa_Number { get; set; }

        [ForeignKey("Villa")]
        [Display(Name = "Villa ID")]
        public int VillaId { get; set; }
        [ValidateNever]
        public Villa villa { get; set; }
        [Display(Name ="Details")]
        public string? SpecialDetails { get; set; }
    }
}
