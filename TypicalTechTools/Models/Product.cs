using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TypicalTechTools.Models
{
    public class Product
    {
        //database ID for product
        public int Id { get; set; }

        //Product code (barcode or similar)
        [Required]
        public int Code { get; set; }

        //Name of product listing
        [Required]
        [StringLength(40, ErrorMessage ="Product Name must be less than 40 characters.")]
        [RegularExpression("^[a-zA-Z0-9_ ']*$", ErrorMessage =
            "Product Name can only contain letters, numbers, underscores, spaces, and single quotes")]
        public string Name { get; set; } = string.Empty;

        //Price of product
        [Required]
        [Range(0,2000000, ErrorMessage = "Price must be a positive number")]
        public double Price { get; set; }

        //Description of product
        [Required]
        [StringLength(1000, ErrorMessage = "Description must be less than 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        //date/time product was updated on database
        public DateTime? UpdatedDate { get; set; }

        //session id holder
        public string? SessionId { get; set; }

        //link to comments in database
        public virtual ICollection<Comment>? Comments { get; set;}
        
        
        
    }
}
