using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nok.Core.Validators;

public class ModelValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var actionArgument in context.ActionArguments)
        {
            if (actionArgument.Value is IBaseValidationModel model)
            {
                ValidateModel(context, actionArgument.Value.GetType(), model);

                // Validate properties
                foreach (var propertyInfo in model.GetType().GetProperties())
                {
                    var property = propertyInfo.GetValue(model);

                    if (property is IBaseValidationModel subModel)
                    {
                        ValidateModel(context, propertyInfo.PropertyType, subModel);
                    }
                }
            }
        }

        base.OnActionExecuting(context);
    }

    private static void ValidateModel(ActionExecutingContext context, Type modelType, IBaseValidationModel model)
    {
        var genericType = typeof(IValidator<>).MakeGenericType(modelType);
        var validator = context.HttpContext.RequestServices.GetService(genericType);

        if (validator != null)
        {
            model.Validate(validator, model);
        }
    }
}
