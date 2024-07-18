using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Nok.Core.Models;
using Nok.Core.Validators;
using Nok.Core.Validators.Members;

namespace Nok.Api.Tests.Validators.Members;

[TestClass]
public class ModelValidatorAttributeTests
{
    private readonly NameDto _validNameDto = new NameDto(null, "firstName", null, "surname");
    private readonly ContactDetailsDto _validContactDetailsDto = new ContactDetailsDto("email@email.com", null, null, null);
    private readonly VehicleDto _validVehicleDto = new VehicleDto("A123ABC", "Make", null, null, null);
    private readonly DateOfBirthDto _validDateOfBirthDto = new DateOfBirthDto(1950, 1, 1);
    private readonly AddressDto _validAddressDto = new AddressDto("Address1", null, "Town", "AB12 3CD", null);
    private readonly string? _validUrl = null;

    [TestMethod]
    public void ModelValidatorAttribute_NestedBaseValidationModel_ValidatorsPass()
    {
        //Arrange
        var memberRequest = new MemberRequest(
            _validNameDto,
            _validContactDetailsDto,
            _validVehicleDto,
            _validDateOfBirthDto,
            _validAddressDto,
            _validUrl);

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(_ => _.GetService(typeof(IValidator<NameDto>)))
            .Returns(new NameDtoValidator());

        ActionExecutingContext context = CreateControllerActionExecutionContext(memberRequest, serviceProviderMock);

        var sut = new ModelValidatorAttribute();

        //Act & Assert
        sut.OnActionExecuting(context);

        // No exception is a pass
    }

    [TestMethod]
    public void ModelValidatorAttribute_NestedBaseValidationModel_ValidatorFails()
    {
        //Arrange
        var memberRequest = new MemberRequest(
            _validNameDto with
            {
                FirstName = null! // Invalid first name
            },
            _validContactDetailsDto,
            _validVehicleDto,
            _validDateOfBirthDto,
            _validAddressDto,
            _validUrl);

        var serviceProviderMock = new Mock<IServiceProvider>();
        serviceProviderMock.Setup(_ => _.GetService(typeof(IValidator<NameDto>)))
            .Returns(new NameDtoValidator());

        ActionExecutingContext context = CreateControllerActionExecutionContext(memberRequest, serviceProviderMock);

        var sut = new ModelValidatorAttribute();

        //Act & Assert
        Assert.ThrowsException<ValidationException>(() => sut.OnActionExecuting(context));
    }

    private static ActionExecutingContext CreateControllerActionExecutionContext(MemberRequest memberRequest, Mock<IServiceProvider> serviceProviderMock)
    {
        var httpContext = new DefaultHttpContext()
        {
            RequestServices = serviceProviderMock.Object,
        };

        var context = new ActionExecutingContext(
            new ActionContext(
                httpContext: httpContext,
                routeData: new RouteData(),
                actionDescriptor: new ActionDescriptor(),
                modelState: new ModelStateDictionary()
            ),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>()
            {
                ["memberRequest"] = memberRequest
            },
            new Mock<Controller>().Object);
        return context;
    }
}
