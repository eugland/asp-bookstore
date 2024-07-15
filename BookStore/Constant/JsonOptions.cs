using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace BookStore.Constant;

public class JsonOptions
{
    public static readonly JsonSerializerOptions CaseNeutralCamelJson = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
    };
}
