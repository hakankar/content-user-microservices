using Domain.Entities;
using Domain.Extensions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            PropertyAdjustment(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        public void BeginTransaction()
        {
            Database.BeginTransaction();
        }
        public void CommitTransaction()
        {
            Database.CommitTransaction();
        }
        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }
        public IExecutionStrategy CreateExecutionStrategy()
        {
            return Database.CreateExecutionStrategy();
        }
        public Task ExecuteAsync(Func<Task> operation)
        {
            return CreateExecutionStrategy().ExecuteAsync(operation);
        }
        public async Task BeginTransactionAsync()
        {
            await Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await Database.CommitTransactionAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await Database.RollbackTransactionAsync();
        }


        private static void PropertyAdjustment(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 4)");
            }
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType("timestamp");
            }
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()?.ToLowerSnakeCase());
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLowerSnakeCase());
                }
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName()?.ToLowerSnakeCase());
                }
                foreach (var key in entity.GetForeignKeys())
                {
                    if (key != null && key.PrincipalKey != null)
                    {
                        key.PrincipalKey.SetName(key.PrincipalKey.GetName()?.ToLowerSnakeCase());
                        key.DeleteBehavior = DeleteBehavior.Restrict;
                    }
                }
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName()?.ToLowerSnakeCase());
                }
            }
        }
    }
}
