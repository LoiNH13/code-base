using Microsoft.EntityFrameworkCore;
using Odoo.Domain.Entities;

namespace Odoo.Persistence;

public partial class OdooDbContext : DbContext
{
    public OdooDbContext()
    {
    }

    public OdooDbContext(DbContextOptions<OdooDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountAnalyticAccount> AccountAnalyticAccounts { get; set; }

    public virtual DbSet<AccountJournal> AccountJournals { get; set; }

    public virtual DbSet<AccountMove> AccountMoves { get; set; }

    public virtual DbSet<AccountPayment> AccountPayments { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductProduct> ProductProducts { get; set; }

    public virtual DbSet<ReportSaleOrderView> ReportSaleOrderViews { get; set; }

    public virtual DbSet<ResCountryState> ResCountryStates { get; set; }

    public virtual DbSet<ResDistrict> ResDistricts { get; set; }

    public virtual DbSet<ResPartner> ResPartners { get; set; }

    public virtual DbSet<ResUser> ResUsers { get; set; }

    public virtual DbSet<ResWard> ResWards { get; set; }

    public virtual DbSet<ResourceResource> ResourceResources { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleReport> SaleReports { get; set; }

    public virtual DbSet<StockLot> StockLots { get; set; }

    public virtual DbSet<VietmapCustomerView> VietmapCustomerViews { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("C")
            .HasPostgresExtension("dblink")
            .HasPostgresExtension("pg_trgm")
            .HasPostgresExtension("unaccent");

        modelBuilder.Entity<AccountAnalyticAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_analytic_account_pkey");

            entity.ToTable("account_analytic_account", tb => tb.HasComment("Analytic Account"));

            entity.HasIndex(e => e.Code, "account_analytic_account__code_index");

            entity.HasIndex(e => e.CompanyAnalyticAccountId, "account_analytic_account__company_analytic_account_id_index");

            entity.HasIndex(e => e.ParentId, "account_analytic_account__parent_id_index");

            entity.HasIndex(e => e.ParentPath, "account_analytic_account__parent_path_index");

            entity.HasIndex(e => new { e.Code, e.CompanyAnalyticAccountId }, "account_analytic_account_code_analytic_account_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.AnalyticType)
                .HasComment("Analytic Type")
                .HasColumnType("character varying")
                .HasColumnName("analytic_type");
            entity.Property(e => e.Code)
                .HasComment("Reference")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CompanyAnalyticAccountId)
                .HasComment("Company Analytic Account")
                .HasColumnName("company_analytic_account_id");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.CompleteName)
                .HasComment("Full Analytic Hierarchy Name")
                .HasColumnType("character varying")
                .HasColumnName("complete_name");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.Level)
                .HasComment("Level")
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .HasComment("Analytic Account")
                .HasColumnType("jsonb")
                .HasColumnName("name");
            entity.Property(e => e.ParentId)
                .HasComment("Parent Analytic Account")
                .HasColumnName("parent_id");
            entity.Property(e => e.ParentPath)
                .HasColumnType("character varying")
                .HasColumnName("parent_path");
            entity.Property(e => e.PartnerId)
                .HasComment("Customer")
                .HasColumnName("partner_id");
            entity.Property(e => e.PlanId)
                .HasComment("Plan")
                .HasColumnName("plan_id");
            entity.Property(e => e.RootPlanId)
                .HasComment("Root Plan")
                .HasColumnName("root_plan_id");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.WillApiConsignment)
                .HasComment("Will API Consignment")
                .HasColumnName("will_api_consignment");
            entity.Property(e => e.WillApiWarehouseTransfer)
                .HasComment("Will API Warehouse Transfer")
                .HasColumnName("will_api_warehouse_transfer");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CompanyAnalyticAccount).WithMany(p => p.InverseCompanyAnalyticAccount)
                .HasForeignKey(d => d.CompanyAnalyticAccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_analytic_account_company_analytic_account_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.AccountAnalyticAccountCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_analytic_account_create_uid_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("account_analytic_account_parent_id_fkey");

            entity.HasOne(d => d.Partner).WithMany(p => p.AccountAnalyticAccounts)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_analytic_account_partner_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.AccountAnalyticAccountWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_analytic_account_write_uid_fkey");
        });

        modelBuilder.Entity<AccountJournal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_journal_pkey");

            entity.ToTable("account_journal", tb => tb.HasComment("Journal"));

            entity.HasIndex(e => e.CompanyId, "account_journal__company_id_index");

            entity.HasIndex(e => new { e.CompanyId, e.Code }, "account_journal_code_company_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessToken)
                .HasComment("Security Token")
                .HasColumnType("character varying")
                .HasColumnName("access_token");
            entity.Property(e => e.AccountDiscountId)
                .HasComment("Trade Discount Account")
                .HasColumnName("account_discount_id");
            entity.Property(e => e.AccountOnlineAccountId)
                .HasComment("Account Online Account")
                .HasColumnName("account_online_account_id");
            entity.Property(e => e.AccountOnlineLinkId)
                .HasComment("Account Online Link")
                .HasColumnName("account_online_link_id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.AliasId)
                .HasComment("Alias")
                .HasColumnName("alias_id");
            entity.Property(e => e.BankAccountId)
                .HasComment("Bank Account")
                .HasColumnName("bank_account_id");
            entity.Property(e => e.BankStatementsSource)
                .HasComment("Bank Feeds")
                .HasColumnType("character varying")
                .HasColumnName("bank_statements_source");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .HasComment("Short Code")
                .HasColumnName("code");
            entity.Property(e => e.Color)
                .HasComment("Color Index")
                .HasColumnName("color");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.CurrencyId)
                .HasComment("Currency")
                .HasColumnName("currency_id");
            entity.Property(e => e.DefaultAccountId)
                .HasComment("Default Account")
                .HasColumnName("default_account_id");
            entity.Property(e => e.GenerateDiscountAccount)
                .HasComment("Enable Trade Discount")
                .HasColumnName("generate_discount_account");
            entity.Property(e => e.InvoiceReferenceModel)
                .HasComment("Communication Standard")
                .HasColumnType("character varying")
                .HasColumnName("invoice_reference_model");
            entity.Property(e => e.InvoiceReferenceType)
                .HasComment("Communication Type")
                .HasColumnType("character varying")
                .HasColumnName("invoice_reference_type");
            entity.Property(e => e.IsFullApi)
                .HasComment("Full API")
                .HasColumnName("is_full_api");
            entity.Property(e => e.LossAccountId)
                .HasComment("Loss Account")
                .HasColumnName("loss_account_id");
            entity.Property(e => e.Name)
                .HasComment("Journal Name")
                .HasColumnType("jsonb")
                .HasColumnName("name");
            entity.Property(e => e.PaymentSequence)
                .HasComment("Dedicated Payment Sequence")
                .HasColumnName("payment_sequence");
            entity.Property(e => e.ProfitAccountId)
                .HasComment("Profit Account")
                .HasColumnName("profit_account_id");
            entity.Property(e => e.RefundSequence)
                .HasComment("Dedicated Credit Note Sequence")
                .HasColumnName("refund_sequence");
            entity.Property(e => e.RenewalContactEmail)
                .HasComment("Connection Requests")
                .HasColumnType("character varying")
                .HasColumnName("renewal_contact_email");
            entity.Property(e => e.RestrictModeHashTable)
                .HasComment("Lock Posted Entries with Hash")
                .HasColumnName("restrict_mode_hash_table");
            entity.Property(e => e.SaleActivityNote)
                .HasComment("Activity Summary")
                .HasColumnName("sale_activity_note");
            entity.Property(e => e.SaleActivityTypeId)
                .HasComment("Schedule Activity")
                .HasColumnName("sale_activity_type_id");
            entity.Property(e => e.SaleActivityUserId)
                .HasComment("Activity User")
                .HasColumnName("sale_activity_user_id");
            entity.Property(e => e.SecureSequenceId)
                .HasComment("Secure Sequence")
                .HasColumnName("secure_sequence_id");
            entity.Property(e => e.Sequence)
                .HasComment("Sequence")
                .HasColumnName("sequence");
            entity.Property(e => e.SequenceId)
                .HasComment("Entry Sequence")
                .HasColumnName("sequence_id");
            entity.Property(e => e.SequenceOverrideRegex)
                .HasComment("Sequence Override Regex")
                .HasColumnName("sequence_override_regex");
            entity.Property(e => e.ShowOnDashboard)
                .HasComment("Show journal on dashboard")
                .HasColumnName("show_on_dashboard");
            entity.Property(e => e.SuspenseAccountId)
                .HasComment("Suspense Account")
                .HasColumnName("suspense_account_id");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.Type)
                .HasComment("Type")
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.UseForEmpExpense)
                .HasComment("Use for Employee Expenses")
                .HasColumnName("use_for_emp_expense");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.AccountJournalCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_journal_create_uid_fkey");

            entity.HasOne(d => d.SaleActivityUser).WithMany(p => p.AccountJournalSaleActivityUsers)
                .HasForeignKey(d => d.SaleActivityUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_journal_sale_activity_user_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.AccountJournalWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_journal_write_uid_fkey");
        });

        modelBuilder.Entity<AccountMove>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_move_pkey");

            entity.ToTable("account_move", tb => tb.HasComment("Journal Entry"));

            entity.HasIndex(e => e.AnalyticAccountId, "account_move__analytic_account_id_index");

            entity.HasIndex(e => e.AssetId, "account_move__asset_id_index");

            entity.HasIndex(e => e.AutoInvoiceId, "account_move__auto_invoice_id_index").HasFilter("(auto_invoice_id IS NOT NULL)");

            entity.HasIndex(e => e.AutoPostOriginId, "account_move__auto_post_origin_id_index").HasFilter("(auto_post_origin_id IS NOT NULL)");

            entity.HasIndex(e => e.CampaignId, "account_move__campaign_id_index").HasFilter("(campaign_id IS NOT NULL)");

            entity.HasIndex(e => e.CompanyId, "account_move__company_id_index");

            entity.HasIndex(e => e.CustomerPaymentFeeId, "account_move__customer_payment_fee_id_index");

            entity.HasIndex(e => e.Date, "account_move__date_index");

            entity.HasIndex(e => e.DestCompanyId, "account_move__dest_company_id_index");

            entity.HasIndex(e => e.ExpenseSheetId, "account_move__expense_sheet_id_index").HasFilter("(expense_sheet_id IS NOT NULL)");

            entity.HasIndex(e => e.InvoiceDateDue, "account_move__invoice_date_due_index");

            entity.HasIndex(e => e.InvoiceDate, "account_move__invoice_date_index");

            entity.HasIndex(e => e.MediumId, "account_move__medium_id_index").HasFilter("(medium_id IS NOT NULL)");

            entity.HasIndex(e => e.MoveType, "account_move__move_type_index");

            entity.HasIndex(e => e.Name, "account_move__name_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.PartnerId, "account_move__partner_id_index");

            entity.HasIndex(e => e.PaymentId, "account_move__payment_id_index").HasFilter("(payment_id IS NOT NULL)");

            entity.HasIndex(e => e.PaymentReference, "account_move__payment_reference_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.ReceiptPaymentId, "account_move__receipt_payment_id_index");

            entity.HasIndex(e => e.Ref, "account_move__ref_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.ReversedEntryId, "account_move__reversed_entry_id_index").HasFilter("(reversed_entry_id IS NOT NULL)");

            entity.HasIndex(e => e.SecureSequenceNumber, "account_move__secure_sequence_number_index");

            entity.HasIndex(e => e.SourceId, "account_move__source_id_index").HasFilter("(source_id IS NOT NULL)");

            entity.HasIndex(e => e.StatementLineId, "account_move__statement_line_id_index").HasFilter("(statement_line_id IS NOT NULL)");

            entity.HasIndex(e => e.StockMoveId, "account_move__stock_move_id_index").HasFilter("(stock_move_id IS NOT NULL)");

            entity.HasIndex(e => e.TaxCashBasisOriginMoveId, "account_move__tax_cash_basis_origin_move_id_index").HasFilter("(tax_cash_basis_origin_move_id IS NOT NULL)");

            entity.HasIndex(e => e.TaxCashBasisRecId, "account_move__tax_cash_basis_rec_id_index").HasFilter("(tax_cash_basis_rec_id IS NOT NULL)");

            entity.HasIndex(e => new { e.JournalId, e.CompanyId, e.Date }, "account_move_journal_id_company_id_idx");

            entity.HasIndex(e => new { e.JournalId, e.State, e.PaymentState, e.MoveType, e.Date }, "account_move_payment_idx");

            entity.HasIndex(e => new { e.JournalId, e.SequencePrefix, e.SequenceNumber, e.Name }, "account_move_sequence_index").IsDescending(false, true, true, false);

            entity.HasIndex(e => new { e.JournalId, e.Id, e.SequencePrefix }, "account_move_sequence_index2").IsDescending(false, true, false);

            entity.HasIndex(e => e.JournalId, "account_move_to_check_idx").HasFilter("(to_check = true)");

            entity.HasIndex(e => new { e.Name, e.JournalId, e.CompanyId }, "account_move_unique_name")
                .IsUnique()
                .HasFilter("((name)::text <> '/'::text)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessToken)
                .HasComment("Security Token")
                .HasColumnType("character varying")
                .HasColumnName("access_token");
            entity.Property(e => e.AlwaysTaxExigible)
                .HasComment("Always Tax Exigible")
                .HasColumnName("always_tax_exigible");
            entity.Property(e => e.AmountResidual)
                .HasComment("Amount Due")
                .HasColumnName("amount_residual");
            entity.Property(e => e.AmountResidualPositive)
                .HasComment("Dummy Amount Due (Positive Value)")
                .HasColumnName("amount_residual_positive");
            entity.Property(e => e.AmountResidualSigned)
                .HasComment("Amount Due Signed")
                .HasColumnName("amount_residual_signed");
            entity.Property(e => e.AmountTax)
                .HasComment("Tax")
                .HasColumnName("amount_tax");
            entity.Property(e => e.AmountTaxSigned)
                .HasComment("Tax Signed")
                .HasColumnName("amount_tax_signed");
            entity.Property(e => e.AmountTotal)
                .HasComment("Total")
                .HasColumnName("amount_total");
            entity.Property(e => e.AmountTotalInCurrencySigned)
                .HasComment("Total in Currency Signed")
                .HasColumnName("amount_total_in_currency_signed");
            entity.Property(e => e.AmountTotalPositive)
                .HasComment("Dummy Total (Positive Value)")
                .HasColumnName("amount_total_positive");
            entity.Property(e => e.AmountTotalSigned)
                .HasComment("Total Signed")
                .HasColumnName("amount_total_signed");
            entity.Property(e => e.AmountUntaxed)
                .HasComment("Untaxed Amount")
                .HasColumnName("amount_untaxed");
            entity.Property(e => e.AmountUntaxedPositive)
                .HasComment("Dummy Untaxed Amount Signed (Positive Value)")
                .HasColumnName("amount_untaxed_positive");
            entity.Property(e => e.AmountUntaxedSigned)
                .HasComment("Untaxed Amount Signed")
                .HasColumnName("amount_untaxed_signed");
            entity.Property(e => e.AnalyticAccountId)
                .HasComment("Analytic Account")
                .HasColumnName("analytic_account_id");
            entity.Property(e => e.AssetDepreciatedValue)
                .HasComment("Cumulative Depreciation")
                .HasColumnName("asset_depreciated_value");
            entity.Property(e => e.AssetDepreciationBeginningDate)
                .HasComment("Date of the beginning of the depreciation")
                .HasColumnName("asset_depreciation_beginning_date");
            entity.Property(e => e.AssetId)
                .HasComment("Asset")
                .HasColumnName("asset_id");
            entity.Property(e => e.AssetNumberDays)
                .HasComment("Number of days")
                .HasColumnName("asset_number_days");
            entity.Property(e => e.AssetRemainingValue)
                .HasComment("Depreciable Value")
                .HasColumnName("asset_remaining_value");
            entity.Property(e => e.AssetValue)
                .HasComment("Asset Value")
                .HasColumnName("asset_value");
            entity.Property(e => e.AssetValueChange)
                .HasComment("Asset Value Change")
                .HasColumnName("asset_value_change");
            entity.Property(e => e.AutoGenerated)
                .HasComment("Auto Generated Document")
                .HasColumnName("auto_generated");
            entity.Property(e => e.AutoInvoiceId)
                .HasComment("Source Invoice")
                .HasColumnName("auto_invoice_id");
            entity.Property(e => e.AutoPost)
                .HasComment("Auto-post")
                .HasColumnType("character varying")
                .HasColumnName("auto_post");
            entity.Property(e => e.AutoPostOriginId)
                .HasComment("First recurring entry")
                .HasColumnName("auto_post_origin_id");
            entity.Property(e => e.AutoPostUntil)
                .HasComment("Auto-post until")
                .HasColumnName("auto_post_until");
            entity.Property(e => e.BuyerNameVatInvoice)
                .HasComment("Buyer Name on VAT Invoice")
                .HasColumnType("character varying")
                .HasColumnName("buyer_name_vat_invoice");
            entity.Property(e => e.CampaignId)
                .HasComment("Campaign")
                .HasColumnName("campaign_id");
            entity.Property(e => e.CommercialPartnerId)
                .HasComment("Commercial Entity")
                .HasColumnName("commercial_partner_id");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.ConfirmMethod)
                .HasComment("Confirm Method")
                .HasColumnName("confirm_method");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.CurrencyId)
                .HasComment("Currency")
                .HasColumnName("currency_id");
            entity.Property(e => e.CurrencyRate)
                .HasComment("Currency Rate")
                .HasColumnName("currency_rate");
            entity.Property(e => e.CustomerPaymentFeeId)
                .HasComment("Related Customer Payment Fee Register")
                .HasColumnName("customer_payment_fee_id");
            entity.Property(e => e.Date)
                .HasComment("Date")
                .HasColumnName("date");
            entity.Property(e => e.DeliveryDate)
                .HasComment("Delivery Date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DeliveryNote)
                .HasComment("Delivery Note")
                .HasColumnName("delivery_note");
            entity.Property(e => e.DeliveryOrder)
                .HasComment("Delivery Order")
                .HasColumnType("character varying")
                .HasColumnName("delivery_order");
            entity.Property(e => e.DepreciationValue)
                .HasComment("Depreciation")
                .HasColumnName("depreciation_value");
            entity.Property(e => e.DestCompanyId)
                .HasComment("Company Dest")
                .HasColumnName("dest_company_id");
            entity.Property(e => e.ExpenseId)
                .HasComment("Expense")
                .HasColumnName("expense_id");
            entity.Property(e => e.ExpenseSheetId)
                .HasComment("Expense Sheet")
                .HasColumnName("expense_sheet_id");
            entity.Property(e => e.Failed)
                .HasComment("Failed")
                .HasColumnName("failed");
            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .HasComment("Filename")
                .HasColumnName("file_name");
            entity.Property(e => e.FiscalPositionId)
                .HasComment("Fiscal Position")
                .HasColumnName("fiscal_position_id");
            entity.Property(e => e.FixedAmountDiscount)
                .HasComment("Fixed Amount Discount")
                .HasColumnName("fixed_amount_discount");
            entity.Property(e => e.InalterableHash)
                .HasComment("Inalterability Hash")
                .HasColumnType("character varying")
                .HasColumnName("inalterable_hash");
            entity.Property(e => e.IncotermLocation)
                .HasComment("Incoterm Location")
                .HasColumnType("character varying")
                .HasColumnName("incoterm_location");
            entity.Property(e => e.InvoiceCashRoundingId)
                .HasComment("Cash Rounding Method")
                .HasColumnName("invoice_cash_rounding_id");
            entity.Property(e => e.InvoiceDate)
                .HasComment("Invoice/Bill Date")
                .HasColumnName("invoice_date");
            entity.Property(e => e.InvoiceDateDue)
                .HasComment("Due Date")
                .HasColumnName("invoice_date_due");
            entity.Property(e => e.InvoiceIncotermId)
                .HasComment("Incoterm")
                .HasColumnName("invoice_incoterm_id");
            entity.Property(e => e.InvoiceNumber)
                .HasComment("Invoice Number")
                .HasColumnType("character varying")
                .HasColumnName("invoice_number");
            entity.Property(e => e.InvoiceOrigin)
                .HasComment("Origin")
                .HasColumnType("character varying")
                .HasColumnName("invoice_origin");
            entity.Property(e => e.InvoicePartnerDisplayName)
                .HasComment("Invoice Partner Display Name")
                .HasColumnType("character varying")
                .HasColumnName("invoice_partner_display_name");
            entity.Property(e => e.InvoicePaymentTermId)
                .HasComment("Payment Terms")
                .HasColumnName("invoice_payment_term_id");
            entity.Property(e => e.InvoiceReference)
                .HasComment("Invoice Reference")
                .HasColumnType("character varying")
                .HasColumnName("invoice_reference");
            entity.Property(e => e.InvoiceSourceEmail)
                .HasComment("Source Email")
                .HasColumnType("character varying")
                .HasColumnName("invoice_source_email");
            entity.Property(e => e.InvoiceUserId)
                .HasComment("Salesperson")
                .HasColumnName("invoice_user_id");
            entity.Property(e => e.IsAssetDisposeEntry)
                .HasComment("Is Asset Dispose Entry")
                .HasColumnName("is_asset_dispose_entry");
            entity.Property(e => e.IsAssetPauseEntry)
                .HasComment("Is Asset Pause Entry")
                .HasColumnName("is_asset_pause_entry");
            entity.Property(e => e.IsDownpayment)
                .HasComment("Is Downpayment")
                .HasColumnName("is_downpayment");
            entity.Property(e => e.IsImportInvoice)
                .HasComment("Is import invoice ?")
                .HasColumnName("is_import_invoice");
            entity.Property(e => e.IsInterCompanyTransaction)
                .HasComment("Is Inter-company Transaction ?")
                .HasColumnName("is_inter_company_transaction");
            entity.Property(e => e.IsIssuedVat)
                .HasComment("Issued VAT")
                .HasColumnName("is_issued_vat");
            entity.Property(e => e.IsMoveSent)
                .HasComment("Is Move Sent")
                .HasColumnName("is_move_sent");
            entity.Property(e => e.IsNeedApi)
                .HasComment("Passed API")
                .HasColumnName("is_need_api");
            entity.Property(e => e.IsNeedIssueVat)
                .HasComment("Need Issue VAT")
                .HasColumnName("is_need_issue_vat");
            entity.Property(e => e.IsStorno)
                .HasComment("Is Storno")
                .HasColumnName("is_storno");
            entity.Property(e => e.IssueCount)
                .HasComment("Issue VAT Invoice Count")
                .HasColumnName("issue_count");
            entity.Property(e => e.IssueState)
                .HasComment("Issue Status")
                .HasColumnType("character varying")
                .HasColumnName("issue_state");
            entity.Property(e => e.IssueVatDate)
                .HasComment("Issue VAT Date")
                .HasColumnName("issue_vat_date");
            entity.Property(e => e.JournalId)
                .HasComment("Journal")
                .HasColumnName("journal_id");
            entity.Property(e => e.LogFailed)
                .HasComment("Log Failed")
                .HasColumnName("log_failed");
            entity.Property(e => e.MediumId)
                .HasComment("Medium")
                .HasColumnName("medium_id");
            entity.Property(e => e.MessageMainAttachmentId)
                .HasComment("Main Attachment")
                .HasColumnName("message_main_attachment_id");
            entity.Property(e => e.MoveType)
                .HasComment("Type")
                .HasColumnType("character varying")
                .HasColumnName("move_type");
            entity.Property(e => e.Name)
                .HasComment("Number")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Narration)
                .HasComment("Terms and Conditions")
                .HasColumnName("narration");
            entity.Property(e => e.PartnerBankId)
                .HasComment("Recipient Bank")
                .HasColumnName("partner_bank_id");
            entity.Property(e => e.PartnerId)
                .HasComment("Partner")
                .HasColumnName("partner_id");
            entity.Property(e => e.PartnerInvoiceId)
                .HasComment("VAT Partner")
                .HasColumnName("partner_invoice_id");
            entity.Property(e => e.PartnerShippingId)
                .HasComment("Delivery Address")
                .HasColumnName("partner_shipping_id");
            entity.Property(e => e.PaymentId)
                .HasComment("Payment")
                .HasColumnName("payment_id");
            entity.Property(e => e.PaymentReference)
                .HasComment("Payment Reference")
                .HasColumnType("character varying")
                .HasColumnName("payment_reference");
            entity.Property(e => e.PaymentState)
                .HasComment("Payment Status")
                .HasColumnType("character varying")
                .HasColumnName("payment_state");
            entity.Property(e => e.PaymentStateBeforeSwitch)
                .HasComment("Payment State Before Switch")
                .HasColumnType("character varying")
                .HasColumnName("payment_state_before_switch");
            entity.Property(e => e.Platform)
                .HasComment("Platform?")
                .HasColumnName("platform");
            entity.Property(e => e.PostedBefore)
                .HasComment("Posted Before")
                .HasColumnName("posted_before");
            entity.Property(e => e.QrCodeMethod)
                .HasComment("Payment QR-code")
                .HasColumnType("character varying")
                .HasColumnName("qr_code_method");
            entity.Property(e => e.QuickEditTotalAmount)
                .HasComment("Total (Tax inc.)")
                .HasColumnName("quick_edit_total_amount");
            entity.Property(e => e.RawDataId)
                .HasComment("Raw Data ID")
                .HasColumnName("raw_data_id");
            entity.Property(e => e.ReceiptPaymentId)
                .HasComment("Related Receipt Payment Register")
                .HasColumnName("receipt_payment_id");
            entity.Property(e => e.Ref)
                .HasComment("Reference")
                .HasColumnType("character varying")
                .HasColumnName("ref");
            entity.Property(e => e.RegularizationId)
                .HasComment("Regularization")
                .HasColumnName("regularization_id");
            entity.Property(e => e.ReversedEntryId)
                .HasComment("Reversal of")
                .HasColumnName("reversed_entry_id");
            entity.Property(e => e.SaleId)
                .HasComment("Sale Order")
                .HasColumnName("sale_id");
            entity.Property(e => e.SecureSequenceNumber)
                .HasComment("Inalteralbility No Gap Sequence #")
                .HasColumnName("secure_sequence_number");
            entity.Property(e => e.SendAndPrintValues)
                .HasComment("Send And Print Values")
                .HasColumnType("jsonb")
                .HasColumnName("send_and_print_values");
            entity.Property(e => e.SequenceNumber)
                .HasComment("Sequence Number")
                .HasColumnName("sequence_number");
            entity.Property(e => e.SequencePrefix)
                .HasComment("Sequence Prefix")
                .HasColumnType("character varying")
                .HasColumnName("sequence_prefix");
            entity.Property(e => e.SkipStockConstraint)
                .HasComment("Skip Periodical Cost Computation Constraint")
                .HasColumnName("skip_stock_constraint");
            entity.Property(e => e.SoTypeId)
                .HasComment("SO Type")
                .HasColumnName("so_type_id");
            entity.Property(e => e.SourceDocument)
                .HasComment("Source Document")
                .HasColumnType("character varying")
                .HasColumnName("source_document");
            entity.Property(e => e.SourceId)
                .HasComment("Source")
                .HasColumnName("source_id");
            entity.Property(e => e.State)
                .HasComment("Status")
                .HasColumnType("character varying")
                .HasColumnName("state");
            entity.Property(e => e.StatementLineId)
                .HasComment("Statement Line")
                .HasColumnName("statement_line_id");
            entity.Property(e => e.StockMoveId)
                .HasComment("Stock Move")
                .HasColumnName("stock_move_id");
            entity.Property(e => e.TaxCashBasisOriginMoveId)
                .HasComment("Cash Basis Origin")
                .HasColumnName("tax_cash_basis_origin_move_id");
            entity.Property(e => e.TaxCashBasisRecId)
                .HasComment("Tax Cash Basis Entry of")
                .HasColumnName("tax_cash_basis_rec_id");
            entity.Property(e => e.TaxClosingEndDate)
                .HasComment("Tax Closing End Date")
                .HasColumnName("tax_closing_end_date");
            entity.Property(e => e.TaxReportControlError)
                .HasComment("Tax Report Control Error")
                .HasColumnName("tax_report_control_error");
            entity.Property(e => e.TeamId)
                .HasComment("Sales Team")
                .HasColumnName("team_id");
            entity.Property(e => e.ToCheck)
                .HasComment("To Check")
                .HasColumnName("to_check");
            entity.Property(e => e.TotalAmountDiscount)
                .HasComment("Total Amount Discount")
                .HasColumnName("total_amount_discount");
            entity.Property(e => e.TotalDiscount)
                .HasComment("Total Discount (%)")
                .HasColumnName("total_discount");
            entity.Property(e => e.TotalFixAmountDiscount)
                .HasComment("Total Fix Amount Discount")
                .HasColumnName("total_fix_amount_discount");
            entity.Property(e => e.TransferModelId)
                .HasComment("Originating Model")
                .HasColumnName("transfer_model_id");
            entity.Property(e => e.VatInvoiceStatus)
                .HasComment("VAT Invoice Status")
                .HasColumnType("character varying")
                .HasColumnName("vat_invoice_status");
            entity.Property(e => e.VatInvoiceType)
                .HasComment("VAT Invoice Type")
                .HasColumnType("character varying")
                .HasColumnName("vat_invoice_type");
            entity.Property(e => e.VatPartnerId)
                .HasComment("Vat Partner")
                .HasColumnName("vat_partner_id");
            entity.Property(e => e.VendorTaxCode)
                .HasComment("Vendor Tax Code")
                .HasColumnType("character varying")
                .HasColumnName("vendor_tax_code");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.AnalyticAccount).WithMany(p => p.AccountMoves)
                .HasForeignKey(d => d.AnalyticAccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_analytic_account_id_fkey");

            entity.HasOne(d => d.AutoInvoice).WithMany(p => p.InverseAutoInvoice)
                .HasForeignKey(d => d.AutoInvoiceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_auto_invoice_id_fkey");

            entity.HasOne(d => d.AutoPostOrigin).WithMany(p => p.InverseAutoPostOrigin)
                .HasForeignKey(d => d.AutoPostOriginId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_auto_post_origin_id_fkey");

            entity.HasOne(d => d.CommercialPartner).WithMany(p => p.AccountMoveCommercialPartners)
                .HasForeignKey(d => d.CommercialPartnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_move_commercial_partner_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.AccountMoveCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_create_uid_fkey");

            entity.HasOne(d => d.CustomerPaymentFee).WithMany(p => p.AccountMoveCustomerPaymentFees)
                .HasForeignKey(d => d.CustomerPaymentFeeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("account_move_customer_payment_fee_id_fkey");

            entity.HasOne(d => d.InvoiceUser).WithMany(p => p.AccountMoveInvoiceUsers)
                .HasForeignKey(d => d.InvoiceUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_invoice_user_id_fkey");

            entity.HasOne(d => d.Journal).WithMany(p => p.AccountMoves)
                .HasForeignKey(d => d.JournalId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_move_journal_id_fkey");

            entity.HasOne(d => d.Partner).WithMany(p => p.AccountMovePartners)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_move_partner_id_fkey");

            entity.HasOne(d => d.PartnerInvoice).WithMany(p => p.AccountMovePartnerInvoices)
                .HasForeignKey(d => d.PartnerInvoiceId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_partner_invoice_id_fkey");

            entity.HasOne(d => d.PartnerShipping).WithMany(p => p.AccountMovePartnerShippings)
                .HasForeignKey(d => d.PartnerShippingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_partner_shipping_id_fkey");

            entity.HasOne(d => d.Payment).WithMany(p => p.AccountMovePayments)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_payment_id_fkey");

            entity.HasOne(d => d.ReceiptPayment).WithMany(p => p.AccountMoveReceiptPayments)
                .HasForeignKey(d => d.ReceiptPaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("account_move_receipt_payment_id_fkey");

            entity.HasOne(d => d.ReversedEntry).WithMany(p => p.InverseReversedEntry)
                .HasForeignKey(d => d.ReversedEntryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_reversed_entry_id_fkey");

            entity.HasOne(d => d.Sale).WithMany(p => p.AccountMoves)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_sale_id_fkey");

            entity.HasOne(d => d.TaxCashBasisOriginMove).WithMany(p => p.InverseTaxCashBasisOriginMove)
                .HasForeignKey(d => d.TaxCashBasisOriginMoveId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_tax_cash_basis_origin_move_id_fkey");

            entity.HasOne(d => d.VatPartner).WithMany(p => p.AccountMoveVatPartners)
                .HasForeignKey(d => d.VatPartnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_vat_partner_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.AccountMoveWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_move_write_uid_fkey");
        });

        modelBuilder.Entity<AccountPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("account_payment_pkey");

            entity.ToTable("account_payment", tb => tb.HasComment("Payments"));

            entity.HasIndex(e => e.AnalyticAccountId, "account_payment__analytic_account_id_index");

            entity.HasIndex(e => e.MoveId, "account_payment__move_id_index");

            entity.HasIndex(e => e.OutstandingAccountId, "account_payment__outstanding_account_id_index").HasFilter("(outstanding_account_id IS NOT NULL)");

            entity.HasIndex(e => e.PairedInternalTransferPaymentId, "account_payment__paired_internal_transfer_payment_id_index").HasFilter("(paired_internal_transfer_payment_id IS NOT NULL)");

            entity.HasIndex(e => e.SourcePaymentId, "account_payment__source_payment_id_index").HasFilter("(source_payment_id IS NOT NULL)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasComment("Amount")
                .HasColumnName("amount");
            entity.Property(e => e.AmountCompanyCurrencySigned)
                .HasComment("Amount Company Currency Signed")
                .HasColumnName("amount_company_currency_signed");
            entity.Property(e => e.AnalyticAccountId)
                .HasComment("Analytic Account")
                .HasColumnName("analytic_account_id");
            entity.Property(e => e.CashbackPayableAccountId)
                .HasComment("Cashback Payable Account")
                .HasColumnName("cashback_payable_account_id");
            entity.Property(e => e.CompanyCurrencyId)
                .HasComment("Company Currency")
                .HasColumnName("company_currency_id");
            entity.Property(e => e.ConversionCurrencyAmount)
                .HasComment("Conversion Amount")
                .HasColumnName("conversion_currency_amount");
            entity.Property(e => e.ConvertedAmountAtInvoiceRate)
                .HasComment("Amount Converted at Invoice Rate")
                .HasColumnName("converted_amount_at_invoice_rate");
            entity.Property(e => e.ConvertedAmountAtPaymentRate)
                .HasComment("Amount Converted at Payment Rate")
                .HasColumnName("converted_amount_at_payment_rate");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.CurrencyId)
                .HasComment("Currency")
                .HasColumnName("currency_id");
            entity.Property(e => e.CurrencyRate)
                .HasComment("Currency Rate")
                .HasColumnName("currency_rate");
            entity.Property(e => e.DestinationAccountId)
                .HasComment("Destination Account")
                .HasColumnName("destination_account_id");
            entity.Property(e => e.DestinationJournalId)
                .HasComment("Destination Journal")
                .HasColumnName("destination_journal_id");
            entity.Property(e => e.ExpensesPaymentId)
                .HasComment("Related Expenses")
                .HasColumnName("expenses_payment_id");
            entity.Property(e => e.InternalReference)
                .HasComment("Internal Reference")
                .HasColumnName("internal_reference");
            entity.Property(e => e.IsInternalTransfer)
                .HasComment("Internal Transfer")
                .HasColumnName("is_internal_transfer");
            entity.Property(e => e.IsMatched)
                .HasComment("Is Matched With a Bank Statement")
                .HasColumnName("is_matched");
            entity.Property(e => e.IsNeedApi)
                .HasComment("Need API")
                .HasColumnName("is_need_api");
            entity.Property(e => e.IsPassedApi)
                .HasComment("Passed API")
                .HasColumnName("is_passed_api");
            entity.Property(e => e.IsReconciled)
                .HasComment("Is Reconciled")
                .HasColumnName("is_reconciled");
            entity.Property(e => e.JournalId)
                .HasComment("Journal")
                .HasColumnName("journal_id");
            entity.Property(e => e.MessageMainAttachmentId)
                .HasComment("Main Attachment")
                .HasColumnName("message_main_attachment_id");
            entity.Property(e => e.MoveId)
                .HasComment("Journal Entry")
                .HasColumnName("move_id");
            entity.Property(e => e.OutstandingAccountId)
                .HasComment("Outstanding Account")
                .HasColumnName("outstanding_account_id");
            entity.Property(e => e.PairedInternalTransferPaymentId)
                .HasComment("Paired Internal Transfer Payment")
                .HasColumnName("paired_internal_transfer_payment_id");
            entity.Property(e => e.PartnerBankId)
                .HasComment("Recipient Bank Account")
                .HasColumnName("partner_bank_id");
            entity.Property(e => e.PartnerId)
                .HasComment("Customer/Vendor")
                .HasColumnName("partner_id");
            entity.Property(e => e.PartnerType)
                .HasComment("Partner Type")
                .HasColumnType("character varying")
                .HasColumnName("partner_type");
            entity.Property(e => e.PaymentDifference)
                .HasComment("Payment Difference")
                .HasColumnName("payment_difference");
            entity.Property(e => e.PaymentDifferenceHandling)
                .HasComment("Payment Difference Handling")
                .HasColumnType("character varying")
                .HasColumnName("payment_difference_handling");
            entity.Property(e => e.PaymentMethodId)
                .HasComment("Method")
                .HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentMethodLineId)
                .HasComment("Payment Method")
                .HasColumnName("payment_method_line_id");
            entity.Property(e => e.PaymentReference)
                .HasComment("Payment Reference")
                .HasColumnType("character varying")
                .HasColumnName("payment_reference");
            entity.Property(e => e.PaymentTokenId)
                .HasComment("Saved Payment Token")
                .HasColumnName("payment_token_id");
            entity.Property(e => e.PaymentTransactionId)
                .HasComment("Payment Transaction")
                .HasColumnName("payment_transaction_id");
            entity.Property(e => e.PaymentType)
                .HasComment("Payment Type")
                .HasColumnType("character varying")
                .HasColumnName("payment_type");
            entity.Property(e => e.Platform)
                .HasComment("Platform?")
                .HasColumnName("platform");
            entity.Property(e => e.PoRequestId)
                .HasComment("PO Request")
                .HasColumnName("po_request_id");
            entity.Property(e => e.RealizedGainLossAmount)
                .HasComment("Realized Gain Loss Amount")
                .HasColumnName("realized_gain_loss_amount");
            entity.Property(e => e.RealizedGainLossDate)
                .HasComment("Realized Gain Loss Entry Date")
                .HasColumnName("realized_gain_loss_date");
            entity.Property(e => e.RelatedPartnerReconciliationId)
                .HasComment("Related Partner Reconciliation")
                .HasColumnName("related_partner_reconciliation_id");
            entity.Property(e => e.Responsible)
                .HasComment("Responsible")
                .HasColumnType("character varying")
                .HasColumnName("responsible");
            entity.Property(e => e.SendReceivePerson)
                .HasComment("Send/ Receive Person")
                .HasColumnType("character varying")
                .HasColumnName("send_receive_person");
            entity.Property(e => e.ShowInvoice)
                .HasComment("Show Payment Receipts")
                .HasColumnName("show_invoice");
            entity.Property(e => e.SourcePaymentId)
                .HasComment("Source Payment")
                .HasColumnName("source_payment_id");
            entity.Property(e => e.SystemAmount)
                .HasComment("Other System Amount")
                .HasColumnName("system_amount");
            entity.Property(e => e.TotalPayment)
                .HasComment("Total Amount (Inc. Payment Receipts)")
                .HasColumnName("total_payment");
            entity.Property(e => e.TotalPaymentCurrency)
                .HasComment("Converted Total Amount (Inc. Payment Receipts)")
                .HasColumnName("total_payment_currency");
            entity.Property(e => e.UserId)
                .HasComment("Salesperson")
                .HasColumnName("user_id");
            entity.Property(e => e.V15Type)
                .HasComment("V15 Type")
                .HasColumnType("character varying")
                .HasColumnName("v15_type");
            entity.Property(e => e.VatPartnerId)
                .HasComment("Vat Partner")
                .HasColumnName("vat_partner_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");
            entity.Property(e => e.WriteoffAccountId)
                .HasComment("Difference Account")
                .HasColumnName("writeoff_account_id");
            entity.Property(e => e.WriteoffLabel)
                .HasComment("Journal Item Label")
                .HasColumnType("character varying")
                .HasColumnName("writeoff_label");
            entity.Property(e => e.XHasRequestApproval)
                .HasComment("x_has_request_approval")
                .HasColumnName("x_has_request_approval");
            entity.Property(e => e.XReviewResult)
                .HasComment("x_review_result")
                .HasColumnType("character varying")
                .HasColumnName("x_review_result");

            entity.HasOne(d => d.AnalyticAccount).WithMany(p => p.AccountPayments)
                .HasForeignKey(d => d.AnalyticAccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_analytic_account_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.AccountPaymentCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_create_uid_fkey");

            entity.HasOne(d => d.DestinationJournal).WithMany(p => p.AccountPaymentDestinationJournals)
                .HasForeignKey(d => d.DestinationJournalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_destination_journal_id_fkey");

            entity.HasOne(d => d.Journal).WithMany(p => p.AccountPaymentJournals)
                .HasForeignKey(d => d.JournalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_journal_id_fkey");

            entity.HasOne(d => d.Move).WithMany(p => p.AccountPayments)
                .HasForeignKey(d => d.MoveId)
                .HasConstraintName("account_payment_move_id_fkey");

            entity.HasOne(d => d.PairedInternalTransferPayment).WithMany(p => p.InversePairedInternalTransferPayment)
                .HasForeignKey(d => d.PairedInternalTransferPaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_paired_internal_transfer_payment_id_fkey");

            entity.HasOne(d => d.Partner).WithMany(p => p.AccountPaymentPartners)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("account_payment_partner_id_fkey");

            entity.HasOne(d => d.SourcePayment).WithMany(p => p.InverseSourcePayment)
                .HasForeignKey(d => d.SourcePaymentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_source_payment_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.AccountPaymentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_user_id_fkey");

            entity.HasOne(d => d.VatPartner).WithMany(p => p.AccountPaymentVatPartners)
                .HasForeignKey(d => d.VatPartnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_vat_partner_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.AccountPaymentWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("account_payment_write_uid_fkey");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_category_pkey");

            entity.ToTable("product_category", tb => tb.HasComment("Product Category"));

            entity.HasIndex(e => e.Code, "product_category__code_index");

            entity.HasIndex(e => e.Name, "product_category__name_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.ParentId, "product_category__parent_id_index");

            entity.HasIndex(e => e.ParentPath, "product_category__parent_path_index");

            entity.HasIndex(e => e.Code, "product_category_category_code_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.Code)
                .HasComment("Code")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CompleteName)
                .HasComment("Complete Name")
                .HasColumnType("character varying")
                .HasColumnName("complete_name");
            entity.Property(e => e.ConsignmentStockAccountId)
                .HasComment("Consignment Stock Account")
                .HasColumnName("consignment_stock_account_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.IsExpenseAnalysis)
                .HasComment("Expense Analysis")
                .HasColumnName("is_expense_analysis");
            entity.Property(e => e.Level)
                .HasComment("Level")
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .HasComment("Name")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PackagingReserveMethod)
                .HasComment("Reserve Packagings")
                .HasColumnType("character varying")
                .HasColumnName("packaging_reserve_method");
            entity.Property(e => e.ParentId)
                .HasComment("Parent Category")
                .HasColumnName("parent_id");
            entity.Property(e => e.ParentPath)
                .HasComment("Parent Path")
                .HasColumnType("character varying")
                .HasColumnName("parent_path");
            entity.Property(e => e.ProductPropertiesDefinition)
                .HasComment("Product Properties")
                .HasColumnType("jsonb")
                .HasColumnName("product_properties_definition");
            entity.Property(e => e.PropertySalesReturnsAccountCategId)
                .HasComment("Sales Returns Account")
                .HasColumnName("property_sales_returns_account_categ_id");
            entity.Property(e => e.RemovalStrategyId)
                .HasComment("Force Removal Strategy")
                .HasColumnName("removal_strategy_id");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ProductCategoryCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_category_create_uid_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("product_category_parent_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ProductCategoryWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_category_write_uid_fkey");
        });

        modelBuilder.Entity<ProductProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_product_pkey");

            entity.ToTable("product_product", tb => tb.HasComment("Product Variant"));

            entity.HasIndex(e => e.Barcode, "product_product__barcode_index").HasFilter("(barcode IS NOT NULL)");

            entity.HasIndex(e => e.CombinationIndices, "product_product__combination_indices_index");

            entity.HasIndex(e => e.DefaultCode, "product_product__default_code_index");

            entity.HasIndex(e => e.DisplayName, "product_product__display_name_index");

            entity.HasIndex(e => e.ProductTmplId, "product_product__product_tmpl_id_index");

            entity.HasIndex(e => new { e.ProductTmplId, e.CombinationIndices }, "product_product_combination_unique")
                .IsUnique()
                .HasFilter("(active IS TRUE)");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.Barcode)
                .HasComment("Barcode")
                .HasColumnType("character varying")
                .HasColumnName("barcode");
            entity.Property(e => e.CanImageVariant1024BeZoomed)
                .HasComment("Can Variant Image 1024 be zoomed")
                .HasColumnName("can_image_variant_1024_be_zoomed");
            entity.Property(e => e.CategLv1Id)
                .HasComment("Category 1 ID")
                .HasColumnName("categ_lv1_id");
            entity.Property(e => e.CategLv2Id).HasColumnName("categ_lv2_id");
            entity.Property(e => e.CategoryLv1)
                .HasComment("Category 1")
                .HasColumnType("character varying")
                .HasColumnName("category_lv1");
            entity.Property(e => e.CategoryLv2)
                .HasComment("Category 2")
                .HasColumnType("character varying")
                .HasColumnName("category_lv2");
            entity.Property(e => e.CategoryLv3)
                .HasComment("Category 3")
                .HasColumnType("character varying")
                .HasColumnName("category_lv3");
            entity.Property(e => e.CombinationIndices)
                .HasComment("Combination Indices")
                .HasColumnType("character varying")
                .HasColumnName("combination_indices");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.DefaultCode)
                .HasComment("Internal Reference")
                .HasColumnType("character varying")
                .HasColumnName("default_code");
            entity.Property(e => e.DisplayName)
                .HasComment("Display Name")
                .HasColumnType("character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.ImageValue)
                .HasComment("Image base64")
                .HasColumnName("image_value");
            entity.Property(e => e.IsAccessoriesIncluded)
                .HasComment("Is Accessories Included")
                .HasColumnName("is_accessories_included");
            entity.Property(e => e.IsNeedApi)
                .HasComment("Need API")
                .HasColumnName("is_need_api");
            entity.Property(e => e.LotPropertiesDefinition)
                .HasComment("Lot Properties")
                .HasColumnType("jsonb")
                .HasColumnName("lot_properties_definition");
            entity.Property(e => e.LstPrice)
                .HasComment("Sales Price")
                .HasColumnName("lst_price");
            entity.Property(e => e.ParentCode)
                .HasComment("Parent Code")
                .HasColumnType("character varying")
                .HasColumnName("parent_code");
            entity.Property(e => e.ProductTmplId)
                .HasComment("Product Template")
                .HasColumnName("product_tmpl_id");
            entity.Property(e => e.V15ProductCode)
                .HasComment("V15 Product Code")
                .HasColumnType("character varying")
                .HasColumnName("v15_product_code");
            entity.Property(e => e.Volume)
                .HasComment("Volume")
                .HasColumnName("volume");
            entity.Property(e => e.Weight)
                .HasComment("Weight")
                .HasColumnName("weight");
            entity.Property(e => e.WriteDate)
                .HasComment("Write Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CategLv1).WithMany(p => p.ProductProductCategLv1s)
                .HasForeignKey(d => d.CategLv1Id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_product_categ_lv1_id_fkey");

            entity.HasOne(d => d.CategLv2).WithMany(p => p.ProductProductCategLv2s)
                .HasForeignKey(d => d.CategLv2Id)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_product_categ_lv2_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ProductProductCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_product_create_uid_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ProductProductWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_product_write_uid_fkey");
        });

        modelBuilder.Entity<ReportSaleOrderView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("report_sale_order_view");

            entity.Property(e => e.AmountPaidInvoice).HasColumnName("amount_paid_invoice");
            entity.Property(e => e.AmountTotalInvoice).HasColumnName("amount_total_invoice");
            entity.Property(e => e.Channel)
                .HasColumnType("character varying")
                .HasColumnName("channel");
            entity.Property(e => e.ClientOrderRef)
                .HasColumnType("character varying")
                .HasColumnName("client_order_ref");
            entity.Property(e => e.CompanyAnalyticAccount)
                .HasColumnType("character varying")
                .HasColumnName("company_analytic_account");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasColumnType("character varying")
                .HasColumnName("customer_name");
            entity.Property(e => e.DateDones)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_dones");
            entity.Property(e => e.DateDues).HasColumnName("date_dues");
            entity.Property(e => e.DateInvoices).HasColumnName("date_invoices");
            entity.Property(e => e.DateOrder).HasColumnName("date_order");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InternalRef)
                .HasColumnType("character varying")
                .HasColumnName("internal_ref");
            entity.Property(e => e.InvoiceRef).HasColumnName("invoice_ref");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderName)
                .HasColumnType("character varying")
                .HasColumnName("order_name");
            entity.Property(e => e.PaymentDueDate).HasColumnName("payment_due_date");
            entity.Property(e => e.PaymentTerm).HasColumnName("payment_term");
            entity.Property(e => e.PickingStatus)
                .HasColumnType("character varying")
                .HasColumnName("picking_status");
            entity.Property(e => e.Responsible)
                .HasColumnType("character varying")
                .HasColumnName("responsible");
            entity.Property(e => e.Salesperson)
                .HasColumnType("character varying")
                .HasColumnName("salesperson");
            entity.Property(e => e.SalespersonId).HasColumnName("salesperson_id");
            entity.Property(e => e.ShippingType)
                .HasColumnType("character varying")
                .HasColumnName("shipping_type");
            entity.Property(e => e.SoReferenceNumber)
                .HasColumnType("character varying")
                .HasColumnName("so_reference_number");
            entity.Property(e => e.SoType)
                .HasColumnType("character varying")
                .HasColumnName("so_type");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("state");
            entity.Property(e => e.StateName).HasColumnName("state_name");
            entity.Property(e => e.Team).HasColumnName("team");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.Warehouse)
                .HasColumnType("character varying")
                .HasColumnName("warehouse");
            entity.Property(e => e.WriteDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
        });

        modelBuilder.Entity<ResCountryState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("res_country_state_pkey");

            entity.ToTable("res_country_state", tb => tb.HasComment("Country state"));

            entity.HasIndex(e => new { e.CountryId, e.Code }, "res_country_state_name_code_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaId)
                .HasComment("Area")
                .HasColumnName("area_id");
            entity.Property(e => e.Code)
                .HasComment("State Code")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CountryId)
                .HasComment("Country")
                .HasColumnName("country_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.Name)
                .HasComment("State Name")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ResCountryStateCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_country_state_create_uid_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ResCountryStateWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_country_state_write_uid_fkey");
        });

        modelBuilder.Entity<ResDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("res_district_pkey");

            entity.ToTable("res_district", tb => tb.HasComment("District"));

            entity.HasIndex(e => e.Code, "res_district_district_code_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasComment("Code")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CountryId)
                .HasComment("Country")
                .HasColumnName("country_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.Name)
                .HasComment("Name")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.StateId)
                .HasComment("Country State")
                .HasColumnName("state_id");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ResDistrictCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_district_create_uid_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.ResDistricts)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("res_district_state_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ResDistrictWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_district_write_uid_fkey");
        });

        modelBuilder.Entity<ResPartner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("res_partner_pkey");

            entity.ToTable("res_partner");

            entity.HasIndex(e => e.CommercialPartnerId, "res_partner__commercial_partner_id_index");

            entity.HasIndex(e => e.CompanyId, "res_partner__company_id_index");

            entity.HasIndex(e => e.CompleteName, "res_partner__complete_name_index");

            entity.HasIndex(e => e.Date, "res_partner__date_index");

            entity.HasIndex(e => e.DisplayName, "res_partner__display_name_index");

            entity.HasIndex(e => e.Name, "res_partner__name_index");

            entity.HasIndex(e => e.ParentId, "res_partner__parent_id_index");

            entity.HasIndex(e => e.Ref, "res_partner__ref_index");

            entity.HasIndex(e => e.Vat, "res_partner__vat_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.AgencyEmail)
                .HasComment("Agency's email")
                .HasColumnType("character varying")
                .HasColumnName("agency_email");
            entity.Property(e => e.AgencyMobile)
                .HasComment("Agency's Mobile")
                .HasColumnType("character varying")
                .HasColumnName("agency_mobile");
            entity.Property(e => e.AgencyName)
                .HasComment("Agency's Name")
                .HasColumnType("character varying")
                .HasColumnName("agency_name");
            entity.Property(e => e.AreaId)
                .HasComment("Area")
                .HasColumnName("area_id");
            entity.Property(e => e.Birthday)
                .HasComment("Date of Birth")
                .HasColumnName("birthday");
            entity.Property(e => e.BusinessRegistrations)
                .HasComment("Business registration")
                .HasColumnType("character varying")
                .HasColumnName("business_registrations");
            entity.Property(e => e.BuyerEmail)
                .HasComment("Buyer's Email")
                .HasColumnType("character varying")
                .HasColumnName("buyer_email");
            entity.Property(e => e.BuyerId)
                .HasComment("Buyer")
                .HasColumnName("buyer_id");
            entity.Property(e => e.BuyerMobile)
                .HasComment("Buyer's Mobile")
                .HasColumnType("character varying")
                .HasColumnName("buyer_mobile");
            entity.Property(e => e.BuyerName)
                .HasComment("Buyer's Name")
                .HasColumnType("character varying")
                .HasColumnName("buyer_name");
            entity.Property(e => e.CalendarLastNotifAck)
                .HasComment("Last notification marked as read from base Calendar")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("calendar_last_notif_ack");
            entity.Property(e => e.CarCompanyIds)
                .HasComment("Car Company")
                .HasColumnName("car_company_ids");
            entity.Property(e => e.ChannelId)
                .HasComment("Channel")
                .HasColumnName("channel_id");
            entity.Property(e => e.City)
                .HasComment("City")
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.Color)
                .HasComment("Color Index")
                .HasColumnName("color");
            entity.Property(e => e.Comment)
                .HasComment("Notes")
                .HasColumnName("comment");
            entity.Property(e => e.CommercialCompanyName)
                .HasComment("Company Name Entity")
                .HasColumnType("character varying")
                .HasColumnName("commercial_company_name");
            entity.Property(e => e.CommercialPartnerId)
                .HasComment("Commercial Entity")
                .HasColumnName("commercial_partner_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CompanyName)
                .HasComment("Company Name")
                .HasColumnType("character varying")
                .HasColumnName("company_name");
            entity.Property(e => e.CompanyRegistry)
                .HasComment("Company ID")
                .HasColumnType("character varying")
                .HasColumnName("company_registry");
            entity.Property(e => e.CompleteName)
                .HasComment("Complete Name")
                .HasColumnType("character varying")
                .HasColumnName("complete_name");
            entity.Property(e => e.ContactAddressComplete)
                .HasComment("Contact Address Complete")
                .HasColumnType("character varying")
                .HasColumnName("contact_address_complete");
            entity.Property(e => e.CountryId)
                .HasComment("Country")
                .HasColumnName("country_id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.CreditAmount)
                .HasComment("Credit Amount")
                .HasColumnName("credit_amount");
            entity.Property(e => e.CreditLimitAmount)
                .HasComment("Credit Limit")
                .HasColumnName("credit_limit_amount");
            entity.Property(e => e.CustomerRank)
                .HasComment("Customer Rank")
                .HasColumnName("customer_rank");
            entity.Property(e => e.Date)
                .HasComment("Date")
                .HasColumnName("date");
            entity.Property(e => e.DebitLimit)
                .HasComment("Payable Limit")
                .HasColumnName("debit_limit");
            entity.Property(e => e.DebitLimitAmount)
                .HasComment("Debit Limit")
                .HasColumnName("debit_limit_amount");
            entity.Property(e => e.DisplayName)
                .HasComment("Display Name")
                .HasColumnType("character varying")
                .HasColumnName("display_name");
            entity.Property(e => e.DistrictId)
                .HasComment("District")
                .HasColumnName("district_id");
            entity.Property(e => e.Email)
                .HasComment("Email")
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.EmailNormalized)
                .HasComment("Normalized Email")
                .HasColumnType("character varying")
                .HasColumnName("email_normalized");
            entity.Property(e => e.Employee)
                .HasComment("Employee")
                .HasColumnName("employee");
            entity.Property(e => e.FollowupReminderType)
                .HasComment("Reminders")
                .HasColumnType("character varying")
                .HasColumnName("followup_reminder_type");
            entity.Property(e => e.Function)
                .HasComment("Job Position")
                .HasColumnType("character varying")
                .HasColumnName("function");
            entity.Property(e => e.Gender)
                .HasComment("Gender")
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.IdentificationId)
                .HasComment("Identification No")
                .HasColumnType("character varying")
                .HasColumnName("identification_id");
            entity.Property(e => e.IndustryId)
                .HasComment("Industry")
                .HasColumnName("industry_id");
            entity.Property(e => e.InvoiceWarn)
                .HasComment("Invoice")
                .HasColumnType("character varying")
                .HasColumnName("invoice_warn");
            entity.Property(e => e.InvoiceWarnMsg)
                .HasComment("Message for Invoice")
                .HasColumnName("invoice_warn_msg");
            entity.Property(e => e.IsCompany)
                .HasComment("Is a Company")
                .HasColumnName("is_company");
            entity.Property(e => e.IsCustomer)
                .HasComment("Customer")
                .HasColumnName("is_customer");
            entity.Property(e => e.IsNeedApi)
                .HasComment("Is need API")
                .HasColumnName("is_need_api");
            entity.Property(e => e.IsVendor)
                .HasComment("Vendor")
                .HasColumnName("is_vendor");
            entity.Property(e => e.Lang)
                .HasComment("Language")
                .HasColumnType("character varying")
                .HasColumnName("lang");
            entity.Property(e => e.LastTimeEntriesChecked)
                .HasComment("Latest Invoices & Payments Matching Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_time_entries_checked");
            entity.Property(e => e.Login)
                .HasComment("Login")
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.MessageBounce)
                .HasComment("Bounce")
                .HasColumnName("message_bounce");
            entity.Property(e => e.Mobile)
                .HasComment("Mobile")
                .HasColumnType("character varying")
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.OcnToken)
                .HasComment("OCN Token")
                .HasColumnType("character varying")
                .HasColumnName("ocn_token");
            entity.Property(e => e.OnlinePartnerInformation)
                .HasComment("Online Partner Information")
                .HasColumnType("character varying")
                .HasColumnName("online_partner_information");
            entity.Property(e => e.OptOut)
                .HasComment("Opt-Out")
                .HasColumnName("opt_out");
            entity.Property(e => e.ParentId)
                .HasComment("Related Company")
                .HasColumnName("parent_id");
            entity.Property(e => e.PartnerGroupId)
                .HasComment("Res Partner Group")
                .HasColumnName("partner_group_id");
            entity.Property(e => e.PartnerLatitude)
                .HasComment("Geo Latitude")
                .HasColumnName("partner_latitude");
            entity.Property(e => e.PartnerLongitude)
                .HasComment("Geo Longitude")
                .HasColumnName("partner_longitude");
            entity.Property(e => e.PartnerShare)
                .HasComment("Share Partner")
                .HasColumnName("partner_share");
            entity.Property(e => e.PayerEmail)
                .HasComment("Payer's Email")
                .HasColumnType("character varying")
                .HasColumnName("payer_email");
            entity.Property(e => e.PayerMobile)
                .HasComment("Payer's Mobile")
                .HasColumnType("character varying")
                .HasColumnName("payer_mobile");
            entity.Property(e => e.PayerName)
                .HasComment("Payer's Name")
                .HasColumnType("character varying")
                .HasColumnName("payer_name");
            entity.Property(e => e.PeppolEas)
                .HasComment("Peppol e-address (EAS)")
                .HasColumnType("character varying")
                .HasColumnName("peppol_eas");
            entity.Property(e => e.PeppolEndpoint)
                .HasComment("Peppol Endpoint")
                .HasColumnType("character varying")
                .HasColumnName("peppol_endpoint");
            entity.Property(e => e.Phone)
                .HasComment("Phone")
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.PhoneSanitized)
                .HasComment("Sanitized Number")
                .HasColumnType("character varying")
                .HasColumnName("phone_sanitized");
            entity.Property(e => e.PickingWarn)
                .HasComment("Stock Picking")
                .HasColumnType("character varying")
                .HasColumnName("picking_warn");
            entity.Property(e => e.PickingWarnMsg)
                .HasComment("Message for Stock Picking")
                .HasColumnName("picking_warn_msg");
            entity.Property(e => e.PricelistIdVal)
                .HasComment("Pricelist")
                .HasColumnName("pricelist_id_val");
            entity.Property(e => e.PricelistName)
                .HasComment("Pricelist")
                .HasColumnType("character varying")
                .HasColumnName("pricelist_name");
            entity.Property(e => e.PurchaseWarn)
                .HasComment("Purchase Order")
                .HasColumnType("character varying")
                .HasColumnName("purchase_warn");
            entity.Property(e => e.PurchaseWarnMsg)
                .HasComment("Message for Purchase Order")
                .HasColumnName("purchase_warn_msg");
            entity.Property(e => e.Ref)
                .HasComment("Reference")
                .HasColumnType("character varying")
                .HasColumnName("ref");
            entity.Property(e => e.Reference)
                .HasComment("Reference")
                .HasColumnType("character varying")
                .HasColumnName("reference");
            entity.Property(e => e.RegionId)
                .HasComment("Region")
                .HasColumnName("region_id");
            entity.Property(e => e.SaleWarn)
                .HasComment("Sales Warnings")
                .HasColumnType("character varying")
                .HasColumnName("sale_warn");
            entity.Property(e => e.SaleWarnMsg)
                .HasComment("Message for Sales Order")
                .HasColumnName("sale_warn_msg");
            entity.Property(e => e.Scale)
                .HasComment("Scale")
                .HasColumnName("scale");
            entity.Property(e => e.ShippingAddress)
                .HasComment("Shipping Address")
                .HasColumnType("character varying")
                .HasColumnName("shipping_address");
            entity.Property(e => e.ShippingTypeIds)
                .HasComment("Shipping Type")
                .HasColumnName("shipping_type_ids");
            entity.Property(e => e.Shortname)
                .HasComment("Trade Name")
                .HasColumnType("character varying")
                .HasColumnName("shortname");
            entity.Property(e => e.SignupExpiration)
                .HasComment("Signup Expiration")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("signup_expiration");
            entity.Property(e => e.SignupToken)
                .HasColumnType("character varying")
                .HasColumnName("signup_token");
            entity.Property(e => e.SignupType)
                .HasComment("Signup Token Type")
                .HasColumnType("character varying")
                .HasColumnName("signup_type");
            entity.Property(e => e.StateId)
                .HasComment("State")
                .HasColumnName("state_id");
            entity.Property(e => e.Street)
                .HasComment("Street")
                .HasColumnType("character varying")
                .HasColumnName("street");
            entity.Property(e => e.Street2)
                .HasComment("Street2")
                .HasColumnType("character varying")
                .HasColumnName("street2");
            entity.Property(e => e.SubChannelId)
                .HasComment("Sub Channel")
                .HasColumnName("sub_channel_id");
            entity.Property(e => e.SupplierRank)
                .HasComment("Supplier Rank")
                .HasColumnName("supplier_rank");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.TeamId)
                .HasComment("Sales Team")
                .HasColumnName("team_id");
            entity.Property(e => e.Title)
                .HasComment("Title")
                .HasColumnName("title");
            entity.Property(e => e.Type)
                .HasComment("Address Type")
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.Tz)
                .HasComment("Timezone")
                .HasColumnType("character varying")
                .HasColumnName("tz");
            entity.Property(e => e.UblCiiFormat)
                .HasComment("Format")
                .HasColumnType("character varying")
                .HasColumnName("ubl_cii_format");
            entity.Property(e => e.UserId)
                .HasComment("Salesperson")
                .HasColumnName("user_id");
            entity.Property(e => e.V15Code)
                .HasComment("V15 Code")
                .HasColumnType("character varying")
                .HasColumnName("v15_code");
            entity.Property(e => e.Vat)
                .HasComment("Tax ID")
                .HasColumnType("character varying")
                .HasColumnName("vat");
            entity.Property(e => e.VatName)
                .HasComment("VAT Name")
                .HasColumnType("character varying")
                .HasColumnName("vat_name");
            entity.Property(e => e.WardId)
                .HasComment("Ward")
                .HasColumnName("ward_id");
            entity.Property(e => e.WarrantyType)
                .HasComment("Warranty Type")
                .HasColumnType("character varying")
                .HasColumnName("warranty_type");
            entity.Property(e => e.Website)
                .HasComment("Website Link")
                .HasColumnType("character varying")
                .HasColumnName("website");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");
            entity.Property(e => e.XVaAccount)
                .HasComment("Tài khoản VA")
                .HasColumnType("character varying")
                .HasColumnName("x_va_account");
            entity.Property(e => e.XVaApprovedDate)
                .HasComment("Ngày duyệt TK VA")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("x_va_approved_date");
            entity.Property(e => e.Zip)
                .HasComment("Zip")
                .HasColumnType("character varying")
                .HasColumnName("zip");

            entity.HasOne(d => d.Buyer).WithMany(p => p.ResPartnerBuyers)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_buyer_id_fkey");

            entity.HasOne(d => d.CommercialPartner).WithMany(p => p.InverseCommercialPartner)
                .HasForeignKey(d => d.CommercialPartnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_commercial_partner_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ResPartnerCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_create_uid_fkey");

            entity.HasOne(d => d.District).WithMany(p => p.ResPartners)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("res_partner_district_id_fkey");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_parent_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.ResPartners)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("res_partner_state_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ResPartnerUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_user_id_fkey");

            entity.HasOne(d => d.Ward).WithMany(p => p.ResPartners)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("res_partner_ward_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ResPartnerWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_partner_write_uid_fkey");
        });

        modelBuilder.Entity<ResUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("res_users_pkey");

            entity.ToTable("res_users");

            entity.HasIndex(e => e.PartnerId, "res_users__partner_id_index");

            entity.HasIndex(e => e.Login, "res_users_login_key").IsUnique();

            entity.HasIndex(e => new { e.OauthProviderId, e.OauthUid }, "res_users_uniq_users_oauth_provider_oauth_uid").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionId)
                .HasComment("Home Action")
                .HasColumnName("action_id");
            entity.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.FollowAllWarehouse)
                .HasComment("Follow All Warehouse")
                .HasColumnName("follow_all_warehouse");
            entity.Property(e => e.Karma)
                .HasComment("Karma")
                .HasColumnName("karma");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.NextRankId)
                .HasComment("Next Rank")
                .HasColumnName("next_rank_id");
            entity.Property(e => e.NotificationType)
                .HasComment("Notification")
                .HasColumnType("character varying")
                .HasColumnName("notification_type");
            entity.Property(e => e.OauthAccessToken)
                .HasComment("OAuth Access Token")
                .HasColumnType("character varying")
                .HasColumnName("oauth_access_token");
            entity.Property(e => e.OauthProviderId)
                .HasComment("OAuth Provider")
                .HasColumnName("oauth_provider_id");
            entity.Property(e => e.OauthUid)
                .HasComment("OAuth User ID")
                .HasColumnType("character varying")
                .HasColumnName("oauth_uid");
            entity.Property(e => e.OdoobotFailed)
                .HasComment("Odoobot Failed")
                .HasColumnName("odoobot_failed");
            entity.Property(e => e.OdoobotState)
                .HasComment("OdooBot Status")
                .HasColumnType("character varying")
                .HasColumnName("odoobot_state");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.RankId)
                .HasComment("Rank")
                .HasColumnName("rank_id");
            entity.Property(e => e.SaleTeamId)
                .HasComment("User Sales Team")
                .HasColumnName("sale_team_id");
            entity.Property(e => e.SaleUser)
                .HasComment("Is Sales User ?")
                .HasColumnName("sale_user");
            entity.Property(e => e.Share)
                .HasComment("Share User")
                .HasColumnName("share");
            entity.Property(e => e.Signature)
                .HasComment("Email Signature")
                .HasColumnName("signature");
            entity.Property(e => e.TargetSalesDone)
                .HasComment("Activities Done Target")
                .HasColumnName("target_sales_done");
            entity.Property(e => e.TargetSalesInvoiced)
                .HasComment("Invoiced in Sales Orders Target")
                .HasColumnName("target_sales_invoiced");
            entity.Property(e => e.TargetSalesWon)
                .HasComment("Won in Opportunities Target")
                .HasColumnName("target_sales_won");
            entity.Property(e => e.TotpSecret)
                .HasColumnType("character varying")
                .HasColumnName("totp_secret");
            entity.Property(e => e.WarehousesAllowDom)
                .HasComment("Warehouses Allow Dom")
                .HasColumnType("character varying")
                .HasColumnName("warehouses_allow_dom");
            entity.Property(e => e.WarehousesDom)
                .HasComment("Warehouses Dom")
                .HasColumnType("character varying")
                .HasColumnName("warehouses_dom");
            entity.Property(e => e.WorkingAllWarehouse)
                .HasComment("Working All Warehouse")
                .HasColumnName("working_all_warehouse");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.InverseCreateU)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_users_create_uid_fkey");

            entity.HasOne(d => d.Partner).WithMany(p => p.ResUsers)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("res_users_partner_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.InverseWriteU)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_users_write_uid_fkey");
        });

        modelBuilder.Entity<ResWard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("res_ward_pkey");

            entity.ToTable("res_ward", tb => tb.HasComment("Ward"));

            entity.HasIndex(e => e.Code, "res_ward_ward_code_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasComment("Code")
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.CountryId)
                .HasComment("Country")
                .HasColumnName("country_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.DistrictId)
                .HasComment("District")
                .HasColumnName("district_id");
            entity.Property(e => e.Name)
                .HasComment("Name")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.StateId)
                .HasComment("Country State")
                .HasColumnName("state_id");
            entity.Property(e => e.SyncId)
                .HasComment("Synchronize ID")
                .HasColumnName("sync_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ResWardCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_ward_create_uid_fkey");

            entity.HasOne(d => d.District).WithMany(p => p.ResWards)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("res_ward_district_id_fkey");

            entity.HasOne(d => d.State).WithMany(p => p.ResWards)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_ward_state_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ResWardWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("res_ward_write_uid_fkey");
        });

        modelBuilder.Entity<ResourceResource>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("resource_resource_pkey");

            entity.ToTable("resource_resource", tb => tb.HasComment("Resources"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasComment("Active")
                .HasColumnName("active");
            entity.Property(e => e.CalendarId)
                .HasComment("Working Time")
                .HasColumnName("calendar_id");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.Name)
                .HasComment("Name")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.ResourceType)
                .HasComment("Type")
                .HasColumnType("character varying")
                .HasColumnName("resource_type");
            entity.Property(e => e.TimeEfficiency)
                .HasComment("Efficiency Factor")
                .HasColumnName("time_efficiency");
            entity.Property(e => e.Tz)
                .HasComment("Timezone")
                .HasColumnType("character varying")
                .HasColumnName("tz");
            entity.Property(e => e.UserId)
                .HasComment("User")
                .HasColumnName("user_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.ResourceResourceCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("resource_resource_create_uid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ResourceResourceUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("resource_resource_user_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.ResourceResourceWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("resource_resource_write_uid_fkey");
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sale_order_pkey");

            entity.ToTable("sale_order", tb => tb.HasComment("Sales Order"));

            entity.HasIndex(e => e.CampaignId, "sale_order__campaign_id_index").HasFilter("(campaign_id IS NOT NULL)");

            entity.HasIndex(e => e.CompanyId, "sale_order__company_id_index");

            entity.HasIndex(e => e.CreateDate, "sale_order__create_date_index");

            entity.HasIndex(e => e.DestCompanyId, "sale_order__dest_company_id_index");

            entity.HasIndex(e => e.MediumId, "sale_order__medium_id_index").HasFilter("(medium_id IS NOT NULL)");

            entity.HasIndex(e => e.Name, "sale_order__name_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.OriginalOrderId, "sale_order__original_order_id_index");

            entity.HasIndex(e => e.PartnerId, "sale_order__partner_id_index");

            entity.HasIndex(e => e.PartnerInvoiceId, "sale_order__partner_invoice_id_index").HasFilter("(partner_invoice_id IS NOT NULL)");

            entity.HasIndex(e => e.PartnerShippingId, "sale_order__partner_shipping_id_index").HasFilter("(partner_shipping_id IS NOT NULL)");

            entity.HasIndex(e => e.SourceId, "sale_order__source_id_index").HasFilter("(source_id IS NOT NULL)");

            entity.HasIndex(e => e.State, "sale_order__state_index");

            entity.HasIndex(e => e.UserId, "sale_order__user_id_index");

            entity.HasIndex(e => new { e.DateOrder, e.Id }, "sale_order_date_order_id_idx").IsDescending();

            entity.HasIndex(e => new { e.Name, e.CompanyId }, "sale_order_name_company_uniq").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessToken)
                .HasComment("Security Token")
                .HasColumnType("character varying")
                .HasColumnName("access_token");
            entity.Property(e => e.AmountPaidInvoice)
                .HasComment("Amount paid")
                .HasColumnName("amount_paid_invoice");
            entity.Property(e => e.AmountTax)
                .HasComment("Taxes")
                .HasColumnName("amount_tax");
            entity.Property(e => e.AmountToInvoice)
                .HasComment("Amount to invoice")
                .HasColumnName("amount_to_invoice");
            entity.Property(e => e.AmountTotal)
                .HasComment("Total")
                .HasColumnName("amount_total");
            entity.Property(e => e.AmountTotalInvoice)
                .HasComment("Amount total")
                .HasColumnName("amount_total_invoice");
            entity.Property(e => e.AmountUntaxed)
                .HasComment("Untaxed Amount")
                .HasColumnName("amount_untaxed");
            entity.Property(e => e.AnalyticAccountId)
                .HasComment("Analytic Account")
                .HasColumnName("analytic_account_id");
            entity.Property(e => e.ApiSoConfig)
                .HasComment("API Source")
                .HasColumnName("api_so_config");
            entity.Property(e => e.AutoGenerated)
                .HasComment("Auto Generated Sales Order")
                .HasColumnName("auto_generated");
            entity.Property(e => e.AutoPurchaseOrderId)
                .HasComment("Source Purchase Order")
                .HasColumnName("auto_purchase_order_id");
            entity.Property(e => e.BuyerNameVatInvoice)
                .HasComment("Buyer Name on VAT Invoice")
                .HasColumnType("character varying")
                .HasColumnName("buyer_name_vat_invoice");
            entity.Property(e => e.CampaignId)
                .HasComment("Campaign")
                .HasColumnName("campaign_id");
            entity.Property(e => e.ChannelId)
                .HasComment("Sales Channel")
                .HasColumnName("channel_id");
            entity.Property(e => e.ClientOrderRef)
                .HasComment("Customer Reference")
                .HasColumnType("character varying")
                .HasColumnName("client_order_ref");
            entity.Property(e => e.CommitmentDate)
                .HasComment("Delivery Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("commitment_date");
            entity.Property(e => e.CompanyCurrencyId)
                .HasComment("Company Currency")
                .HasColumnName("company_currency_id");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.ComputeHideReverseTranfers)
                .HasComment("Hide Reverse Tranfers")
                .HasColumnName("compute_hide_reverse_tranfers");
            entity.Property(e => e.CreateDate)
                .HasComment("Creation Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.CurrencyId)
                .HasComment("Currency")
                .HasColumnName("currency_id");
            entity.Property(e => e.CurrencyRate)
                .HasComment("Currency Rate")
                .HasColumnName("currency_rate");
            entity.Property(e => e.DateDones)
                .HasComment("Date done")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_dones");
            entity.Property(e => e.DateDues)
                .HasComment("Date due")
                .HasColumnName("date_dues");
            entity.Property(e => e.DateInvoices)
                .HasComment("Date invoice")
                .HasColumnName("date_invoices");
            entity.Property(e => e.DateOrder)
                .HasComment("Order Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_order");
            entity.Property(e => e.DeliveryCount)
                .HasComment("Delivery Orders")
                .HasColumnName("delivery_count");
            entity.Property(e => e.DeliveryPrice)
                .HasComment("Estimated Delivery Price")
                .HasColumnName("delivery_price");
            entity.Property(e => e.DeliveryStatus)
                .HasComment("Delivery Status")
                .HasColumnType("character varying")
                .HasColumnName("delivery_status");
            entity.Property(e => e.DestCompanyId)
                .HasComment("Company Dest")
                .HasColumnName("dest_company_id");
            entity.Property(e => e.DestWarehouseId)
                .HasComment("Dest Warehouse (Inter-comp)")
                .HasColumnName("dest_warehouse_id");
            entity.Property(e => e.EffectiveDate)
                .HasComment("Effective Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("effective_date");
            entity.Property(e => e.FirstApproverRecordId)
                .HasComment("First Approver")
                .HasColumnName("first_approver_record_id");
            entity.Property(e => e.FiscalPositionId)
                .HasComment("Fiscal Position")
                .HasColumnName("fiscal_position_id");
            entity.Property(e => e.Incoterm)
                .HasComment("Incoterm")
                .HasColumnName("incoterm");
            entity.Property(e => e.IncotermLocation)
                .HasComment("Incoterm Location")
                .HasColumnType("character varying")
                .HasColumnName("incoterm_location");
            entity.Property(e => e.InputOrderNumber)
                .HasComment("Input Original SO Number")
                .HasColumnType("character varying")
                .HasColumnName("input_order_number");
            entity.Property(e => e.InvoiceStatus)
                .HasComment("Invoice Status")
                .HasColumnType("character varying")
                .HasColumnName("invoice_status");
            entity.Property(e => e.IsInterCompanyTransaction)
                .HasComment("Is Inter-company Transaction ?")
                .HasColumnName("is_inter_company_transaction");
            entity.Property(e => e.IsNeedCreateConsignment15)
                .HasComment("Need Create Consignment V15")
                .HasColumnName("is_need_create_consignment_15");
            entity.Property(e => e.IsNeedCreateSo15)
                .HasComment("Need Create SO V15")
                .HasColumnName("is_need_create_so_15");
            entity.Property(e => e.JournalId)
                .HasComment("Invoicing Journal")
                .HasColumnName("journal_id");
            entity.Property(e => e.Locked)
                .HasComment("Locked")
                .HasColumnName("locked");
            entity.Property(e => e.MediumId)
                .HasComment("Medium")
                .HasColumnName("medium_id");
            entity.Property(e => e.Name)
                .HasComment("Order Reference")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasComment("Terms and conditions")
                .HasColumnName("note");
            entity.Property(e => e.OpportunityId)
                .HasComment("Opportunity")
                .HasColumnName("opportunity_id");
            entity.Property(e => e.OrderType)
                .HasComment("Order Type")
                .HasColumnType("character varying")
                .HasColumnName("order_type");
            entity.Property(e => e.Origin)
                .HasComment("Source Document")
                .HasColumnType("character varying")
                .HasColumnName("origin");
            entity.Property(e => e.OriginalOrderId)
                .HasComment("Original SO Number")
                .HasColumnName("original_order_id");
            entity.Property(e => e.PartnerAddress)
                .HasComment("Address")
                .HasColumnType("character varying")
                .HasColumnName("partner_address");
            entity.Property(e => e.PartnerGroupId)
                .HasComment("Res Partner Group")
                .HasColumnName("partner_group_id");
            entity.Property(e => e.PartnerId)
                .HasComment("Customer")
                .HasColumnName("partner_id");
            entity.Property(e => e.PartnerInvoiceId)
                .HasComment("Invoice Address")
                .HasColumnName("partner_invoice_id");
            entity.Property(e => e.PartnerShippingId)
                .HasComment("Delivery Address")
                .HasColumnName("partner_shipping_id");
            entity.Property(e => e.PaymentDueDate)
                .HasComment("Payment Due Day")
                .HasColumnName("payment_due_date");
            entity.Property(e => e.PaymentPayload)
                .HasComment("Auto Reconcile")
                .HasColumnName("payment_payload");
            entity.Property(e => e.PaymentTermId)
                .HasComment("Payment Terms")
                .HasColumnName("payment_term_id");
            entity.Property(e => e.PendingEmailTemplateId)
                .HasComment("Pending Email Template")
                .HasColumnName("pending_email_template_id");
            entity.Property(e => e.PickingChecked)
                .HasComment("Picking Checked")
                .HasColumnName("picking_checked");
            entity.Property(e => e.PickingCountReturn)
                .HasComment("Picking Return Count")
                .HasColumnName("picking_count_return");
            entity.Property(e => e.PickingPolicy)
                .HasComment("Shipping Policy")
                .HasColumnType("character varying")
                .HasColumnName("picking_policy");
            entity.Property(e => e.Platform)
                .HasComment("Platform?")
                .HasColumnName("platform");
            entity.Property(e => e.PrepaymentPercent)
                .HasComment("Prepayment percentage")
                .HasColumnName("prepayment_percent");
            entity.Property(e => e.PricelistId)
                .HasComment("Pricelist")
                .HasColumnName("pricelist_id");
            entity.Property(e => e.ProcurementGroupId)
                .HasComment("Procurement Group")
                .HasColumnName("procurement_group_id");
            entity.Property(e => e.Reference)
                .HasComment("Payment Ref.")
                .HasColumnType("character varying")
                .HasColumnName("reference");
            entity.Property(e => e.RequirePayment)
                .HasComment("Online payment")
                .HasColumnName("require_payment");
            entity.Property(e => e.RequireSignature)
                .HasComment("Online signature")
                .HasColumnName("require_signature");
            entity.Property(e => e.Responsible)
                .HasComment("Responsible")
                .HasColumnType("character varying")
                .HasColumnName("responsible");
            entity.Property(e => e.ReturnProcurementGroupId)
                .HasComment("Return Procurement Group")
                .HasColumnName("return_procurement_group_id");
            entity.Property(e => e.ReturnReason)
                .HasComment("Return Reason")
                .HasColumnName("return_reason");
            entity.Property(e => e.ReturnReasonId)
                .HasComment("Return Reason")
                .HasColumnName("return_reason_id");
            entity.Property(e => e.SaleOrderTemplateId).HasColumnName("sale_order_template_id");
            entity.Property(e => e.SecondApproverRecordId)
                .HasComment("Second Approver")
                .HasColumnName("second_approver_record_id");
            entity.Property(e => e.ShippingTypeIds)
                .HasComment("Shipping Type")
                .HasColumnName("shipping_type_ids");
            entity.Property(e => e.SignedBy)
                .HasComment("Signed By")
                .HasColumnType("character varying")
                .HasColumnName("signed_by");
            entity.Property(e => e.SignedOn)
                .HasComment("Signed On")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("signed_on");
            entity.Property(e => e.SoReferenceId)
                .HasComment("Reference SO")
                .HasColumnType("character varying")
                .HasColumnName("so_reference_id");
            entity.Property(e => e.SoTypeIds)
                .HasComment("SO Type")
                .HasColumnName("so_type_ids");
            entity.Property(e => e.SourceId)
                .HasComment("Source")
                .HasColumnName("source_id");
            entity.Property(e => e.State)
                .HasComment("Status")
                .HasColumnType("character varying")
                .HasColumnName("state");
            entity.Property(e => e.TeamId)
                .HasComment("Sales Team")
                .HasColumnName("team_id");
            entity.Property(e => e.TotalDeliveredQty)
                .HasComment("Total Delivered Qty")
                .HasColumnName("total_delivered_qty");
            entity.Property(e => e.TotalDeliveredValue)
                .HasComment("Total Delivered Value")
                .HasColumnName("total_delivered_value");
            entity.Property(e => e.TotalQuantity)
                .HasComment("Total Quantity")
                .HasColumnName("total_quantity");
            entity.Property(e => e.UserId)
                .HasComment("Salesperson")
                .HasColumnName("user_id");
            entity.Property(e => e.ValidityDate)
                .HasComment("Expiration")
                .HasColumnName("validity_date");
            entity.Property(e => e.VatDate)
                .HasComment("VAT Date")
                .HasColumnName("vat_date");
            entity.Property(e => e.VatInvoiceStatus)
                .HasComment("VAT Invoice Status")
                .HasColumnType("character varying")
                .HasColumnName("vat_invoice_status");
            entity.Property(e => e.VatInvoiceType)
                .HasComment("VAT Invoice Type")
                .HasColumnType("character varying")
                .HasColumnName("vat_invoice_type");
            entity.Property(e => e.VatNumber)
                .HasComment("VAT Number")
                .HasColumnType("character varying")
                .HasColumnName("vat_number");
            entity.Property(e => e.VatPartnerId)
                .HasComment("Vat Partner")
                .HasColumnName("vat_partner_id");
            entity.Property(e => e.WarehouseId)
                .HasComment("Warehouse")
                .HasColumnName("warehouse_id");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.AnalyticAccount).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.AnalyticAccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_analytic_account_id_fkey");

            entity.HasOne(d => d.CreateU).WithMany(p => p.SaleOrderCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_create_uid_fkey");

            entity.HasOne(d => d.FirstApproverRecord).WithMany(p => p.SaleOrderFirstApproverRecords)
                .HasForeignKey(d => d.FirstApproverRecordId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_first_approver_record_id_fkey");

            entity.HasOne(d => d.Journal).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.JournalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_journal_id_fkey");

            entity.HasOne(d => d.OriginalOrder).WithMany(p => p.InverseOriginalOrder)
                .HasForeignKey(d => d.OriginalOrderId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_original_order_id_fkey");

            entity.HasOne(d => d.Partner).WithMany(p => p.SaleOrderPartners)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sale_order_partner_id_fkey");

            entity.HasOne(d => d.PartnerInvoice).WithMany(p => p.SaleOrderPartnerInvoices)
                .HasForeignKey(d => d.PartnerInvoiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("sale_order_partner_invoice_id_fkey");

            entity.HasOne(d => d.PartnerShipping).WithMany(p => p.SaleOrderPartnerShippings)
                .HasForeignKey(d => d.PartnerShippingId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_partner_shipping_id_fkey");

            entity.HasOne(d => d.SecondApproverRecord).WithMany(p => p.SaleOrderSecondApproverRecords)
                .HasForeignKey(d => d.SecondApproverRecordId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_second_approver_record_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.SaleOrderUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_user_id_fkey");

            entity.HasOne(d => d.VatPartner).WithMany(p => p.SaleOrderVatPartners)
                .HasForeignKey(d => d.VatPartnerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_vat_partner_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.SaleOrderWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("sale_order_write_uid_fkey");
        });

        modelBuilder.Entity<SaleReport>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("sale_report");

            entity.Property(e => e.AnalyticAccountId).HasColumnName("analytic_account_id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.CategId).HasColumnName("categ_id");
            entity.Property(e => e.CommercialPartnerId).HasColumnName("commercial_partner_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.InvoiceStatus)
                .HasColumnType("character varying")
                .HasColumnName("invoice_status");
            entity.Property(e => e.MediumId).HasColumnName("medium_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Nbr).HasColumnName("nbr");
            entity.Property(e => e.OrderReference).HasColumnName("order_reference");
            entity.Property(e => e.PartnerId).HasColumnName("partner_id");
            entity.Property(e => e.PartnerZip)
                .HasColumnType("character varying")
                .HasColumnName("partner_zip");
            entity.Property(e => e.PriceSubtotal).HasColumnName("price_subtotal");
            entity.Property(e => e.PriceTotal).HasColumnName("price_total");
            entity.Property(e => e.PricelistId).HasColumnName("pricelist_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductTmplId).HasColumnName("product_tmpl_id");
            entity.Property(e => e.ProductUom).HasColumnName("product_uom");
            entity.Property(e => e.ProductUomQty).HasColumnName("product_uom_qty");
            entity.Property(e => e.QtyDelivered).HasColumnName("qty_delivered");
            entity.Property(e => e.QtyInvoiced).HasColumnName("qty_invoiced");
            entity.Property(e => e.QtyToDeliver).HasColumnName("qty_to_deliver");
            entity.Property(e => e.QtyToInvoice).HasColumnName("qty_to_invoice");
            entity.Property(e => e.SourceId).HasColumnName("source_id");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("state");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.UntaxedAmountInvoiced).HasColumnName("untaxed_amount_invoiced");
            entity.Property(e => e.UntaxedAmountToInvoice).HasColumnName("untaxed_amount_to_invoice");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Volume).HasColumnName("volume");
            entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<StockLot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("stock_lot_pkey");

            entity.ToTable("stock_lot", tb => tb.HasComment("Lot/Serial"));

            entity.HasIndex(e => e.CompanyId, "stock_lot__company_id_index");

            entity.HasIndex(e => e.Name, "stock_lot__name_index")
                .HasMethod("gin")
                .HasOperators(new[] { "gin_trgm_ops" });

            entity.HasIndex(e => e.ProductId, "stock_lot__product_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlertDate)
                .HasComment("Alert Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("alert_date");
            entity.Property(e => e.CheckStockDate)
                .HasComment("Check Stock")
                .HasColumnType("character varying")
                .HasColumnName("check_stock_date");
            entity.Property(e => e.CompanyCurrencyId)
                .HasComment("Currency")
                .HasColumnName("company_currency_id");
            entity.Property(e => e.CompanyId)
                .HasComment("Company")
                .HasColumnName("company_id");
            entity.Property(e => e.ConsignmentDate)
                .HasComment("Consignment Date")
                .HasColumnName("consignment_date");
            entity.Property(e => e.CostUnit)
                .HasComment("Cost Unit")
                .HasColumnName("cost_unit");
            entity.Property(e => e.CreateDate)
                .HasComment("Created on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUid)
                .HasComment("Created by")
                .HasColumnName("create_uid");
            entity.Property(e => e.ExpirationDate)
                .HasComment("Expiration Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiration_date");
            entity.Property(e => e.FilterDatetime)
                .HasComment("Filter Datetime")
                .HasColumnName("filter_datetime");
            entity.Property(e => e.InstallationDate)
                .HasComment("Installation Date")
                .HasColumnName("installation_date");
            entity.Property(e => e.LocationId)
                .HasComment("Location")
                .HasColumnName("location_id");
            entity.Property(e => e.LotCustomerId)
                .HasComment("Customer")
                .HasColumnName("lot_customer_id");
            entity.Property(e => e.LotDocsSource)
                .HasComment("Documents")
                .HasColumnName("lot_docs_source");
            entity.Property(e => e.LotExSnDate)
                .HasComment("Exchange Serial Number Date")
                .HasColumnName("lot_ex_sn_date");
            entity.Property(e => e.LotImportSnDate)
                .HasComment("Import Date")
                .HasColumnName("lot_import_sn_date");
            entity.Property(e => e.LotLocationId)
                .HasComment("Location")
                .HasColumnName("lot_location_id");
            entity.Property(e => e.LotNote)
                .HasComment("Notes")
                .HasColumnName("lot_note");
            entity.Property(e => e.LotOrderSource)
                .HasComment("Orders")
                .HasColumnName("lot_order_source");
            entity.Property(e => e.LotProperties)
                .HasComment("Properties")
                .HasColumnType("jsonb")
                .HasColumnName("lot_properties");
            entity.Property(e => e.LotSerialnumberType)
                .HasComment("Type")
                .HasColumnType("character varying")
                .HasColumnName("lot_serialnumber_type");
            entity.Property(e => e.LotShipment)
                .HasComment("Shipments")
                .HasColumnType("character varying")
                .HasColumnName("lot_shipment");
            entity.Property(e => e.LotState)
                .HasComment("Status")
                .HasColumnType("character varying")
                .HasColumnName("lot_state");
            entity.Property(e => e.LotWarrantyEndDate)
                .HasComment("Warranty End Date")
                .HasColumnName("lot_warranty_end_date");
            entity.Property(e => e.LotWarrantyStartDate)
                .HasComment("Warranty Start Date")
                .HasColumnName("lot_warranty_start_date");
            entity.Property(e => e.ManualActiveDate)
                .HasComment("Manual Activated Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("manual_active_date");
            entity.Property(e => e.Name)
                .HasComment("Lot/Serial Number")
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasComment("Description")
                .HasColumnName("note");
            entity.Property(e => e.OnlineActiveDate)
                .HasComment("Online Activated Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("online_active_date");
            entity.Property(e => e.ProductExpiryReminded)
                .HasComment("Expiry has been reminded")
                .HasColumnName("product_expiry_reminded");
            entity.Property(e => e.ProductId)
                .HasComment("Product")
                .HasColumnName("product_id");
            entity.Property(e => e.ProductUomId)
                .HasComment("Unit of Measure")
                .HasColumnName("product_uom_id");
            entity.Property(e => e.Ref)
                .HasComment("Internal Reference")
                .HasColumnType("character varying")
                .HasColumnName("ref");
            entity.Property(e => e.RemovalDate)
                .HasComment("Removal Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("removal_date");
            entity.Property(e => e.SalespersonId)
                .HasComment("Salesperson")
                .HasColumnName("salesperson_id");
            entity.Property(e => e.SerialName)
                .HasComment("Serial 2")
                .HasColumnType("character varying")
                .HasColumnName("serial_name");
            entity.Property(e => e.UseDate)
                .HasComment("Best before Date")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("use_date");
            entity.Property(e => e.Value)
                .HasComment("Cost Value")
                .HasColumnName("value");
            entity.Property(e => e.WriteDate)
                .HasComment("Last Updated on")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
            entity.Property(e => e.WriteUid)
                .HasComment("Last Updated by")
                .HasColumnName("write_uid");

            entity.HasOne(d => d.CreateU).WithMany(p => p.StockLotCreateUs)
                .HasForeignKey(d => d.CreateUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("stock_lot_create_uid_fkey");

            entity.HasOne(d => d.LotCustomer).WithMany(p => p.StockLots)
                .HasForeignKey(d => d.LotCustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("stock_lot_lot_customer_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.StockLots)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("stock_lot_product_id_fkey");

            entity.HasOne(d => d.Salesperson).WithMany(p => p.StockLotSalespeople)
                .HasForeignKey(d => d.SalespersonId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("stock_lot_salesperson_id_fkey");

            entity.HasOne(d => d.WriteU).WithMany(p => p.StockLotWriteUs)
                .HasForeignKey(d => d.WriteUid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("stock_lot_write_uid_fkey");
        });

        modelBuilder.Entity<VietmapCustomerView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vietmap_customer_view");

            entity.Property(e => e.ActiveCustomer).HasColumnName("active_customer");
            entity.Property(e => e.CarCompany)
                .HasColumnType("character varying")
                .HasColumnName("car_company");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.CreateDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUserEmail)
                .HasColumnType("character varying")
                .HasColumnName("create_user_email");
            entity.Property(e => e.Credit).HasColumnName("credit");
            entity.Property(e => e.CreditLimit).HasColumnName("credit_limit");
            entity.Property(e => e.CustomerAddress)
                .HasColumnType("character varying")
                .HasColumnName("customer_address");
            entity.Property(e => e.District)
                .HasColumnType("character varying")
                .HasColumnName("district");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeeName)
                .HasColumnType("character varying")
                .HasColumnName("employee_name");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InternalRef)
                .HasColumnType("character varying")
                .HasColumnName("internal_ref");
            entity.Property(e => e.Mobile)
                .HasColumnType("character varying")
                .HasColumnName("mobile");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PartnerGroup)
                .HasColumnType("character varying")
                .HasColumnName("partner_group");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Pricelist)
                .HasColumnType("character varying")
                .HasColumnName("pricelist");
            entity.Property(e => e.PricelistId).HasColumnName("pricelist_id");
            entity.Property(e => e.Reference)
                .HasColumnType("character varying")
                .HasColumnName("reference");
            entity.Property(e => e.Scale)
                .HasColumnType("character varying")
                .HasColumnName("scale");
            entity.Property(e => e.StateName)
                .HasColumnType("character varying")
                .HasColumnName("state_name");
            entity.Property(e => e.WriteDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("write_date");
        });
        modelBuilder.HasSequence("base_cache_signaling_assets");
        modelBuilder.HasSequence("base_cache_signaling_default");
        modelBuilder.HasSequence("base_cache_signaling_routing");
        modelBuilder.HasSequence("base_cache_signaling_templates");
        modelBuilder.HasSequence("base_registry_signaling");
        modelBuilder.HasSequence("ir_sequence_006");
        modelBuilder.HasSequence("ir_sequence_007");
        modelBuilder.HasSequence("ir_sequence_008");
        modelBuilder.HasSequence("ir_sequence_009");
        modelBuilder.HasSequence("ir_sequence_010");
        modelBuilder.HasSequence("ir_sequence_011");
        modelBuilder.HasSequence("ir_sequence_012");
        modelBuilder.HasSequence("ir_sequence_013");
        modelBuilder.HasSequence("ir_sequence_026");
        modelBuilder.HasSequence("ir_sequence_028");
        modelBuilder.HasSequence("ir_sequence_030");
        modelBuilder.HasSequence("ir_sequence_039");
        modelBuilder.HasSequence("ir_sequence_040");
        modelBuilder.HasSequence("ir_sequence_041");
        modelBuilder.HasSequence("ir_sequence_054");
        modelBuilder.HasSequence("ir_sequence_055");
        modelBuilder.HasSequence("ir_sequence_056");
        modelBuilder.HasSequence("ir_sequence_057");
        modelBuilder.HasSequence("ir_sequence_059");
        modelBuilder.HasSequence("ir_sequence_061");
        modelBuilder.HasSequence("ir_sequence_083");
        modelBuilder.HasSequence("ir_sequence_084");
        modelBuilder.HasSequence("ir_sequence_085");
        modelBuilder.HasSequence("ir_sequence_086");
        modelBuilder.HasSequence("ir_sequence_087");
        modelBuilder.HasSequence("ir_sequence_088");
        modelBuilder.HasSequence("ir_sequence_089");
        modelBuilder.HasSequence("ir_sequence_090");
        modelBuilder.HasSequence("ir_sequence_091");
        modelBuilder.HasSequence("ir_sequence_092");
        modelBuilder.HasSequence("ir_sequence_093");
        modelBuilder.HasSequence("ir_sequence_097");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
