using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace tailorShop.Models
{
    public partial class tailoreShopContext : DbContext
    {
        public tailoreShopContext()
        {
        }

        public tailoreShopContext(DbContextOptions<tailoreShopContext> options): base(options)
        {
        }

        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<LowerBody> LowerBody { get; set; }
        public virtual DbSet<Security> Security { get; set; }
        public virtual DbSet<Shops> Shops { get; set; }
        public virtual DbSet<UpperBody> UpperBody { get; set; }
        public virtual DbSet<UserDetails> UserDetails { get; set; }
        public virtual DbSet<UserType> UserType { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Work> Work { get; set; }
        public virtual DbSet<WorkOut> WorkOut { get; set; }
        public virtual DbSet<LaxumiPooja> LaxumiPooja { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=PRITAM-KININGE-\\SQLEXPRESS; Database=tailoreShop; Trusted_connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Feedback1)
                    .HasColumnName("feedback")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Fit).HasColumnName("fit");

                entity.Property(e => e.Quality).HasColumnName("quality");

                entity.Property(e => e.Style).HasColumnName("style");
            });

            modelBuilder.Entity<LowerBody>(entity =>
            {
                entity.HasKey(e => new { e.WorkId, e.ClothId })
                    .HasName("PK__LowerBod__2BE53E36AB01AD7F");

                entity.Property(e => e.WorkId).HasColumnName("workId");

                entity.Property(e => e.ClothId).HasColumnName("clothId");

                entity.Property(e => e.AssignTo).HasColumnName("assignTo");

                entity.Property(e => e.Bottom)
                    .HasColumnName("bottom")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.ClothName)
                    .IsRequired()
                    .HasColumnName("clothName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Knee)
                    .HasColumnName("knee")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaidTo)
                    .HasColumnName("paidTo")
                    .HasColumnType("money");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Seat)
                    .HasColumnName("seat")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Thigh)
                    .HasColumnName("thigh")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Underline)
                    .HasColumnName("underline")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Waist)
                    .HasColumnName("waist")
                    .HasColumnType("decimal(4, 2)");

                entity.HasOne(d => d.AssignToNavigation)
                    .WithMany(p => p.LowerBody)
                    .HasForeignKey(d => d.AssignTo)
                    .HasConstraintName("FK__LowerBody__assig__51300E55");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.LowerBody)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK__LowerBody__feedb__5224328E");

                entity.HasOne(d => d.Work)
                    .WithMany(p => p.LowerBody)
                    .HasForeignKey(d => d.WorkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LowerBody__workI__4F47C5E3");
            });

            modelBuilder.Entity<Security>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Security__6238D4B22D54F347");

                entity.HasIndex(e => e.Question)
                    .HasName("UQ__Security__9AA2C3BEF2593F18")
                    .IsUnique();

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasColumnName("question")
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shops>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__Shops__E5C424DCC7669AD6");

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasColumnName("branchName")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasColumnName("shopName")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UpperBody>(entity =>
            {
                entity.HasKey(e => new { e.WorkId, e.ClothId })
                    .HasName("PK__UpperBod__2BE53E36C1322429");

                entity.Property(e => e.WorkId).HasColumnName("workId");

                entity.Property(e => e.ClothId).HasColumnName("clothId");

                entity.Property(e => e.AssignTo).HasColumnName("assignTo");

                entity.Property(e => e.ClothName)
                    .IsRequired()
                    .HasColumnName("clothName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Collar)
                    .HasColumnName("collar")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Cuff)
                    .HasColumnName("cuff")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Front)
                    .HasColumnName("front")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Height)
                    .HasColumnName("height")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PaidTo)
                    .HasColumnName("paidTo")
                    .HasColumnType("money");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.Shoulder)
                    .HasColumnName("shoulder")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Sleeve)
                    .HasColumnName("sleeve")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.SleeveWidth)
                    .HasColumnName("sleeveWidth")
                    .HasColumnType("decimal(4, 2)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AssignToNavigation)
                    .WithMany(p => p.UpperBody)
                    .HasForeignKey(d => d.AssignTo)
                    .HasConstraintName("FK__UpperBody__assig__4B7734FF");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.UpperBody)
                    .HasForeignKey(d => d.FeedbackId)
                    .HasConstraintName("FK__UpperBody__feedb__4C6B5938");

                entity.HasOne(d => d.Work)
                    .WithMany(p => p.UpperBody)
                    .HasForeignKey(d => d.WorkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UpperBody__workI__498EEC8D");
            });

            modelBuilder.Entity<UserDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserDeta__CB9A1CFFD596BBE4");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("date");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EmailAddress)
                    .HasColumnName("emailAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete)
                    .HasColumnName("isDelete")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsOnWhatsApp)
                    .HasColumnName("isOnWhatsApp")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsTestData)
                    .HasColumnName("isTestData")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobileNo")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updatedOn")
                    .HasColumnType("date");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.InverseCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__UserDetai__creat__22751F6C");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.UserDetails)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserDetai__shopI__208CD6FA");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InverseUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__UserDetai__updat__236943A5");
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasIndex(e => e.UserType1)
                    .HasName("UQ__UserType__7383789938A63569")
                    .IsUnique();

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeId");

                entity.Property(e => e.UserType1)
                    .IsRequired()
                    .HasColumnName("userType")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.LogInid)
                    .HasName("PK__Users__7FE165E3A2CB10B3");

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__F3DBC57236636719")
                    .IsUnique();

                entity.Property(e => e.LogInid).HasColumnName("logINId");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasColumnName("answer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionId).HasColumnName("questionId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.UserTypeId).HasColumnName("userTypeId");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__questionI__6FB49575");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Users__userId__6DCC4D03");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__userTypeI__6EC0713C");
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.Property(e => e.WorkId).HasColumnName("workId");

                entity.Property(e => e.Advance)
                    .HasColumnName("advance")
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("createdOn")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeliveredOn).HasColumnType("date");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnName("deliveryDate")
                    .HasColumnType("date");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updatedOn")
                    .HasColumnType("date");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.WorkCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__Work__createdBy__3D2915A8");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.WorkUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__Work__updatedBy__3E1D39E1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Work__userId__3B40CD36");
            });

            modelBuilder.Entity<WorkOut>(entity =>
            {
                entity.Property(e => e.WorkOutId).HasColumnName("workOutId");

                entity.Property(e => e.CreatedBy).HasColumnName("createdBy");

                entity.Property(e => e.DeliveredOn)
                    .HasColumnName("deliveredOn")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pay)
                    .HasColumnName("pay")
                    .HasColumnType("money");

                entity.Property(e => e.UpdatedBy).HasColumnName("updatedBy");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("updatedOn")
                    .HasColumnType("date");

                entity.Property(e => e.WorkId).HasColumnName("workId");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.WorkOutCreatedByNavigation)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__WorkOut__created__56E8E7AB");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.WorkOutUpdatedByNavigation)
                    .HasForeignKey(d => d.UpdatedBy)
                    .HasConstraintName("FK__WorkOut__updated__57DD0BE4");

                entity.HasOne(d => d.Work)
                    .WithMany(p => p.WorkOut)
                    .HasForeignKey(d => d.WorkId)
                    .HasConstraintName("FK__WorkOut__workId__55009F39");
            });

            modelBuilder.Entity<LaxumiPooja>(entity =>
            {
                entity.Property(e => e.Year)
                    .HasColumnName("year")
                    .HasColumnType("decimal(4,0)");

                entity.Property(e => e.Date)
                .HasColumnName("date")
                .HasColumnType("date");
            });
        }
    }
}
