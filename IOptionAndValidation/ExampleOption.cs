namespace IOption;

public class ExampleOption
{
    public const string Section = "Example";

    public required LogLevel LogLevel { get; init; }
    
    public required int Retries { get; init; }
}