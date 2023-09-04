namespace UrlShortener.Extensions;

public static class UriExtensions
{
    public static bool IsValidUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, result: out _);
}
