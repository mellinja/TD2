using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SQA_Tower_Defense;
using Microsoft.Xna.Framework;


namespace ClassTests
{
    [TestFixture()]
    class LanguageManagerTests
    {
        LanguageManager L;
        [SetUp()]
        public void SetUp()
        {
            L = new LanguageManager();
        }
        [Test()]
        public void Initializes()
        {
            Assert.IsNotNull(L);
        }

        [Test()]
        public void HasHello()
        {
            Assert.IsTrue(L.HasPhrase("Hello")); 
        }

        [Test()]
        public void IsHello()
        {
            Assert.AreEqual("Hola", L.getTranslation("Hello", "Sp"));
        }
        [Test()]
        public void HasName()
        {
            Assert.IsTrue(L.HasPhrase("Name"));
        }

        [Test()]
        public void IsName()
        {
            Assert.AreEqual("Name", L.getTranslation("Name", "En"));
        }



    }
}
