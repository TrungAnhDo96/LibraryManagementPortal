using LibraryManagementPortal.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementPortal.DB
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext(DbContextOptions options) : base(options) { }

        public DbSet<User>? Users { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<BorrowRequest>? BorrowRequests { get; set; }
        public DbSet<BorrowRequestDetails>? BorrowRequestDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(user => user.ToTable("Users"));

            builder.Entity<Book>(book => book.ToTable("Books"));

            builder.Entity<Category>(category => category.ToTable("Categories"));

            builder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<BorrowRequest>(e => e.ToTable("BorrowRequests"));

            builder.Entity<BorrowRequest>()
                .HasOne(b => b.RequestedUser)
                .WithMany(c => c.BorrowRequests)
                .HasForeignKey(b => b.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Entity<BorrowRequest>()
                .HasOne(b => b.ProcessedUser)
                .WithMany(c => c.ProcessedRequests)
                .HasForeignKey(b => b.ProcessedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Entity<BorrowRequestDetails>(e => e.ToTable("BorrowRequestDetails"));

            builder.Entity<BorrowRequestDetails>()
                .HasKey(d => new { d.BorrowRequestId, d.BookId });

            builder.Entity<BorrowRequestDetails>()
                .HasOne(b => b.Book)
                .WithMany(d => d.RequestDetails)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(b => b.BookId);

            builder.Entity<BorrowRequestDetails>()
                .HasOne(b => b.BorrowRequest)
                .WithMany(d => d.RequestDetails)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(b => b.BorrowRequestId);

            builder.Entity<Book>().HasData(SeedData.SeedingBooks);
            builder.Entity<Category>().HasData(SeedData.SeedingCategories);
            builder.Entity<User>().HasData(SeedData.SeedingUsers);
            builder.Entity<BorrowRequest>().HasData(SeedData.SeedingBorrowRequests);
            builder.Entity<BorrowRequestDetails>().HasData(SeedData.SeedingBorrowRequestDetails);
        }
    }
}