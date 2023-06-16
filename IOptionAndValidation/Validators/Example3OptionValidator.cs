using FluentValidation;

namespace IOption.Validators;

public class Example3OptionValidator : AbstractValidator<Example3Option>
{
    public Example3OptionValidator()
    {

        RuleFor(x => x.LogLevel)
            .IsInEnum()
            .Must(level => level is LogLevel.Debug or LogLevel.Information or LogLevel.None);
      
        RuleFor(x => x.Retries)
            .InclusiveBetween(1, 9);
    }
}