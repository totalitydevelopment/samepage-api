namespace Nok.Core.Validators;

public interface IBaseValidationModel
{
    public void Validate(object validator, IBaseValidationModel model);
}
