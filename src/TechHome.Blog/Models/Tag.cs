using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechHome.Blog.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The tag name cannot be empty")]
        [MaxLength(32, ErrorMessage = "The tag name cannot be longer than 32!")]
        public string Name { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
