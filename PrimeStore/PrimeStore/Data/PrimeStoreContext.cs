using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrimeStore.Data.Models;

namespace PrimeStore.Data;

public partial class PrimeStoreContext : IdentityDbContext<User>
{
    public PrimeStoreContext()
    {
       // Database.EnsureDeleted();
    }

    public PrimeStoreContext(DbContextOptions<PrimeStoreContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
    }

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

        modelBuilder.Entity<Models.File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, null, 0L, null, null, null)
                .HasColumnName("id");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Filename)
                .HasMaxLength(128)
                .HasColumnName("filename");
            entity.Property(e => e.FolderId).HasColumnName("folder_id");
            entity.Property(e => e.Size)
                .HasMaxLength(128)
                .HasColumnName("size");
            entity.Property(e => e.UploadTime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("upload_time");
            entity.Property(e => e.InBasket).HasColumnName("in_basket");

            entity.HasOne(d => d.Folder).WithMany(p => p.Files)
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("fk_folder_id");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("folder_pkey");

            entity.ToTable("folder");

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
            entity.Property(e => e.Guid).HasColumnName("user_id");
            entity.Property(e => e.InBasket).HasColumnName("in_basket");

            entity.HasOne(d => d.User).WithMany(p => p.Folders)
                .HasForeignKey(d => d.Guid)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<SharedFile>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("shared_files");

            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.Guid).HasColumnName("user_id");

            entity.HasOne(d => d.File).WithMany()
                .HasForeignKey(d => d.FileId)
                .HasConstraintName("fk_folder_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.Guid)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<SharedFolder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("shared_folder");

            entity.Property(e => e.FolderId).HasColumnName("folder_id");
            entity.Property(e => e.Guid).HasColumnName("user_id");

            entity.HasOne(d => d.Folder).WithMany()
                .HasForeignKey(d => d.FolderId)
                .HasConstraintName("fk_folder_id");

            entity.HasOne(d => d.User).WithMany()
                .HasForeignKey(d => d.Guid)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
