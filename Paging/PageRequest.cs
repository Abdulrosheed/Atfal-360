using System.Text.Json.Serialization;

namespace Atfal360.Paging;

public record PageRequest
{
    [JsonIgnore]
    public bool UsePaging {get;set;} =  true;
    public int PageSize { get; init; } = 20;
    public int Page { get; init; } = 1;
}