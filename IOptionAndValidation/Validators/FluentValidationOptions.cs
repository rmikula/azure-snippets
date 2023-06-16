using FluentValidation;
using Microsoft.Extensions.Options;

namespace IOption.Validators;

public static class OptionsBuilderFluentValidationExtensions
{

    public static OptionsBuilder<TOptions> ValidateFluently<TOptions>(this OptionsBuilder<TOptions> builder) where TOptions : class
    {
        // builder.Services.AddSingleton<IValidateOptions<TOptions>>(new FluentValidationOptions<TOptions>(builder.Name));
        
        builder.Services.AddSingleton<IValidateOptions<TOptions>>(sp =>
        {
            var validator = sp.GetRequiredService<IValidator<TOptions>>();
            return new FluentValidationOptions<TOptions>(builder.Name, validator);
        });
        
        return builder;
    }
}

public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
    private readonly IValidator<TOptions> _validator;
    public string? Name { get; }
    
    public FluentValidationOptions(string? name, IValidator<TOptions> validator)
    {
        _validator = validator;
        Name = name;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        var result = _validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var errors = result.Errors.Select(x =>
            $"Options validation fail for '{x.PropertyName}' with error: {x.ErrorMessage} ");

        return ValidateOptionsResult.Fail(errors);
    }
}