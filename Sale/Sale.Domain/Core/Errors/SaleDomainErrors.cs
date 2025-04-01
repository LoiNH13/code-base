using Domain.Core.Primitives;

namespace Sale.Domain.Core.Errors;

public static class SaleDomainErrors
{
    public static class PlannewCustomer
    {
        public static Error MustHaveOdooRef => new Error("PlannewCustomer.Customer", "Customer must have odoo ref to open");
    }

    public static class ERole
    {
        public static Error NotPermission => new Error("ERole.NotPermission", "Not permission");
    }

    public static class PlanningApproval
    {
        public static Error NotFound => new Error("PlanningApproval.NotFound", "Planning approval not found");

        public static Error AlreadyLocked => new Error("PlanningApproval.AlreadyLocked", "Planning approval already locked");

        public static Error AlreadyApproved => new Error("PlanningApproval.AlreadyApproved", "Planning approval already approved");
    }

    public static class PlanningControlLine
    {
        public static Error NotFound => new Error("PlanningControlLine.NotFound", "Planning control line not found");
    }

    public static class PlanningControl
    {
        public static Error StatusInvalid => new Error("PlanningControl.StatusInvalid", "Planning control status invalid");

        public static Error NotFound => new Error("PlanningControl.NotFound", "Planning control not found");
    }

    public static class ProductTimeFramePrice
    {
        public static Error NotPrice => new Error("ProductTimeFramePrice.NotPrice", "Product has no price at {0}");

        public static Error TimeFrameExisted => new Error("ProductTimeFramePrice.TimeFrameExisted", "Time frame {0} existed");

        public static Error NotFound => new Error("ProductTimeFramePrice.NotFound", "Time frame not found");
    }

    public static class TimeFrame
    {
        public static Error InvalidTimeFrame => new Error("TimeFrame.InvalidTimeFrame", "Invalid time frame");

        public static Error NotFound => new Error("TimeFrame.NotFound", "Time frame not found");
    }

    public static class Product
    {
        public static Error ProductMustInOdoo => new Error("Product.ProductMustInOdoo", "Product must in odoo");

        public static Error NotFound => new Error("Product.NotFound", "Product not found");
    }

    public static class Customer
    {
        public static Error NotFound => new Error("Customer.NotFound", "Customer not found");

        public static Error ExistCustomerTimeFrame => new Error("Customer.ExistCustomerTimeFrame", "Customer time frame existed");

        public static Error OdooRefAlreadyExist => new Error("Customer.OdooRefAlreadyExist", "Odoo ref already exist");

        public static Error TargetOdooRefExisted => new Error("Customer.TargetOdooRefExisted", "Odoo ref existed");

        public static Error PlanNewCustomerCannotRemove => new Error("Customer.PlanNewCustomerCannotRemove", "Plan new customer cannot remove");
    }

    public static class Category
    {
        public static Error NotFound => new Error("Category.NotFound", "Category not found");
    }

    public static class MonthlyReport
    {
        //From time should be less than to time
        public static Error FromThanToTime => new Error("MonthlyReport.FromThanToTime", "From time should be less than to time");

        public static Error TimeMustWithInDay => new Error("MonthlyReport.TimeMustWithInDay", "Time must with in day");

        public static Error ReportConfirmed => new Error("MonthlyReport.ReportConfirmed", "Report confirmed");

        public static Error NotFound => new Error("MonthlyReport.ReportNotFound", "Report not found");

        public static Error BusinessTypeMismatch => new Error("MonthlyReport.BusinessTypeMismatch", "Business type mismatch {0} - {1}");
    }

    public static class MonthlyReportItem
    {
        public static Error NotFound => new Error("MonthlyReportItem.NotFound", "Monthly report item {0} not found");

        public static Error CategoryAlreadyExists => new Error("MonthlyReportItem.CategoryAlreadyExists", "{0} existed");
    }

    public static class User
    {
        public static Error MustAdmin => new Error("User.MustAdmin", "You must be admin");

        public static Error NotFound => new Error("User.NotFound", "User not found");

        public static Error DependencyLoop => new Error("User.DependencyLoop", "User dependency loop");

        public static Error LoginFailed => new Error("User.LoginFailed", "User login failed");

        public static Error CannotChangePassword => new Error("User.CannotChangePassword", "User cannot change password");
    }
}
