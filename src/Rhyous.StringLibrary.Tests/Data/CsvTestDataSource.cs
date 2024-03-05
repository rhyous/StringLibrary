using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhyous.EasyCsv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Rhyous.UnitTesting
{
    /// <summary>
    /// An attribute used to decorate Unit Test methods to provide a list of test data and
    /// run the test for each test data in the list.
    /// </summary>
    public class CsvTestDataSourceAttribute : Attribute, ITestDataSource
    {
        private readonly string _File;

        /// <summary>A Func which allows for mocking File.ReadAllText in Unit Tests</summary>
        internal Func<string, string> FileReadAllTextMethod = File.ReadAllText;

        /// <summary>The Attribute constructor</summary>
        /// <param name="file">The file path</param>
        public CsvTestDataSourceAttribute(string file)
        {
            _File = file;
        }

        /// <summary>Gets the data. This is called by test methods.</summary>
        /// <param name="methodInfo">The test method passed in by test.</param>
        /// <returns></returns>
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            if (!File.Exists(_File))
                throw new FileNotFoundException($"Could not find csv file: {_File}.");

            var csv = new Csv(_File);
            foreach (var row in csv.Rows)
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
            return ((Row<string>)data[0])[0];
        }
    }
}