using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PBL3_2.Models
{
    public class Post
    {
        public int ID { get; set; } // Added key property

        public DateTime? CreatedAt { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }
    }
}
