namespace Odoo.Contract.Services.Abstracts;

public class OdooResponse<T> where T : class
{
    public T[] Data { get; set; } = [];

    public int Count { get; set; }
}
