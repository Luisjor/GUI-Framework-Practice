namespace Todoly.Core.Helpers;

public class ConfigModel
{
    public static readonly int DriverImplicitTimeout = ConfigBuilder.Instance.GetInt(
        "ui",
        "DriverImplicitTimeout"
    );

    public static readonly int DriverExplicitTimeout = ConfigBuilder.Instance.GetInt(
        "ui",
        "DriverExplicitTimeout"
    );

    public static readonly string HostUrl = ConfigBuilder.Instance.GetString("ui", "HostUrl");
    public static readonly string WEB_USERNAME = ConfigBuilder.Instance.GetString("WEB_USERNAME");
    public static readonly string WEB_PASS = ConfigBuilder.Instance.GetString("WEB_PASS");
    public static readonly string DriverType = ConfigBuilder.Instance.GetString("ui", "DriverType");

    public static readonly string DriverMode = ConfigBuilder.Instance.GetString("ui", "DriverMode");

}
