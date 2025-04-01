namespace MOT.Infrastructure.VietmapLive.Settings;

public class VietmapLiveSetting
{
    public const string SettingKey = "VietmapLive";

    public string Url { get; set; } = null!;

    public string CheckSmsPath { get; set; } = null!;
}