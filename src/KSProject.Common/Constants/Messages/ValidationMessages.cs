namespace KSProject.Common.Constants.Messages;

public static class ValidationMessages
{
    public static string MinimumLengthError(int minimumLength) => $"Minimum length is {minimumLength}.";

    public static string MaximumLengthError(int maximumLength) => $"Maximum length is {maximumLength}";
    
    public static string InvalidInputType(string inputType) => $"The input type is not valid, it should be {inputType}.";

    public static string InvalidTitle = "The title is not valid.";

    public static string InvalidPhoneNumber = "The phone number is not valid.";

    public static string InvalidPhoneNumberFormat = "The phone number format is not valid.";

    public static string InvalidEmail = "The email is not valid.";

    public static string InvalidEmailFormat = "The email format is not valid.";

    public static string InputShouldBeGreaterThan(int inputNumber) =>
        $"The input should be greater than {inputNumber}";

    public const string ShouldNotBeNull = "The value can not be null.";
    
    public const string ShouldNotBeEmpty = "The value can not be empty.";
    
    public const string ShouldNotBeEmptyOrWhiteSpace = "The value can not be empty or whitespace.";

    public const string ResourceCouldNotBeFound = "The resource could not be found.";

    public const string TheRecordCouldNotBeFound = "The record could not be found.";

    public static string LengthShouldBeBetween(int min, int max) => $"Input should be between {min} and {max}.";

    public const string PasswordAndConfirmationAreNotEqual = "Password and Confirmation are not equal.";

    public const string OldPasswordIsNotCorrect = "The old password is not correct";

    public const string TheListCouldNotBeEmpty = "The list could not be empty.";
    
    public const string TheListCouldNotBeNull = "The list could not be null.";

}