using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebGentle.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the authors of your book")]
        public string Author { get; set; }
        [StringLength(500, MinimumLength = 5)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter the category of your book")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose the language of your book")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter total pages of your book")]
        [Display(Name = "Total Pages of book")]
        public int? TotalPages { get; set; }
        //[Required(ErrorMessage = "Please choose the languages of your book")]
        public List<string> MultiLanguage { get; set; }

    }
}
