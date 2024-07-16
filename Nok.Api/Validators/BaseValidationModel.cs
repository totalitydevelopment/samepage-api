using FluentValidation;

namespace Nok.Api.Validators;

public abstract class BaseValidationModel<T> : IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel modelObj)
    {
        var instance = (IValidator<T>)validator;
        instance.ValidateAndThrow((T)modelObj);
    }
}
