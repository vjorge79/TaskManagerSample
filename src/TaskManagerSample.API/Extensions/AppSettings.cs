namespace TaskManagerSample.API.Extensions;

public class AppSettings
{
    public string Secret { get; set; }

    public int HoursExpiration { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}