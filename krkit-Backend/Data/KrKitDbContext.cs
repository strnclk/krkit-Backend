using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using krkit_Backend.Data.Models;
using System.Linq.Expressions;

namespace krkit_Backend.Data
{
    public class KrKitDbContext : DbContext
    {
        public KrKitDbContext(DbContextOptions<KrKitDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Product> Products { get; set; }



        //DBCC CHECKIDENT ('Users', RESEED, 0);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
                }
            }

            modelBuilder.Entity<User>()
           .HasIndex(u => u.Username)
           .IsUnique(); // Kullanıcı adı benzersiz olacak

            modelBuilder.Entity<Product>()
       .HasIndex(u => u.Barcode)
       .IsUnique(); // BARKOD benzersiz olacak

            base.OnModelCreating(modelBuilder);
        }

        private static LambdaExpression ConvertFilterExpression(Type entityType)
        {
            var parameter = Expression.Parameter(entityType, "e");
            var property = Expression.Property(parameter, "IsDeleted");
            var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);
            return filter;
        }

    }
}
