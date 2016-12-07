using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechHome.Blog.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The category name cannot be empty")]
        [MaxLength(64, ErrorMessage = "The category name cannot be longer than 64!")]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
