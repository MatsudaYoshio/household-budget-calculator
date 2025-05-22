# Household Budget Calculator

A sleek WPF application that reads payment data from PayPay CSV files and categorizes your spending for easy analysis.

---

## Requirements

* .NET 9 SDK

---

## Build Instructions

To build the application in Release mode, run the following command:

```bash
dotnet build --configuration Release
```

---

## Category Mapping Format

Define your spending categories using a simple `data.json` file with the following structure:

```json
{
  "ProductName": "Category"
}
```

Each product name from the PayPay CSV will be mapped to its corresponding category for aggregation.
