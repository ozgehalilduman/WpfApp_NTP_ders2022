using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WpfApp_NTP_ders
{
    public partial class vt2022Context : DbContext
    {
        public vt2022Context()
        {
        }

        public vt2022Context(DbContextOptions<vt2022Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Ogrenci> Ogrencis { get; set; } = null!;
        public virtual DbSet<Okul> Okuls { get; set; } = null!;
        public virtual DbSet<Sinif> Sinifs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=vt2022;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ogrenci>(entity =>
            {
                entity.HasOne(d => d.SinifNavigation)
                    .WithMany(p => p.Ogrencis)
                    .HasForeignKey(d => d.Sinif)
                    .HasConstraintName("FK_Ogrenci_Sinif");
            });

            modelBuilder.Entity<Sinif>(entity =>
            {
                entity.HasOne(d => d.OkulNavigation)
                    .WithMany(p => p.Sinifs)
                    .HasForeignKey(d => d.Okul)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sinif_Okul");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
