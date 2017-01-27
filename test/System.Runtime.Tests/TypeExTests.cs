using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Runtime.Tests
{
    [TestClass]
    public class TypeExTests
    {
        [TestMethod]
        public void TypeEx_GetInterface()
        {
            Type typeBar = typeof(Bar);
            Type typeFoo = typeof(Foo);

            Type foundType = Mock.System.TypeEx.GetInterface(typeBar, "Foo");
            Assert.IsNotNull(foundType);
            Assert.AreEqual(typeFoo.FullName, foundType.FullName);
            Assert.IsTrue(foundType.IsInterface);

            foundType = Mock.System.TypeEx.GetInterface(typeBar, "foo");
            Assert.IsNull(foundType);

            foundType = Mock.System.TypeEx.GetInterface(typeBar, "Qux");
            Assert.IsNull(foundType);

            foundType = Mock.System.TypeEx.GetInterface(typeBar, "foo", true);
            Assert.IsNotNull(foundType);
            Assert.AreEqual(typeFoo.FullName, foundType.FullName);
            Assert.IsTrue(foundType.IsInterface);

            foundType = Mock.System.TypeEx.GetInterface(typeBar, "qux", true);
            Assert.IsNull(foundType);
        }

        [TestMethod]
        public void TypeEx_GetMember()
        {
            Type typeBar = typeof(Bar);
            Type typeFoo = typeof(Foo);

            // Get field    ===================================================
            var members = Mock.System.TypeEx.GetMember(
                typeBar, "_field1",
                Reflection.MemberTypes.Field,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Field, members[0].MemberType);
            Assert.AreEqual("_field1", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "_field2",
                Reflection.MemberTypes.Field,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Field, members[0].MemberType);
            Assert.AreEqual("_field2", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "_field3",
                Reflection.MemberTypes.Field,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.NonPublic);

            Assert.IsNotNull(members);
            Assert.AreEqual(0, members.Length);

            // Get property     ===============================================
            members = Mock.System.TypeEx.GetMember(
                typeBar, "Field1",
                Reflection.MemberTypes.Property,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Property, members[0].MemberType);
            Assert.AreEqual("Field1", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "Field2",
                Reflection.MemberTypes.Property,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Property, members[0].MemberType);
            Assert.AreEqual("Field2", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "Field3",
                Reflection.MemberTypes.Property,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(0, members.Length);

            // Get method   ===================================================
            members = Mock.System.TypeEx.GetMember(
                typeBar, "SetField1",
                Reflection.MemberTypes.Method,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Method, members[0].MemberType);
            Assert.AreEqual("SetField1", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "SetField2",
                Reflection.MemberTypes.Method,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(1, members.Length);
            Assert.AreEqual(Reflection.MemberTypes.Method, members[0].MemberType);
            Assert.AreEqual("SetField2", members[0].Name);
            Assert.AreEqual(typeBar, members[0].DeclaringType);

            members = Mock.System.TypeEx.GetMember(
                typeBar, "SetField3",
                Reflection.MemberTypes.Method,
                Reflection.BindingFlags.Instance | Reflection.BindingFlags.Public);

            Assert.IsNotNull(members);
            Assert.AreEqual(0, members.Length);
        }

        interface Foo
        {
            int Field1 { get; }
            void SetField1(int value);
        }
        interface Qux { }
        class Bar : Foo
        {
            private int _field1;
            private string _field2;

            public int Field1
                => _field1;

            public string Field2
                => _field2;

            public void SetField1(int value)
                => _field1 = value;

            public void SetField2(string value)
                => _field2 = value;
        }
    }
}
