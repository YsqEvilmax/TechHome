using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TechHome.Blog.Data;
using Microsoft.EntityFrameworkCore;
using TechHome.Blog.Models;

namespace TechHome.Blog
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BlogDbConnection")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //Initialize Db
            InitializeDb(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeDb(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
                context.Database.Migrate();

                if (env.IsDevelopment())
                {
                    foreach(var post in context.Posts) { context.Posts.Remove(post); }
                    //if(context.Posts.Count() == 0)
                    {
                        Tag tag1 = new Tag() { Name = "tag1" };
                        Tag tag2 = new Tag() { Name = "tag2" };
                        Tag tag3 = new Tag() { Name = "tag3" };
                        context.Tags.Add(tag1);
                        context.Tags.Add(tag2);
                        context.Tags.Add(tag3);

                        Category category1 = new Category() { Name = "category1" };
                        Category category2 = new Category() { Name = "category2" };
                        context.Catergories.Add(category1);
                        context.Catergories.Add(category2);

                        Post post1 = new Post() { Title = "he", Description = "ha", CategoryId = category1.Id};
                        Post post2 = new Post() { Title = "hehe", Description = "haha", CategoryId = category1.Id };
                        Post post3 = new Post() { Title = "hehehe", Description = "hahaha", CategoryId = category1.Id };
                        Post post4 = new Post() { Title = "hehehehe", Description = "hahahaha", CategoryId = category2.Id };
                        Post post5 = new Post() { Title = "hehehehehe", Description = "hahahahaha", CategoryId = category2.Id };
                        Post post6 = new Post() { Title = "hehehehehehe", Description = "hahahahahaha", CategoryId = category2.Id };
    
                        context.Posts.Add(post1);
                        context.Posts.Add(post2);
                        context.Posts.Add(post3);
                        context.Posts.Add(post4);
                        context.Posts.Add(post5);
                        context.Posts.Add(post6);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
