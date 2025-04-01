namespace Odoo.Infrastructure.Odoo.Settings
{
    internal sealed class OdooServicesSetting
    {
        public const string SettingKey = "OdooAPI";

        public string BaseUrl { get; set; } = null!;

        public string AccessToken { get; set; } = null!;
    }
}
