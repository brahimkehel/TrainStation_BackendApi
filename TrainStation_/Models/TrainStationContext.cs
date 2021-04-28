using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TrainStation_.Models
{
    public partial class TrainStationContext : DbContext
    {
        public TrainStationContext()
        {
        }

        public TrainStationContext(DbContextOptions<TrainStationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Affectation> Affectations { get; set; }
        public virtual DbSet<Gare> Gares { get; set; }
        public virtual DbSet<Passager> Passagers { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<Trajet> Trajets { get; set; }
        public virtual DbSet<Ville> Villes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Affectation>(entity =>
            {
                entity.ToTable("Affectation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateArr)
                    .HasColumnType("date")
                    .HasColumnName("dateArr");

                entity.Property(e => e.DateDep)
                    .HasColumnType("date")
                    .HasColumnName("dateDep");

                entity.Property(e => e.HeureArr).HasColumnName("heureArr");

                entity.Property(e => e.HeureDep).HasColumnName("heureDep");

                entity.Property(e => e.IdTrain).HasColumnName("idTrain");

                entity.Property(e => e.IdTrajet).HasColumnName("idTrajet");

                entity.HasOne(d => d.IdTrainNavigation)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.IdTrain)
                    .HasConstraintName("FK__Affectati__idTra__412EB0B6");

                entity.HasOne(d => d.IdTrajetNavigation)
                    .WithMany(p => p.Affectations)
                    .HasForeignKey(d => d.IdTrajet)
                    .HasConstraintName("FK__Affectati__idTra__4222D4EF");
            });

            modelBuilder.Entity<Gare>(entity =>
            {
                entity.ToTable("Gare");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdVille).HasColumnName("idVille");

                entity.Property(e => e.NomGare)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nomGare");

                entity.HasOne(d => d.IdVilleNavigation)
                    .WithMany(p => p.Gares)
                    .HasForeignKey(d => d.IdVille)
                    .HasConstraintName("FK__Gare__idVille__3A81B327");
            });

            modelBuilder.Entity<Passager>(entity =>
            {
                entity.ToTable("Passager");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cin)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cin");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nom");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("prenom");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Annulé).HasColumnName("annulé");

                entity.Property(e => e.DateRes)
                    .HasColumnType("datetime")
                    .HasColumnName("dateRes");

                entity.Property(e => e.IdPassager).HasColumnName("idPassager");

                entity.Property(e => e.IdTrain).HasColumnName("idTrain");

                entity.HasOne(d => d.IdPassagerNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdPassager)
                    .HasConstraintName("FK__Reservati__idPas__46E78A0C");

                entity.HasOne(d => d.IdTrainNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.IdTrain)
                    .HasConstraintName("FK__Reservati__idTra__47DBAE45");
            });

            modelBuilder.Entity<Train>(entity =>
            {
                entity.ToTable("Train");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NbPlaces).HasColumnName("nbPlaces");
            });

            modelBuilder.Entity<Trajet>(entity =>
            {
                entity.ToTable("Trajet");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GareArr).HasColumnName("gareArr");

                entity.Property(e => e.GareDep).HasColumnName("gareDep");

                entity.HasOne(d => d.GareArrNavigation)
                    .WithMany(p => p.TrajetGareArrNavigations)
                    .HasForeignKey(d => d.GareArr)
                    .HasConstraintName("FK__Trajet__gareArr__3E52440B");

                entity.HasOne(d => d.GareDepNavigation)
                    .WithMany(p => p.TrajetGareDepNavigations)
                    .HasForeignKey(d => d.GareDep)
                    .HasConstraintName("FK__Trajet__gareDep__3D5E1FD2");
            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.ToTable("Ville");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nom)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("nom");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
