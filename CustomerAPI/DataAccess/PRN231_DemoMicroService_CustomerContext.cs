using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CustomerAPI.DataAccess
{
    public partial class PRN231_DemoMicroService_CustomerContext : DbContext
    {
        public PRN231_DemoMicroService_CustomerContext()
        {
        }

        public PRN231_DemoMicroService_CustomerContext(DbContextOptions<PRN231_DemoMicroService_CustomerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server = 10.0.46.205,1433; database = PRN231_DemoMicroService_Customer;uid=sa;pwd=123456;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CusId);

                entity.ToTable("Customer");

                entity.Property(e => e.CusId).HasColumnName("Cus_Id");

                entity.Property(e => e.CusAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Cus_Address");

                entity.Property(e => e.CusName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Cus_Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
