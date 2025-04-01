namespace Odoo.Contract.Services.Abstracts
{
    public interface IOdooRequest
    {
        string _path { get; }

        string GetPath();
    }
}
