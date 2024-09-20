using System.ComponentModel.DataAnnotations;

namespace TypicalTechTools.Models
{
    public class Comment
    {
        // database ID no.
        public int Id { get; set; }

        //Text or body of comment
        [Required]
        [Display(Name = "Comment")]
        public string Text { get; set; } = string.Empty;

        //Product code associated with comment
        [Required]
        [Display(Name = "Product Code")]
        public int Code { get; set; }

        //Link from comments to products in database
        public virtual Product? Products { get; set; }

        //session id holder
        public string? SessionId { get; set; }

        //Date/time comment was created (added to the database)
        [Display(Name = "Date Created")]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Return a CSV formatted string of the a comment object
        /// </summary>
        /// <returns></returns>
        public string ToCSVString()
        {
            return $"{Id},{Text},{Code}";
        }

    }
}
