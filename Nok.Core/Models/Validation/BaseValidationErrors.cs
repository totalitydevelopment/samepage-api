using Newtonsoft.Json;

namespace Nok.Core.Models.Validation;

public class BaseValidationErrors
{
    [JsonProperty("message")]
    public string? Message { get; init; }
    [JsonProperty("validationErrors")]
    public List<string>? ValidationErrors { get; init; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
