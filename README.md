[![Nuget](https://img.shields.io/nuget/v/Pixsys.Library.Token.TokenManager)](https://www.nuget.org/packages/Pixsys.Library.Token.TokenManager)

# Token Manager

This manager will generate random or patterned strings depending on your preferences: uppercase letters, lowercase letters, numbers and symbols.

## 1. Installation

### 1.1 Register the service in `Program.cs`

```csharp
using Pixsys.Library.Token.TokenManager;


var builder = WebApplication.CreateBuilder(args);

_ = builder.AddTokenManager();

```
## 2. Usage

### 2.1 Inject the service into your controller

```csharp
private readonly ITokenManager _tokenManager;

public MyController(ITokenManager tokenManager)
{
    _tokenManager = tokenManager;
}
```

### 2.2 Manager Methods

```csharp
var randomToken = _tokenManager.Generate(true, false, true, true, 20); // Output example : be(/j&2kg=)-++fi(+m=
var patternedToken = _tokenManager.GenerateFromPattern("LLdd##00"); // Output example : RXvb!#46
```