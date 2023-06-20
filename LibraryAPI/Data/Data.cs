using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet <Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasIndex(u => u.Id).IsUnique();
            modelBuilder.Entity<Book>().HasCheckConstraint("Date", "Date< GETDATE()");
            modelBuilder.Entity<Book>().HasCheckConstraint("Author", "len(Author)>0");
            modelBuilder.Entity<Book>().HasCheckConstraint("Id", "len(Id)>0");

        }
    }
}
