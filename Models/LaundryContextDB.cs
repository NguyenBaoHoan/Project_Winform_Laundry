using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Project1_Laundry.Models
{
    public partial class LaundryContextDB : DbContext
    {
        public LaundryContextDB()
            : base("name=LaundryContextDB")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbCash> tbCashes { get; set; }
        public virtual DbSet<tbCompany> tbCompanies { get; set; }
        public virtual DbSet<tbCostofGood> tbCostofGoods { get; set; }
        public virtual DbSet<tbCustomer> tbCustomers { get; set; }
        public virtual DbSet<tbEmployee> tbEmployees { get; set; }
        public virtual DbSet<tbService> tbServices { get; set; }
        public virtual DbSet<tbType> tbTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbCash>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<tbCustomer>()
                .HasMany(e => e.tbCashes)
                .WithOptional(e => e.tbCustomer)
                .HasForeignKey(e => e.cid);

            modelBuilder.Entity<tbService>()
                .HasMany(e => e.tbCashes)
                .WithOptional(e => e.tbService)
                .HasForeignKey(e => e.sid);

            modelBuilder.Entity<tbType>()
                .HasMany(e => e.tbCashes)
                .WithOptional(e => e.tbType)
                .HasForeignKey(e => e.idType);

            modelBuilder.Entity<tbType>()
                .HasMany(e => e.tbCustomers)
                .WithOptional(e => e.tbType)
                .HasForeignKey(e => e.idType);
        }
    }
}
