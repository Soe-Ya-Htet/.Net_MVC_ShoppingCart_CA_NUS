using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class ExamModel
    {
        [Display(Name ="Exam Id : ")]
        [Required(ErrorMessage = "Exam id is required.")]
        [Range(1, 100, ErrorMessage = "Exam id must be between 1 and 100")]
        public int ExamId { get; set; }

        [Display(Name = "Exam Name : ")]
        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string ExamName { get; set; }

        [Display(Name = "Student Email : ")]
        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set; }

        [Display(Name = "Exam Date : ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime ExamDate { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal Price { get; set; }

    }
}