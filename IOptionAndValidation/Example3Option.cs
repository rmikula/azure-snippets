namespace IOption.Validators;

public class Example3Option
{
    public const string Section = "Example3";

    public required LogLevel LogLevel { get; init; }
    
    public required int Retries { get; init; }
}