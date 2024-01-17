#nullable disable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCoreSqlDb.Models
{
    /*
 Package Manager Console:
 * Add a new migration.
 Add-Migration -Name Initial -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb
 * Update migration to a database.
 Update-Database -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb
 * Update a database to MigrationName level.
 Update-Database -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb -Migration MigrationName
 * Clear a database.
 Update-Database -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb -Migration 0
 * Remove the newest migration.
 Remove-Migration -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb
 * Make database update SQL script. Contains all migrations.
 Script-Migration -From 0 -idempotent -Context MyDatabaseContext -Project DotNetCoreSqlDb -StartupProject DotNetCoreSqlDb
*/
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Todo>(new TodoDatabaseConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public DbSet<DotNetCoreSqlDb.Models.Todo> Todo { get; set; }
    }

    public class TodoDatabaseConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(b => b.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(2000)
                .IsRequired();

            builder
                .Property(b => b.CreatedDate)
                .HasColumnType("datetime2");
        }
    }
}
