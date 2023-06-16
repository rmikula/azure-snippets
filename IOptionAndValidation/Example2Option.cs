using System.ComponentModel.DataAnnotations;

namespace IOption;

public class Example2Option
{
    public const string Section = "Example2";

    [EnumDataType(typeof(LogLevel))]
    public required LogLevel LogLevel { get; init; }
    
    [Range(1,9)]
    public required int Retries { get; init; }
}