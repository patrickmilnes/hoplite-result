# How to use

## Variable creation

Results can be created as such

```csharp
var result = Result<string, MyError>.Ok(expectedMessage);
```

with more specific types for `ServiceResult` and `TraceResult`

```csharp
var result = ServiceResult<string, MyError>.Ok(value, statusCode);
var result = TraceResult<string, MyError>.Ok(id, "Hello");
```

## Pattern Matching

### Result Type

A single variable pattern match can be performed

```csharp
var output = result.Match(
    value => value,
    err => err.Message
);
```

### ServiceResult or TraceResult

Two methods can be performed

```csharp
    var (output, outputStatus) = result.Match(
            (val, status) => (val, status),
            (err, status) => (err.Message, status));

        Assert.True(result.IsOk);
        Assert.Equal(value, output);
        Assert.Equal(statusCode, outputStatus);
```

or

```csharp
        var output = result.Match(
            (traceId, value) => value,
            (traceId, err) => err.Message);

        // Assert
        Assert.True(result.IsOk);
        Assert.Equal("Hello", output);
        Assert.Equal(id, result.TraceId);
```
