using Microsoft.EntityFrameworkCore;

namespace Payment.Persistence.Models;

public partial class PaymentDbContext : DbContext
{
    public PaymentDbContext()
    {
    }

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<PaymentSm> PaymentSms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("branch");

            entity.Property(e => e.BranchName)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<PaymentSm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_sms");

            entity.Property(e => e.AccountantNote)
                .HasMaxLength(1000)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AccountantNoteModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ActionBy).HasMaxLength(200);
            entity.Property(e => e.BranchId).HasDefaultValueSql("'1'");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("utc_timestamp()")
                .HasColumnType("datetime");
            entity.Property(e => e.InboundPaymentDate).HasColumnType("datetime");
            entity.Property(e => e.InboundPaymentModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.InboundPaymentNumber).HasMaxLength(100);
            entity.Property(e => e.LbsdocumentCode)
                .HasMaxLength(100)
                .HasColumnName("LBSDocumentCode")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Lbsmodified)
                .HasMaxLength(100)
                .HasColumnName("LBSModified")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Lbsplate)
                .HasMaxLength(100)
                .HasColumnName("LBSPlate")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.LbsuserName)
                .HasMaxLength(100)
                .HasColumnName("LBSUserName")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ObjectType)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Accountant'")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OdooCustomerCode)
                .HasMaxLength(20)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OdooCustomerName)
                .HasMaxLength(500)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OdooModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OdooSonumber)
                .HasMaxLength(200)
                .HasColumnName("OdooSONumber")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OtherRemark)
                .HasMaxLength(1000)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OtherRemarkModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SaleName).HasMaxLength(500);
            entity.Property(e => e.SignalModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SignalStatusName)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SimModified)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SimStatusName)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Smsamount)
                .HasPrecision(10)
                .HasColumnName("SMSAmount");
            entity.Property(e => e.SmsbankName)
                .HasMaxLength(100)
                .HasColumnName("SMSBankName")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SmsbankNumber)
                .HasMaxLength(100)
                .HasColumnName("SMSBankNumber")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Smscontent)
                .HasMaxLength(500)
                .HasColumnName("SMSContent")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Smsdate)
                .HasColumnType("datetime")
                .HasColumnName("SMSDate");
            entity.Property(e => e.Smsid).HasColumnName("SMSId");
            entity.Property(e => e.Smsphone)
                .HasMaxLength(20)
                .HasColumnName("SMSPhone")
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Vaaccount)
                .HasMaxLength(200)
                .HasColumnName("VAAccount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
