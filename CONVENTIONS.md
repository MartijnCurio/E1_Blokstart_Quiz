### Naming Conventions
- **Variables and Functions**: Use `camelCase` for variable and function names.
    - Example: `let userName = "John";`
- **Constants**: Use `UPPER_SNAKE_CASE` for constant variables.
    - Example: `const MAX_RETRIES = 5;`
- **Classes**: Use `PascalCase` for class names.
    - Example: `class UserProfile { }`
- **File Names**: Use `kebab-case` for file names.
    - Example: `user-profile.js`

### Formatting
- Use 2 or 4 spaces for indentation (based on project preference).
- Limit lines to 80 or 100 characters for better readability.
- Add a newline at the end of each file.
- Avoid trailing spaces at the end of lines.
- Use semicolons consistently (or omit based on your language preferences).

### Comments
- **Single-line comments**: Use `//` for single-line comments.
- **Multi-line comments**: Use `/* */` for multi-line comments.
- **Docstrings/Documentation comments**: Use language-specific docstring format for functions, classes, and modules.
  - Example for JavaScript/TypeScript:
    ```javascript
    /**
     * Function to add two numbers.
     * @param {number} a - The first number.
     * @param {number} b - The second number.
     * @returns {number} The sum of the two numbers.
     */
    function add(a, b) {
      return a + b;
    }
    ```

## Git Commit Messages
Follow the format below for commit messages:
- Use a short and concise message, ideally within 50 characters.
- Use imperative mood ("Add", "Fix", "Remove" instead of "Added", "Fixed", etc.).
- **Structure**:
