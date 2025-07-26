# 🎯 Household Budget Calculator (.NET 9 WPF) Style Guide

**Purpose**  
This style guide follows standard C# conventions to ensure readability, maintainability, and consistency throughout the project. It is designed to support automated code review using Gemini Code Assist and to provide a unified development experience for all team members.

---

## 📌 Coding Standards

- This project is based on the **[official Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)**.
- **Code style must follow the `.editorconfig` configuration**. All formatting rules such as indentation, naming, and line breaks are defined in `.editorconfig` and are automatically applied by supported IDEs and tools such as `dotnet format`.

---

## ✅ Coding Rules (Design Policies)

### Naming and Responsibilities
- Use `PascalCase` for type names, method names, and property names.
- Use `camelCase` for local variables and method parameters.
- Use `_camelCase` for private fields (e.g., `_totalAmount`).
- Classes must follow the **Single Responsibility Principle (SRP)**, and shared logic should be extracted into helper classes as needed.
- Separate UI event handling from logic by strictly adhering to the **MVVM pattern**.

### ✅ Use Named Arguments When a Method Has Multiple Parameters
- ✅ Recommended:  
  `PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);`
- ❌ Not Recommended:  
  `PrintOrderDetails("Red Mug", "Gift Shop", 31);`

Named arguments improve clarity, prevent argument order mistakes, and enhance code reviewability.

---

## ✅ UI Layer (XAML)

- Use meaningful `PascalCase` names for `x:Name` attributes (e.g., `MonthlyIncomeTextBox`).
- Avoid placing logic in code-behind; instead, **delegate processing to the ViewModel**.
- Use `x:DataType` to enable static type checking in bindings.
- Centralize styles and control templates (e.g., `PrimaryButtonStyle`) for reuse and consistency.

---

## ✅ Gemini Code Assist Instructions

- Follow this style guide when performing code reviews.
- Base comments and suggestions on `.editorconfig` settings and Microsoft’s coding conventions.
- Provide feedback in accordance with C#/.NET/.NET 9/WPF/MVVM best practices.
- If code does not comply with this guide, provide clear feedback and specific examples for correction.