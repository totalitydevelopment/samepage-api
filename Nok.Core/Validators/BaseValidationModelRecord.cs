using FluentValidation;

namespace Nok.Core.Validators;

public abstract record BaseValidationModelRecord<T> : IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel modelObj)
    {
        var instance = (IValidator<T>)validator;
        instance.ValidateAndThrow((T)modelObj);
    }
}

public abstract record BaseValidationModel<T> : IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel modelObj)
    {
        var instance = (IValidator<T>)validator;
        instance.ValidateAndThrow((T)modelObj);
    }
}
