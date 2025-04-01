namespace Sale.ApiService.Contracts;

/// <summary>
///     Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    public static class PlanningApprovals
    {
        public const string GetStatuses = "planning-approvals/statuses";

        public const string Get = "planning-approvals";

        public const string WaitingForApproval = "planning-approvals/waiting-for-approval";

        public const string Approve = "planning-approvals/{planningApprovalId:guid}/approve";

        public const string EditRequest = "planning-approvals/{planningApprovalId:guid}/edit-request";

        public const string Unlock = "planning-approvals/{planningApprovalId:guid}/unlock";

        public const string DashboardBySales = "planning-approvals/dashboard/by-sales";
    }

    public static class Metrics
    {
        public const string Summary = "metrics/{customerId:guid}/summary";

        public const string GetForeCastMetrics = "metrics/{customerId:guid}/get-forecasts";

        public const string AddsOrUpdates = "metrics/{customerId:guid}";
    }

    public static class PlanningControlLines
    {
        public const string Create = "planning-control-lines/{planningControlId:guid}";

        public const string Delete = "planning-control-lines/{planningControlId:guid}/{planningControlLineId:guid}";
    }

    public static class PlanningControls
    {
        public const string GetStatuses = "planning-controls/statuses";

        public const string Get = "planning-controls";

        public const string GetById = "planning-controls/{planningControlId:guid}";

        public const string Create = "planning-controls";

        public const string Update = "planning-controls/{planningControlId:guid}";

        public const string UpdateStatus = "planning-controls/{planningControlId:guid}/status";
    }

    public static class TimeFrameProductPrices
    {
        public const string Create = "time-frame-product-prices";

        public const string Update = "time-frame-product-prices/{productId:guid}/{timeFrameProductId:guid}";

        public const string Delete = "time-frame-product-prices/{productId:guid}/{timeFrameProductId:guid}";
    }

    public static class Products
    {
        public const string Get = "products";

        public const string GetById = "products/{productId:guid}";

        public const string Create = "products";

        public const string Update = "products/{productId:guid}";

        public const string UpdateStatus = "products/{productId:guid}/status";
    }

    public static class TimeFrames
    {
        public const string Get = "time-frames";

        public const string GetRange = "time-frames/range";

        public const string GetById = "time-frames/{timeFrameId:guid}";

        public const string Create = "time-frames";
    }

    public static class Customers
    {
        public const string Get = "customers";

        public const string GetWithPlan = "customers/plan";

        public const string GetWithPlanSummary = "customers/plan/summary";

        public const string GetById = "customers/{customerId:guid}";

        public const string Create = "customers";

        public const string OdooCreate = "customers/odoo-create";

        public const string PlanCreate = "customers/plan-create";

        public const string Update = "customers/{customerId:guid}";

        public const string UpdateManagedBy = "customers/managedby";

        public const string MappingOdoo = "customers/{customerId:guid}/mapping-odoo";

        public const string Delete = "customers/{customerId:guid}";
    }

    public static class MonthlyReportItems
    {
        public const string Create = "monthly-report-items/{monthlyReportId:guid}";

        public const string Update = "monthly-report-items/{monthlyReportId:guid}/{monthlyReportItemId:guid}";

        public const string Updates = "monthly-report-items/{monthlyReportId:guid}/updates";

        public const string Delete = "monthly-report-items/{monthlyReportId:guid}/{monthlyReportItemId:guid}";
    }

    public static class MonthlyReports
    {
        public const string Get = "monthly-reports";

        public const string GetById = "monthly-reports/{monthlyReportId:guid}";

        public const string Create = "monthly-reports";

        public const string Update = "monthly-reports/{monthlyReportId:guid}";

        public const string Confirm = "monthly-reports/{monthlyReportId:guid}/confirm";

        public const string DashboardMonthlyReportBySales = "monthly-reports/dashboard/by-sales";

        public const string DashboardCustomerTracking = "monthly-reports/dashboard/customer-tracking";

        public const string GetByYearMonth = "monthlyreports/customers/by-year-month";

        public const string CreateForManufacturer = "monthly-reports/manufacturer";

        public const string UpdateForManufacturer = "monthly-reports/{monthlyReportId:guid}/manufacturer";
    }

    public static class OdooCommons
    {
        public const string GetStates = "odoo-commons/states";

        public const string GetStateById = "odoo-commons/states/{stateId:int}";

        public const string GetDistricts = "odoo-commons/districts";

        public const string GetDistrictById = "odoo-commons/districts/{districtId:int}";

        public const string GetWards = "odoo-commons/wards";

        public const string GetWardById = "odoo-commons/wards/{wardId:int}";
    }

    public static class OdooProducts
    {
        public const string Get = "odoo-products";
    }

    public static class OdooCustomers
    {
        public const string Get = "odoo-customers";
    }

    public static class Categories
    {
        public const string Get = "categories";

        public const string GetById = "categories/{categoryId:guid}";

        public const string Create = "categories";

        public const string Update = "categories/{categoryId:guid}";
    }

    /// <summary>
    ///     This class contains constants for authentication routes.
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        ///     The route for getting user profile information.
        /// </summary>
        /// <remarks>
        ///     This route is used to retrieve the user's profile information.
        /// </remarks>
        public const string Profile = "authentication/profile";

        /// <summary>
        ///     The route for logging in with SSO Vietmap.
        /// </summary>
        /// <remarks>
        ///     This route is used to authenticate the user with SSO Vietmap.
        /// </remarks>
        public const string LoginSSOVietmap = "authentication/login-sso-vietmap";

        /// <summary>
        ///     The route for logging in.
        /// </summary>
        /// <remarks>
        ///     This route is used to authenticate the user.
        /// </remarks>
        public const string Login = "authentication/login";

        /// <summary>
        ///     The route for registering a new user.
        /// </summary>
        /// <remarks>
        ///     This route is used to create a new user account.
        /// </remarks>
        public const string Register = "authentication/register";
    }

    /// <summary>
    ///     Contains constants for user-related API routes.
    /// </summary>
    public static class Users
    {
        /// <summary>
        ///     Route for retrieving all users.
        /// </summary>
        /// <remarks>
        ///     This route is used to retrieve a list of all users.
        /// </remarks>
        public const string Get = "users";

        /// <summary>
        ///     Route for retrieving a user's tree.
        /// </summary>
        public const string GetTree = "users/tree";

        public const string GetWithPlan = "users/plan/summary";

        /// <summary>
        ///     Route for retrieving a user by ID.
        /// </summary>
        /// <remarks>
        ///     This route is used to retrieve a specific user by their unique identifier.
        /// </remarks>
        public const string GetById = "users/{userId:guid}";

        /// <summary>
        ///     Route for updating a user.
        /// </summary>
        /// <remarks>
        ///     This route is used to update the information of a specific user.
        /// </remarks>
        public const string Update = "users/{userId:guid}";

        /// <summary>
        ///     Route for changing a user's password.
        /// </summary>
        /// <remarks>
        ///     This route is used to change the password of a specific user.
        /// </remarks>
        public const string ChangePassword = "users/{userId:guid}/change-password";

        /// <summary>
        ///     Route for sending a friendship request.
        /// </summary>
        /// <remarks>
        ///     This route is used to send a friendship request to a specific user.
        /// </remarks>
        public const string SendFriendshipRequest = "users/{userId:guid}/send-friendship-request";

        /// <summary>
        ///     Route for retrieving all user roles.
        /// </summary>
        /// <remarks>
        ///     This route is used to retrieve a list of all user roles.
        /// </remarks>
        public const string Role = "users/roles";
    }
}