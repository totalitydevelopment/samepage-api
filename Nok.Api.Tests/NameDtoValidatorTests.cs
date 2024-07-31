using FluentValidation.TestHelper;
using SamePage.Core.Models;
using SamePage.Core.Validators.Members;

namespace SamePage.Api.Tests.Validators.Members;

[TestClass]
public class NameDtoValidatorTests
{
    private NameDtoValidator? _validator;
    private NameDto _validNameDto = new NameDto("ValidTitle", "ValidFirstName", "ValidMiddleName", "ValidSurname");

    [TestInitialize]
    public void Setup()
    {
        _validator = new NameDtoValidator();
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Is_Null()
    {
        var model = _validNameDto with
        {
            FirstName = null!
        };

        var result = _validator!.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var model = _validNameDto with
        {
            FirstName = string.Empty
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_FirstName_Is_Valid()
    {
        var model = _validNameDto with
        {
            FirstName = "John"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Have_Error_When_FirstName_Exceeds_MaxLength()
    {
        var model = _validNameDto with
        {
            FirstName = new string('a', 101)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [TestMethod]
    public void Should_Have_Error_When_SurnameName_Is_Null()
    {
        var model = _validNameDto with
        {
            Surname = null!
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Surname);
    }

    [TestMethod]
    public void Should_Have_Error_When_Surname_Is_Empty()
    {
        var model = _validNameDto with
        {
            Surname = string.Empty
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Surname);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_Surname_Is_Valid()
    {
        var model = _validNameDto with
        {
            Surname = "Doe"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Surname);
    }

    [TestMethod]
    public void Should_Have_Error_When_Surname_Exceeds_MaxLength()
    {
        var model = _validNameDto with
        {
            Surname = new string('a', 101)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Surname);
    }

    //[TestMethod]
    //public void Should_Have_Error_When_Email_Is_Invalid()
    //{
    //    var model = new CreateMemberRequest { Email = "invalid-email" };
    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Email);
    //}

    //[TestMethod]
    //public void Should_Not_Have_Error_When_Email_Is_Valid()
    //{
    //    var model = new CreateMemberRequest { Email = "john.doe@example.com" };
    //    var result = _validator.TestValidate(model);
    //    result.ShouldNotHaveValidationErrorFor(x => x.Email);
    //}

    //[TestMethod]
    //public void Should_Have_Error_When_Email_Is_Null()
    //{
    //    var model = new CreateMemberRequest { Email = null };
    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Email);
    //}

    //[TestMethod]
    //public void Should_Have_Error_When_Email_Is_Empty()
    //{
    //    var model = new CreateMemberRequest { Email = string.Empty };
    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Email);
    //}

    //[TestMethod]
    //public void Should_Have_Error_When_Email_Exceeds_MaxLength()
    //{
    //    var model = new CreateMemberRequest { Email = new string('a', 301) + "@example.com" };
    //    var result = _validator.TestValidate(model);
    //    result.ShouldHaveValidationErrorFor(x => x.Email);
    //}

    [TestMethod]
    public void Should_Not_Have_Error_When_MiddleName_Is_Valid()
    {
        var model = _validNameDto with
        {
            MiddleName = "A"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.MiddleName);
    }

    [TestMethod]
    public void Should_Have_Error_When_MiddleName_Exceeds_MaxLength()
    {
        var model = _validNameDto with
        {
            MiddleName = new string('a', 101)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.MiddleName);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_Title_Is_Valid()
    {
        var model = _validNameDto with
        {
            Title = "Miss"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }

    [TestMethod]
    public void Should_Have_Error_When_Title_Exceeds_MaxLength()
    {
        var model = _validNameDto with
        {
            Title = new string('a', 101)
        };

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
