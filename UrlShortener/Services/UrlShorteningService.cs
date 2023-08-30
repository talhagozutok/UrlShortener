using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Services;

public class UrlShorteningService
{
    public const byte NumberOfCharsInShortLink = 7;
    private const string ShortenUrlAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private readonly Random _random = new Random();
    private readonly ApplicationDbContext _dbContext;

    public UrlShorteningService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateUniqueCode()
    {
        var codeChars = new char[NumberOfCharsInShortLink];

        while (true)
        {
            for (int i = 0; i < NumberOfCharsInShortLink; i++)
            {
                var randomIndex = _random.Next(ShortenUrlAlphabet.Length);
                codeChars[i] = ShortenUrlAlphabet[randomIndex];
            }

            var code = new string(codeChars);

            if (!await _dbContext.ShortenedUrls.AnyAsync(s => s.Code == code))
            {
                return code;
            }
        }
    }
}
