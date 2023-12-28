using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace VTT.models{
    //VTT.models.MyBlogContent
    public class MyBlogContent : IdentityDbContext<AppUser>
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
            foreach(var entitype in modelBuilder.Model.GetEntityTypes()){
                var tableName = entitype.GetTableName();
                if(tableName.StartsWith("AspNet")){
                    entitype.SetTableName(tableName.Substring(6)); // bo di sau ky tu dau
                }
            }
        }
        public DbSet<Article> articles {get; set;}
    }
}