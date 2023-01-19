# XMLValidator

XML Validator is a C# project that identifies whether or not an XML string is valid XML. It is designed to provide developers with a simple and easy way to validate XML strings without the use of System.XML or Regular Expressions.

##Features
- Validates syntax and structure of XML string.

##Requirements
.NET 7.0
Visual Studio 2020

##Usage

To use the XML Validator, simply call DetermineXml() with the XML string as the parameter. The method will return a boolean determining whether or not the XML string is valid.

```csharp
string xmlString = "<Design><Code>Hello World</Code></Design>";

bool isValid = XmlValidator.ValidateXml(xmlString);
```

