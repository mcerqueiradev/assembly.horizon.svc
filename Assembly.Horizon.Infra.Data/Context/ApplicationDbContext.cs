using Assembly.Horizon.Domain.Model;
using Assembly.Horizon.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Assembly.Horizon.Infra.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Realtor> Realtors { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Favorites> Favorites { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyFile> PropertyFiles { get; set; }
    public DbSet<PropertyVisit> PropertyVisits { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new RealtorConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new FavoritesConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationsConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyFileConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyVisitConfiguration());
        modelBuilder.ApplyConfiguration(new ContractConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new InvoiceConfiguration());

        modelBuilder.HasDefaultSchema("Horizon");
    }
}
