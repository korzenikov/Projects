using System;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class CustomDictionaryTest
    {
        [TestMethod]
        public void AddTest()
        {
            var customDictionary = new CustomDictionary<string, int>();
            customDictionary.Add("Ivan", 1);
            customDictionary.Add("Stepan", 2);
            customDictionary.Add("Nikolay", 3);

            customDictionary.Invoking(dictionary => dictionary.Add("Nikolay", 4)).ShouldThrow<ArgumentException>();

            customDictionary["Ivan"].Should().Be(1);
            customDictionary["Stepan"].Should().Be(2);
            customDictionary["Nikolay"].Should().Be(3);

            customDictionary.Invoking(dictionary => { int value = customDictionary["Peter"]; }).ShouldThrow<ArgumentException>();

            customDictionary["Nikolay"] = 4;

            customDictionary["Nikolay"].Should().Be(4);

            customDictionary.Remove("Nikolay");
            customDictionary.Contains("Nikolay").Should().BeFalse();


            customDictionary.Add("Nikolay", 3);
            customDictionary.Add("Andrey", 4);
            customDictionary.Add("Sergey", 5);

            customDictionary["Ivan"].Should().Be(1);
            customDictionary["Stepan"].Should().Be(2);
            customDictionary["Nikolay"].Should().Be(3);
            customDictionary["Andrey"].Should().Be(4);
            customDictionary["Sergey"].Should().Be(5);
        }
    }
}
