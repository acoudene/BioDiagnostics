using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace BioDiagnostics.Data.EFCore.MongoDb.Tests;

public class TestOutputLoggerProvider : ILoggerProvider
{
  private readonly ITestOutputHelper _output;

  public TestOutputLoggerProvider(ITestOutputHelper output)
  {
    _output = output;
  }

  public ILogger CreateLogger(string categoryName)
  {
    return new TestOutputLogger(_output, categoryName);
  }

  public void Dispose() { }

  private class TestOutputLogger : ILogger
  {
    private readonly ITestOutputHelper _output;
    private readonly string _categoryName;

    public TestOutputLogger(ITestOutputHelper output, string categoryName)
    {
      _output = output;
      _categoryName = categoryName;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception? exception, Func<TState, Exception?, string> formatter)      
    {
      _output.WriteLine($"[{logLevel}] {_categoryName}: {formatter(state, exception)}");
    }
  }
}