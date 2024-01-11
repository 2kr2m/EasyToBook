using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyToBook.Domain.Entities
{
    public class Villa
    {
        public int Id { get; set; }
        [MaxLength(50),MinLength(5, ErrorMessage = "Name must be 5 characters at least")]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Price per Night")]
        [Range(10,1000)]
        public double Price { get; set; }
        [Display(Name ="Surface")]
        public int Sqft { get; set;}
        [Range(1,10)]
        public int Occupancy { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get;set; }

    }
}
