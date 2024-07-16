﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Nok.Api.Validators;

public class ModelValidatorAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var actionArgument in context.ActionArguments)
        {
            if (actionArgument.Value is IBaseValidationModel model)
            {
                var modelType = actionArgument.Value.GetType();
                var genericType = typeof(IValidator<>).MakeGenericType(modelType);
                var validator = context.HttpContext.RequestServices.GetService(genericType);

                if (validator != null)
                {
                    model.Validate(validator, model);
                }
            }
        }

        base.OnActionExecuting(context);
    }
}
