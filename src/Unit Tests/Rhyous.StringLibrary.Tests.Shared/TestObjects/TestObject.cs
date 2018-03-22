using System.Collections.Generic;

namespace Rhyous.StringLibrary.Tests
{
    public class TestObject
    {
        public int Id { get; set; }
        public string String1 { get; set; }
        public SubTestObject Sub { get; set; }
    }

    public class SubTestObject
    {
        public int Id { get; set; }
        public string String2 { get; set; }
    }

    public class Getter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Get { get { return " This cannot be set and cannot be trimmed. "; } }
    }

    public class ObjectList : List<object>
    {
        public string Name { get; set; }
    }

    public class ContainsStringArray
    {
        public string Name { get; set; }
        public string[] Array { get; set; }
        }

    public class ParentOfDictionary
    {
        public string Name { get; set; }
        public Dictionary<string, object> Map { get; set; } = new Dictionary<string, object>();
    }

    public class ObjectProperties
    {
        public string Name { get; set; }
        public object StringAsObject { get; set; }
    }

    public enum TestEnum
    {
        Test1,
        Test2,
        Test3,
        Test4
    }
}
