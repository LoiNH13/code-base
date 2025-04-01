using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Contract.Views;

namespace Sale.Persistence.Configurations.Views;

/// <summary>
/// Configures the cus_ag_monthly_view view for the CusAgMonthlyView entity.
/// </summary>
internal sealed class CusAgMonthlyViewConfiguration //: IEntityTypeConfiguration<CusAgMonthlyView>
{

    public const string VIEW_NAME = "cus_ag_monthly_view";
    public const string VIEW_CONTENT = @"CREATE OR REPLACE VIEW cus_ag_monthly_view AS
                                    WITH monthlyData AS
                                  (SELECT id,
                                          customer_id,
                                          from_time_on_utc
                                   FROM
                                     (SELECT id,
                                             customer_id,
                                             from_time_on_utc,
                                             RANK() OVER (PARTITION BY customer_id,
                                                                        EXTRACT(MONTH FROM from_time_on_utc),
                                                                        EXTRACT(YEAR FROM from_time_on_utc)
                                                          ORDER BY from_time_on_utc DESC) AS rank_by_customer
                                      FROM monthly_reports ) AS a
                                   WHERE a.rank_by_customer = 1 )
                                SELECT a.id,
                                       a.managed_by_user_id,
                                       c.name as sale_name,
                                       a.odoo_ref,
                                       a.name,
                                       b.id AS monthly_report_id,
                                       b.from_time_on_utc
                                FROM customers a
                                LEFT JOIN monthlyData b ON a.id = b.customer_id
                                LEFT JOIN users c ON a.managed_by_user_id = c.id
                                WHERE a. odoo_ref IS NOT NULL";

    public void Configure(EntityTypeBuilder<CusAgMonthlyView> builder)
    {
        builder.ToView(VIEW_NAME);

        builder.Property(x => x.Id).HasColumnName("id");
        builder.HasNoKey();

        builder.Property(view => view.MonthlyReportId).HasColumnName("monthly_report_id");

        builder.Property(view => view.ManagedByUserId).HasColumnName("managed_by_user_id");

        builder.Property(view => view.OdooRef).HasColumnName("odoo_ref");

        builder.Property(view => view.Name).HasColumnName("name");

        builder.Property(view => view.FromTimeOnUtc).HasColumnName("from_time_on_utc");
    }
}
