using Microsoft.EntityFrameworkCore;
namespace VTT.models{
    //VTT.models.MyBlogContent
    public class MyBlogContent : DbContext
    {

        public MyBlogContent(DbContextOptions<MyBlogContent> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Article> articles {get; set;}
    }
}