using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace Rhyous.UnitTesting
{
    /// <summary>
    /// An attribute used to decorate Unit Test methods to provide a list of test data and
    /// run the test for each test data in the list.
    /// </summary>
    public class XmlTestDataSourceAttribute : Attribute, ITestDataSource
    {
        private readonly Type _Type;
        private readonly string _File;
        private readonly string _TestNameProperty;

        /// <summary>A Func which allows for mocking File.ReadAllText in Unit Tests</summary>
        internal Func<string, string> FileReadAllTextMethod = File.ReadAllText;

        /// <summary>The Attribute constructor</summary>
        /// <param name="file">The file path</param>
        public XmlTestDataSourceAttribute(Type type, string file, string testNameProperty = null)
        {
            _Type = type;
            _File = file;
            _TestNameProperty = testNameProperty;
        }

        /// <summary>Gets the data. This is called by test methods.</summary>
        /// <param name="methodInfo">The test method passed in by test.</param>
        /// <returns></returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            if (!File.Exists(_File))
                throw new FileNotFoundException($"Could not find Xml file: {_File}.");
            if (!typeof(IEnumerable).IsAssignableFrom(_Type))
                throw new ArgumentException($"The type {_Type} must implement IEnumerable.");

            XmlSerializer xmlSerializer = new XmlSerializer(_Type);
            TextReader textReader = new StreamReader(_File);
            XmlTextReader xmlTextReader = new XmlTextReader(textReader);
            xmlTextReader.Read();
            var result = xmlSerializer.Deserialize(xmlTextReader) as IEnumerable;
            textReader.Close();

            foreach (var row in result)
                yield return new object[] { row };
        }

        /// <summary>
        /// Returns the name of the test.
        /// If your data model implements both ITestRunOrder and ITestName, the format is "{RunOrder}:{TestName}".
        /// If you data model implements only ITestRunOrder, the name is the format is "{RunOrder}". 
        /// If you data model implements only ITestName, the name is the format is "{TestName}".
        /// If neither are implemented, the test name returns null.
        /// </summary>
        /// <param name="methodInfo">The test method</param>
        /// <param name="data">The data passed into the test.</param>
        /// <returns>The name of the test being run.</returns>
        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (string.IsNullOrWhiteSpace(_TestNameProperty))
                return null;
            if (_TestNameProperty.Contains(","))
            {
                var values = _TestNameProperty.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(p => GetPropertyValue(data, p));
                return string.Join("|", values);
            }
            else
            {
                return GetPropertyValue(data, _TestNameProperty);
            }
        }

        private string GetPropertyValue(object[] data, string property)
        {
            var propInfo = data[0].GetType().GetProperty(property);
            if (propInfo is null)
                return null;
            return propInfo.GetValue(data[0], null) as string;
        }
    }
}