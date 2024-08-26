### Naming Conventions
- **Classes, Interfaces, and Structs**: Use `PascalCase` for class, interface, and struct names. Prefix interfaces with an `I`.
  - Example: `class UserProfile { }`, `interface IAuthenticationService { }`
- **Methods**: Use `PascalCase` for method names.
  - Example: `public void ValidateUser() { }`
- **Properties**: Use `PascalCase` for property names.
  - Example: `public string FirstName { get; set; }`
- **Fields**: Use `_camelCase` for private fields, prefixed with an underscore (`_`).
  - Example: `private string _firstName;`
- **Local Variables and Parameters**: Use `camelCase` for local variables and method parameters.
  - Example: `string userName = "John";`
- **Constants**: Use `PascalCase` for constant fields.
  - Example: `private const int MaxRetries = 3;`

### Formatting
- **Indentation**: Use 4 spaces for indentation (no tabs).
- **Line Length**: Limit lines to 100 characters.
- **Curly Braces**: 
  - Place opening curly braces on a new line for types and members.
  - Place opening curly braces on the same line for control statements (e.g., `if`, `for`, etc.).
  - Example:
    ```csharp
    public class UserService
    {
        public void CreateUser(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
        }
    }
    ```
- **Namespace Declaration**: Use `PascalCase` for namespaces and ensure they match the folder structure.
  - Example: `namespace MyProject.Services { }`

### Comments
- **Single-line comments**: Use `//` for single-line comments.
- **Multi-line comments**: Use `/* */` for multi-line comments.
- **XML Documentation Comments**: Use `///` for XML documentation comments to provide detailed information about classes, methods, and parameters.
  - Example:
    ```csharp
    /// <summary>
    /// Validates the user input.
    /// </summary>
    /// <param name="userName">The username to validate.</param>
    /// <returns>True if valid, otherwise false.</returns>
    public bool ValidateUser(string userName)
    {
        // Implementation
    }
    ```

### Usings
- **Sort and Remove Usings**: Organize `using` statements with System directives first, followed by other namespaces, and ensure unused usings are removed.
  - Example:
    ```csharp
    using System;
    using System.Collections.Generic;
    using MyProject.Services;
    ```

## Git Commit Messages
- **Format**: Commit messages should be concise and use the imperative mood (e.g., "Add feature", "Fix bug").
- **Structure**:
