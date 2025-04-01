using Application.Core.Abstractions.Messaging;
using Domain.Core.Primitives.Maybe;
using OdooPayment.Contract.OdooCustomers;

namespace OdooPayment.Application.Customers.Queries.Odoo.Customers
{
    public sealed class GetOdooCustomerQuery : IQuery<Maybe<CustomerOdooModel>>
    {
        public GetOdooCustomerQuery(string? search, string? invoiceSearch, bool? isSO, bool? isCustomer)
        {
            Search = search;
            InvoiceSearch = invoiceSearch;
            IsSO = isSO;
            IsCustomer = isCustomer;
        }

        public string? Search { get; }

        public string? InvoiceSearch { get; }

        public bool? IsSO { get; }

        public bool? IsCustomer { get; }
    }
}
