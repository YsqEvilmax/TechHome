using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TechHome.Blog.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(AllowEmptyStrings =false, ErrorMessage ="The post tile cannot be empty")]
        [MaxLength(128, ErrorMessage ="The post title cannot be longer than 128!")]
        public string Title { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "The post description cannot be empty")]
        [MaxLength(256, ErrorMessage = "The post title cannot be longer than 256!")]
        public string Description { get; set; }
        [Required]
        public DateTime LastModified { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}
