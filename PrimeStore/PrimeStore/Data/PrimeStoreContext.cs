using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Models;

namespace PrimeStore.Data;

public partial class PrimeStoreContext : IdentityDbContext<User>
{
    public PrimeStoreContext()
    {
        
    }

    public PrimeStoreContext(DbContextOptions<PrimeStoreContext> options)
        : base(options)
    {
       
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Models.File> Files { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<SharedFile> SharedFiles { get; set; }

    public virtual DbSet<SharedFolder> SharedFolders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=PrimeStore;Username=postgres;Password=password");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Basket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("basket_pkey");

            entity.ToTable("basket");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id");
            entity.Property(e => e.IdPreviousFolder).HasColumnName("id_previous_folder");
            entity.Property(e => e.Path)
                .HasMaxLength(128)
                .HasColumnName("path");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Baskets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<Models.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.HasIndex(e => e.Filename, "files_filename_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id");
            entity.Property(e => e.BasketId).HasColumnName("basket_id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Filename)
                .HasMaxLength(16)
                .HasColumnName("filename");
            entity.Property(e => e.FolderId).HasColumnName("folder_id");
            entity.Property(e => e.Size)
                .HasMaxLength(5)
                .HasColumnName("size");
            entity.Property(e => e.UploadTime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("upload_time");

            entity.HasOne(d => d.Basket).WithMany(p => p.Files)
                .HasForeignKey(d => d.BasketId)
                .HasConstraintName("fk_basket_id");

            entity.HasOne(d => d.Folder).WithMany(p => p.Files)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("fk_folder_id");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("folder_pkey");

            entity.ToTable("folder");

            entity.HasIndex(e => e.Foldername, "folder_foldername_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id");
            entity.Property(e => e.CreationTime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("creation_time");
            entity.Property(e => e.Foldername)
                .HasMaxLength(32)
                .HasColumnName("foldername");
            entity.Property(e => e.IdNextFolder).HasColumnName("id_next_folder");
            entity.Property(e => e.IdPreviousFolder).HasColumnName("id_previous_folder");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<SharedFile>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("shared_files");

            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.File).WithMany()
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("fk_folder_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<SharedFolder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("shared_folder");

            entity.Property(e => e.FolderId).HasColumnName("folder_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Folder).WithMany()
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("fk_folder_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "users_login_key").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("Id");
            entity.Property(e => e.Login)
                .HasMaxLength(16)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(16)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(32)
                .HasColumnName("username");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
