using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BLOG.Models
{
    public partial class BLOGContext : DbContext
    {
        public BLOGContext(DbContextOptions<BLOGContext> options)
      : base(options)
        {

        }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Categorys> Categorys { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Users> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer(@"server=.;DataBase=BLOG;uid=sa;pwd=123;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Adminname)
                    .IsRequired()
                    .HasColumnName("ADMINNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ArticleId).HasColumnName("ArticleID");

                entity.Property(e => e.Time).HasMaxLength(50);
            });

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Blogcontent).HasColumnName("BLOGCONTENT");

                entity.Property(e => e.Blogtitle)
                    .HasColumnName("BLOGTITLE")
                    .HasMaxLength(100);

                entity.Property(e => e.Browamount).HasColumnName("BROWAMOUNT");

                entity.Property(e => e.Categoryid).HasColumnName("CATEGORYID");

                entity.Property(e => e.Date)
                    .IsRequired()
                    .HasColumnName("DATE")
                    .HasMaxLength(50);

                entity.Property(e => e.Isfrist).HasColumnName("ISFRIST");

                entity.Property(e => e.Lasteditdate)
                    .IsRequired()
                    .HasColumnName("LASTEDITDATE")
                    .HasMaxLength(50);

                entity.Property(e => e.State).HasColumnName("STATE");

                entity.Property(e => e.Userid).HasColumnName("USERID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.Categoryid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Articles_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_Articles_User");
            });

            modelBuilder.Entity<Categorys>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(50);

                entity.Property(e => e.State).HasColumnName("STATE");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("USERNAME")
                    .HasMaxLength(50);
            });
        }
    }
}
