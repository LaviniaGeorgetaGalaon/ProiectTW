using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace smart_dorms.Models
{
    public partial class uvtdemosdbContext : DbContext
    {
        public uvtdemosdbContext()
        {
        }

        public uvtdemosdbContext(DbContextOptions<uvtdemosdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Camera> Camera { get; set; }
        public virtual DbSet<Camin> Camin { get; set; }
        public virtual DbSet<DateUser> DateUser { get; set; }
        public virtual DbSet<Dosar> Dosar { get; set; }
        public virtual DbSet<Obiecte> Obiecte { get; set; }
        public virtual DbSet<Permisiuni> Permisiuni { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Roluri> Roluri { get; set; }
        public virtual DbSet<TipCamera> TipCamera { get; set; }
        public virtual DbSet<TipCerere> TipCerere { get; set; }
        public virtual DbSet<TipDocumente> TipDocumente { get; set; }
        public virtual DbSet<TipObiect> TipObiect { get; set; }
        public virtual DbSet<TipStatus> TipStatus { get; set; }
        public virtual DbSet<Useri> Useri { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:uvt-demos-server-db.database.windows.net,1433;Initial Catalog=uvt-demos-db;Persist Security Info=False;User ID=AdminUser;Password=Andreea19.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCamin).HasColumnName("idCamin");

                entity.Property(e => e.IdTipCamera).HasColumnName("idTipCamera");

                entity.HasOne(d => d.IdCaminNavigation)
                    .WithMany(p => p.Camera)
                    .HasForeignKey(d => d.IdCamin)
                    .HasConstraintName("FK__Camera__idCamin__1BC821DD");

                entity.HasOne(d => d.IdTipCameraNavigation)
                    .WithMany(p => p.Camera)
                    .HasForeignKey(d => d.IdTipCamera)
                    .HasConstraintName("FK__Camera__idTipCam__1AD3FDA4");
            });

            modelBuilder.Entity<Camin>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LocatieCamin)
                    .HasColumnName("locatieCamin")
                    .HasMaxLength(255);

                entity.Property(e => e.NameCamin)
                    .HasColumnName("nameCamin")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<DateUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);

                entity.Property(e => e.Prenume)
                    .HasColumnName("prenume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Dosar>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdTipDocumente).HasColumnName("idTipDocumente");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.HasOne(d => d.IdTipDocumenteNavigation)
                    .WithMany(p => p.Dosar)
                    .HasForeignKey(d => d.IdTipDocumente)
                    .HasConstraintName("FK__Dosar__idTipDocu__3B40CD36");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Dosar)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Dosar__idUser__3C34F16F");
            });

            modelBuilder.Entity<Obiecte>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantitate).HasColumnName("cantitate");

                entity.Property(e => e.IdCamera).HasColumnName("idCamera");

                entity.Property(e => e.IdTipObiect).HasColumnName("idTipObiect");

                entity.HasOne(d => d.IdCameraNavigation)
                    .WithMany(p => p.Obiecte)
                    .HasForeignKey(d => d.IdCamera)
                    .HasConstraintName("FK__Obiecte__idCamer__1EA48E88");

                entity.HasOne(d => d.IdTipObiectNavigation)
                    .WithMany(p => p.Obiecte)
                    .HasForeignKey(d => d.IdTipObiect)
                    .HasConstraintName("FK__Obiecte__idTipOb__1DB06A4F");
            });

            modelBuilder.Entity<Permisiuni>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.NumePagina)
                    .HasColumnName("numePagina")
                    .HasMaxLength(255);

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Permisiuni)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Permisiun__idRol__208CD6FA");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Detalii)
                    .HasColumnName("detalii")
                    .HasMaxLength(255);

                entity.Property(e => e.IdCamera).HasColumnName("idCamera");

                entity.Property(e => e.IdTipCerere).HasColumnName("idTipCerere");

                entity.Property(e => e.IdTipStatus).HasColumnName("idTipStatus");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Prioritate).HasColumnName("prioritate");

                entity.HasOne(d => d.IdCameraNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.IdCamera)
                    .HasConstraintName("FK__Request__idCamer__367C1819");

                entity.HasOne(d => d.IdTipCerereNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.IdTipCerere)
                    .HasConstraintName("FK__Request__idTipCe__3587F3E0");

                entity.HasOne(d => d.IdTipStatusNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.IdTipStatus)
                    .HasConstraintName("FK__Request__idTipSt__37703C52");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Request)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK__Request__idUser__3864608B");
            });

            modelBuilder.Entity<Roluri>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });


            modelBuilder.Entity<TipCamera>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.NrMaxLocuriDisp).HasColumnName("nrMaxLocuriDisp");

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipCerere>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipDocumente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipObiect>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantitate).HasColumnName("cantitate");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TipStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descriere)
                    .HasColumnName("descriere")
                    .HasMaxLength(255);

                entity.Property(e => e.Nume)
                    .HasColumnName("nume")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Useri>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gdpr).HasColumnName("gdpr");

                entity.Property(e => e.IdCamera).HasColumnName("idCamera");

                entity.Property(e => e.IdDateUser).HasColumnName("idDateUser");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdCameraNavigation)
                    .WithMany(p => p.Useri)
                    .HasForeignKey(d => d.IdCamera)
                    .HasConstraintName("FK__Useri__idCamera__32AB8735");

                entity.HasOne(d => d.IdDateUserNavigation)
                    .WithMany(p => p.Useri)
                    .HasForeignKey(d => d.IdDateUser)
                    .HasConstraintName("FK__Useri__idDateUse__339FAB6E");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Useri)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Useri__idRol__31B762FC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
