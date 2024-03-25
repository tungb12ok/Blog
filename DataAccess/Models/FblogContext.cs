    using System;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    namespace DataAccess.Models;

    public partial class FblogContext : DbContext
    {
        public FblogContext()
        {
        }

        public FblogContext(DbContextOptions<FblogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Field> Fields { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("server =localhost; database = FBlog;uid=sa;pwd=sa;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Article__3213E83F5284D24B");

                entity.ToTable("Article");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ArticleContent)
                    .HasMaxLength(2000)
                    .IsFixedLength()
                    .HasColumnName("article_content");
                entity.Property(e => e.DatePublished)
                    .HasColumnType("date")
                    .HasColumnName("date_published");
                entity.Property(e => e.FieldId).HasColumnName("field_id");
                entity.Property(e => e.Image).HasColumnName("image");
                entity.Property(e => e.StatusId).HasColumnName("statusID");
                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("title");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Vote).HasColumnName("vote");

                entity.HasOne(d => d.Field).WithMany(p => p.Articles)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_Field");

                entity.HasOne(d => d.Status).WithMany(p => p.Articles)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Article_Status");

                entity.HasOne(d => d.User).WithMany(p => p.Articles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Article_User");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Comment__3213E83FFBED1E5D");

                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ArticleId).HasColumnName("article_id");
                entity.Property(e => e.CommentContent)
                    .HasMaxLength(250)
                    .IsFixedLength()
                    .HasColumnName("comment_content");
                entity.Property(e => e.DateCommented)
                    .HasColumnType("datetime")
                    .HasColumnName("date_commented");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Article).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Article");

                entity.HasOne(d => d.User).WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Field__3213E83FA2A56914");

                entity.ToTable("Field");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsFixedLength()
                    .HasColumnName("description");
                entity.Property(e => e.FieldName)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("field_name");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Notifica__3213E83FE413E329");

                entity.ToTable("Notification");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.NotificationContent)
                    .HasMaxLength(250)
                    .IsFixedLength()
                    .HasColumnName("notification_content");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notification_User");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Report__3213E83FA2B69EC8");

                entity.ToTable("Report");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ArticleId).HasColumnName("article_id");
                entity.Property(e => e.ReportContent)
                    .HasMaxLength(250)
                    .IsFixedLength()
                    .HasColumnName("report_content");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Article).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_Article");

                entity.HasOne(d => d.User).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FE7755718");

                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("statusID");
                entity.Property(e => e.Status1)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__User__3213E83F7C6E99FC");

                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Bio)
                    .HasMaxLength(250)
                    .IsFixedLength()
                    .HasColumnName("bio");
                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsFixedLength()
                    .HasColumnName("email");
                entity.Property(e => e.FieldId).HasColumnName("field_id");
                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("gender");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("password");
                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("phonenumber");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("status");
                entity.Property(e => e.Yearofbirth).HasColumnName("yearofbirth");

                entity.HasOne(d => d.Field).WithMany(p => p.Users)
                    .HasForeignKey(d => d.FieldId)
                    .HasConstraintName("FK_User_Field");

                entity.HasOne(d => d.Role).WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Vote__3213E83FAF5ABB0A");

                entity.ToTable("Vote");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ArticleId).HasColumnName("article_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Article).WithMany(p => p.Votes)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_Article");

                entity.HasOne(d => d.User).WithMany(p => p.Votes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
