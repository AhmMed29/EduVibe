using EduVibe.Tests.Helpers;

namespace EduVibe.Tests;

public class LogAnalyzerTests
{
    private readonly LogAnalyzer _analyzer;
    
    public LogAnalyzerTests()
    {
        _analyzer =  new();  
    }
    
    [Fact]
    public void IsValidLogFileName_BadExtension_ReturnsFalse()
    {
        bool result = _analyzer.IsValidLogFileName("filewithbadextension.foo");
        Assert.False(result);
    }

    [Fact]
    public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
    {
        bool result = _analyzer.IsValidLogFileName("filewithgoodextension.TXT");
        Assert.True(result);
    }

    [Fact]
    public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
    {
        bool result = _analyzer.IsValidLogFileName("filewithgoodextension.txt");
        Assert.True(result);
    }
    
    [Theory]
    [InlineData("file.TXT", true)]
    [InlineData("file.txt", true)]
    [InlineData("file.foo", false)]
    public void IsValidLogFileName_VariousExtensions_ReturnsExpected(string fileName, bool expected)
    {
        bool result = _analyzer.IsValidLogFileName(fileName);
        Assert.Equal(expected, result);
    }

    
    // the 2 phase : the Expected Exceptions (Assert.Throws<T>)
    [Fact]
    public void IsValidFileName_EmptyFileName_ThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _analyzer.IsValidLogFileName(string.Empty));
        Assert.Equal("No filename provided!", ex.Message);
    }
    
    
}