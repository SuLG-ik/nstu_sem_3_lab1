using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApp1.Tests;

[TestClass]
public class ApplicationInternalExceptionTests
{
    [TestMethod]
    public void ApplicationInternalException_ShouldUseDefaultMessage_WhenMessageIsNull()
    {
        // Act
        var exception = new ApplicationInternalException();

        // Assert
        Assert.AreEqual("Internal application exception", exception.Message);
        Assert.IsNull(exception.InnerException);
    }

    [TestMethod]
    public void ApplicationInternalException_ShouldSetCustomMessage_WhenMessageIsProvided()
    {
        // Arrange
        var customMessage = "Custom error occurred";

        // Act
        var exception = new ApplicationInternalException(customMessage);

        // Assert
        Assert.AreEqual(customMessage, exception.Message);
        Assert.IsNull(exception.InnerException);
    }

    [TestMethod]
    public void ApplicationInternalException_ShouldSetInnerException_WhenInnerExceptionIsProvided()
    {
        // Arrange
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var exception = new ApplicationInternalException(innerException: innerException);

        // Assert
        Assert.AreEqual("Internal application exception", exception.Message);
        Assert.AreEqual(innerException, exception.InnerException);
    }

    [TestMethod]
    public void ApplicationInternalException_ShouldSetCustomMessageAndInnerException_WhenBothAreProvided()
    {
        // Arrange
        var customMessage = "Custom error occurred";
        var innerException = new InvalidOperationException("Inner exception");

        // Act
        var exception = new ApplicationInternalException(customMessage, innerException);

        // Assert
        Assert.AreEqual(customMessage, exception.Message);
        Assert.AreEqual(innerException, exception.InnerException);
    }
}