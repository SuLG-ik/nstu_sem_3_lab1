using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class ValidationExceptionTests
{
    [TestMethod]
    public void ValidationException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var tag = "TestTag";
        var message = "TestMessage";

        // Act
        var exception = new ValidationException(tag, message);

        // Assert
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationConvertException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var actual = "42";
        var expectedType = typeof(int);
        var tag = "TestTag";
        var message = "Invalid conversion";

        // Act
        var exception = new ValidationConvertException<string>(actual, expectedType, tag, message);

        // Assert
        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(expectedType, exception.ExpectedType);
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationLengthException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var actual = "abc";
        var minLength = 5;
        var maxLength = 10;
        var tag = "TestTag";
        var message = "Length is out of range";

        // Act
        var exception = new ValidationLengthException(actual, minLength, maxLength, tag, message);

        // Assert
        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(minLength, exception.MinLength);
        Assert.AreEqual(maxLength, exception.MaxLength);
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNotBlankException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var actual = "";
        var tag = "TestTag";
        var message = "Value cannot be blank";

        // Act
        var exception = new ValidationNotBlankException(actual, tag, message);

        // Assert
        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNotCourseInException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var actual = 42;
        var minValue = 0;
        var maxValue = 100;
        var tag = "TestTag";
        var message = "Value is not within range";

        // Act
        var exception = new ValidationNotCourseInException<int>(actual, minValue, maxValue, tag, message);

        // Assert
        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(minValue, exception.MinValue);
        Assert.AreEqual(maxValue, exception.MaxValue);
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationEqualsException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var actual = 42;
        var expected = 100;
        var tag = "TestTag";
        var message = "Values are not equal";

        // Act
        var exception = new ValidationEqualsException<int>(actual, expected, tag, message);

        // Assert
        Assert.AreEqual(actual, exception.Actual);
        Assert.AreEqual(expected, exception.Expected);
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }

    [TestMethod]
    public void ValidationNullException_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var tag = "TestTag";
        var message = "Value cannot be null";

        // Act
        var exception = new ValidationNullException(tag, message);

        // Assert
        Assert.AreEqual(tag, exception.Tag);
        Assert.AreEqual(message, exception.Message);
    }
}