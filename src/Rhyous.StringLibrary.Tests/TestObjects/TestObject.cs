using System;
using System.Collections.Generic;

namespace Rhyous.StringLibrary.Tests
{
    public class TestObject
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public SubTestObject Sub { get; set; }
    }

    public class SubTestObject
    {
        public int Id { get; set; }
        public string SubValue { get; set; }
    }

    public class TestObjectGeneric<T>
    {
        public int Id { get; set; }
        public T Value { get; set; }
        public SubTestObjectGeneric<T> Sub { get; set; }
    }

    public class SubTestObjectGeneric<T>
    {
        public int Id { get; set; }
        public T SubValue { get; set; }
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

    public class ObjectThatThrowsOnSet<T>
    {
        public ObjectThatThrowsOnSet(T value) { _Value = value; }

        public T Value
        {
            get { return _Value; }
            set { throw new NotImplementedException(); }
        } private T _Value;
    }

    public class ObjectThatThrowsOnGet<T>
    {
        public ObjectThatThrowsOnGet(T value) { _Value = value; }

        public T Value
        {
            get { throw new NotImplementedException(); }
            set { _Value = value; }
        } private T _Value;
    }

    public enum TestEnum
    {
        Test1,
        Test2,
        Test3,
        Test4
    }
}
