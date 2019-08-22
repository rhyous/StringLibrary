# StringLibrary
A library to make string functions, such as trimming and much more, easier.

## Why you should use this library
It will save you time. Yes, you can write your own string extension methods, but you also have to write unit tests for them. This library is fully unit tested so you can include it and move on.

You get:
1. Consistent string extension methods between projects
2. Faster time to code, as you don't have to roll your own extension methods and then either lack code coverage or have to unit test them.
3. The same code in .NET Framework and .Net Core/.NET standard.

## What is in this library?

1. String Capitalization extension methods - 
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Capitalization/StringCapitalizationExtensions.cs
You can capitalize things easily.

2. String Comparison extension methods - 
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Comparison/StringComparisonExtensions.cs
This has the contains with the ability to ignore case, that is just missing form .Net.

3. String Conversion extension methods
To Primitives https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Convers
ion/PrimitiveStringExtensions.cs 
Easily convert strings to and from primitives, with the option for a default value if the string conversion fails.

To Stream https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Conversion/StreamStringExtensions.cs
Easily convert to and from a Stream.

To enum https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Enum/StringEnumExtensions.cs
Easily convert to and from an enum.

4. AllowedString
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Enum/AllowedString.cs
A class that is a wrapper to a string and allows for you to define a list of allowed strings.

5. String to Expression extensions:
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Expression/PropertyNameLambdaExtensions.cs
This is pretty cool. If you have a property name and want to convert it to an Expression, you can do so.

6. Remove Diacritics from a string:
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Globalization/GlobalizationStringExtensions.cs
This allows you to create dictionaries

7. A pluralization library.
https://github.com/rhyous/StringLibrary/tree/master/src/Rhyous.StringLibrary.Shared/Pluralization
This makes it easy to do pluralization, include custom pluralization. If a word is missing, it is pretty easy to add to this library.

8. A crypto random string generator
https://github.com/rhyous/StringLibrary/blob/master/src/Rhyous.StringLibrary.Shared/Random/CryptoRandomString.cs

9. Enhanced trimming abilities
https://github.com/rhyous/StringLibrary/tree/master/src/Rhyous.StringLibrary.Shared/Trim
The ability to trim all strings inside a complex object, which can and will trim nested complex objects as well. This is very useful for apis and inputs.

Have you heard of [The Oft Forgotten Middle Trim](http://www.rhyous.com/2016/03/18/the-oft-forgotten-middle-trim/), you can trim left and right and middle, with the TrimAll() extension method.

10. String wrapping extensions. 
https://github.com/rhyous/StringLibrary/tree/master/src/Rhyous.StringLibrary.Shared/Wrap
Easily quote strings or wrap them in brackets or parentheses or xml tags.

