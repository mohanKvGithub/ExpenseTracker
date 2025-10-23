using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data;

public partial class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public virtual DbSet<Transaction> Transaction { get; set; }
    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<BudgetReset> BudgetReset { get; set; }
    public virtual DbSet<PaymentType> PaymentType { get; set; }
    public virtual DbSet<TransactionType> TransactionType { get; set; }
    public virtual DbSet<Budget> Budget { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transaction");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Dr).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cr).HasColumnType("decimal(18, 2)");
        });
        modelBuilder.Entity<Budget>(entity =>
        {
            entity.ToTable("Budget");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });
        modelBuilder.Entity<BudgetReset>(entity =>
        {
            entity.ToTable("BudgetReset");
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.ToTable("PaymentType");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccountName).HasMaxLength(20);
            entity.Property(e => e.Type).HasMaxLength(10);
        });
        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.ToTable("TransactionType");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).HasMaxLength(20);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Salt).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
