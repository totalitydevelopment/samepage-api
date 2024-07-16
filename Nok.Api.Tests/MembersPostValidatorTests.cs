using FluentValidation.TestHelper;
using Nok.Api.Controllers;
using Nok.Api.Validators.Members;

namespace Nok.Api.Tests.Validators.Members;

[TestClass]
public class MembersPostValidatorTests
{
    private MembersPostValidator? _validator;

    [TestInitialize]
    public void Setup()
    {
        _validator = new MembersPostValidator();
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Is_Null()
    {
        var model = new CreateMemberRequest { FirstName = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var model = new CreateMemberRequest { FirstName = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_FirstName_Is_Valid()
    {
        var model = new CreateMemberRequest { FirstName = "John" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Exceeds_MaxLength()
    {
        var model = new CreateMemberRequest { FirstName = new string('a', 101) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }    

    [TestMethod]
    public void Should_Have_Error_When_LastName_Is_Null()
    {
        var model = new CreateMemberRequest { LastName = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [TestMethod]
    public void Should_Have_Error_When_LastName_Is_Empty()
    {
        var model = new CreateMemberRequest { LastName = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_LastName_Is_Valid()
    {
        var model = new CreateMemberRequest { LastName = "Doe" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.LastName);
    }

    [TestMethod]
    public void Should_Have_Error_When_LastName_Exceeds_MaxLength()
    {
        var model = new CreateMemberRequest { LastName = new string('a', 101) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
    }    

    [TestMethod]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var model = new CreateMemberRequest { Email = "invalid-email" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_Email_Is_Valid()
    {
        var model = new CreateMemberRequest { Email = "john.doe@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [TestMethod]
    public void Should_Have_Error_When_Email_Is_Null()
    {
        var model = new CreateMemberRequest { Email = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [TestMethod]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var model = new CreateMemberRequest { Email = string.Empty };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [TestMethod]
    public void Should_Have_Error_When_Email_Exceeds_MaxLength()
    {
        var model = new CreateMemberRequest { Email = new string('a', 301) + "@example.com" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_MiddleName_Is_Valid()
    {
        var model = new CreateMemberRequest { MiddleName = "A" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
    }

    [TestMethod]
    public void Should_Have_Error_When_MiddleName_Exceeds_MaxLength()
    {
        var model = new CreateMemberRequest { MiddleName = new string('a', 101) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.MiddleName);
    }
}
