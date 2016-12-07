using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechHome.Blog.Models.BlogViewModel
{
    public class ListVM
    {
        public ICollection<Post> Posts { get; set; }
    }
}
