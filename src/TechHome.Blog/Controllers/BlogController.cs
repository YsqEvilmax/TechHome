using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechHome.Blog.Data;
using Microsoft.Extensions.Logging;
using TechHome.Blog.Models.BlogViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TechHome.Blog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger _logger;
        private readonly BlogDbContext _db;
        public BlogController(BlogDbContext db, ILoggerFactory loggerFactory)
        {
            _db = db;
            _logger = loggerFactory.CreateLogger<BlogController>();
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Blog/List
        public IActionResult List()
        {
            ListVM vm = new ListVM();
            vm.Posts = _db.Posts.Include(u => u.Category)
                .Include(u => u.PostTags)
                .ToList();

            return View(vm);
        }

        // GET /Blog/Detial/5
        public IActionResult Detial(int id)
        {
            DetialVM vm = new DetialVM();
            vm.Post = _db.Posts.Include(u => u.Category)
                .Include(u => u.PostTags)
                .ToList().Find(x => x.Id == id);

            return View(vm);
        }
    }
}
