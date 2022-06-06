using Microsoft.EntityFrameworkCore;

namespace ItemApi.Models
{
    public class ItemApiContext : DbContext
    {
        public ItemApiContext()
        {
        }
        public ItemApiContext(DbContextOptions<ItemApiContext> options) : base(options)
        {
            Options = options;
        }
        public DbContextOptions<ItemApiContext> Options;
        public DbSet<Item> Items {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=postgres;Integrated Security=true; Pooling=true;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(k => k.itemcode);
                entity.ToTable("erpitem");
            });
        }
    }
}