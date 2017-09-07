using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ToDoApp.Models;
//using ToDoApp.Infrastructure;


namespace ToDo_UnitTests
{
    /// <summary>
    /// Summary description for DataValidation_UnitTest
    /// </summary>
    [TestClass]
    public class DataValidation_UnitTest
    {
        public DataValidation_UnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DataValidation_TextIsParagraphic_Test()
        {
            string test_string = "This has sentences. It is in a paragraph format. There are $20 bills and 1.25% chances.   There are also whitespaces. ";
            bool result = false;

            result = DataValidator.StringIsText(test_string);
            Assert.IsTrue(result, "String did not contain English text content.");

            result = DataValidator.TextIsSentences(test_string);
            Assert.IsTrue(result, "String did not match English sentence content.");

            result = DataValidator.TextIsParagraphic(test_string);
            Assert.IsTrue(result, "String did not match English paragraph content.");
        }


        [TestMethod]
        public void DataValidation_TextIsNotParagraphic_Test()
        {
            string test_string = @"This has mixed content. !@#$%^&*()-=<>?:|,./;'[]\  ͢  alpha	α	beta	β	gamma	γ	delta	δ";
            bool result = false;

            result = DataValidator.StringIsText(test_string);
            Assert.IsTrue(result, "String contains English text content.");

            result = DataValidator.TextIsSentences(test_string);
            Assert.IsFalse(result, "String contains English sentence content.");

            result = DataValidator.TextIsParagraphic(test_string);
            Assert.IsFalse(result, "String contains English paragraph content.");
        }

        [TestMethod]
        public void DataValidation_TextIsEmpty_Test()
        {
            string test_string = @"";
            bool result = false;

            result = DataValidator.StringIsText(test_string);
            Assert.IsFalse(result, "String is not empty.");

            result = DataValidator.TextIsSentences(test_string);
            Assert.IsFalse(result, "String matched English sentence content.");

            result = DataValidator.TextIsParagraphic(test_string);
            Assert.IsFalse(result, "String matched English paragraph content.");
        }
    }
}
