using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TesteGenialNet.Data
{
    public class TesteGenialNetContext : DbContext
    {
        public TesteGenialNetContext(DbContextOptions<TesteGenialNetContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TesteGenialNetContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }

#if DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));


            base.OnConfiguring(optionsBuilder);
        }
#endif

    }
}
