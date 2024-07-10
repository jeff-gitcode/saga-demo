using Microsoft.EntityFrameworkCore;
using saga_demo2.saga;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewsLetterOnboardingSagaData>(entity =>
        {
            entity.HasKey(e => e.CorrelationId);
        });
    }

    public DbSet<Subscriber> Subscribers { get; set; }

    public DbSet<NewsLetterOnboardingSagaData> SagaData { get; set; } 
}

public class Subscriber
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTime SubscribedAt { get; set; }
}