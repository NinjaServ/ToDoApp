using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;

using ToDoApp.Infrastructure;

namespace ToDo_UnitTests
{
    /// <summary>
    /// Summary description for Infrastructure_UnitTest
    /// </summary>
    [TestClass]
    public class Infrastructure_UnitTest
    {
        public Infrastructure_UnitTest()
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
        public void ToDoList_Service_Counter_Test()
        {
            ToDoList_Service toDoList_Service = new ToDoList_Service();

            int counter = -2, counter2, counter3 = -2;

            counter = toDoList_Service.GetItemCounter();
            Assert.AreNotEqual(counter, -2);
            toDoList_Service.ItemCounterIncrement();
            counter2 = toDoList_Service.GetItemCounter();
            toDoList_Service.ItemCounterIncrement();
            counter3 = toDoList_Service.GetItemCounter();
            Assert.AreNotEqual(counter2, counter3);
            Assert.AreNotEqual(counter, counter3);
            Assert.AreNotEqual(counter, counter2);
            
            Trace.WriteLine(($"{counter}, {counter2}, {counter3}"), "Item Counter, 2++ Unit Test");

            int counter4 = -1, counter5 = -1;
            counter4= toDoList_Service.GetItemCounterAndIncrement();
            counter5 = counter4; 
            counter4 = toDoList_Service.GetItemCounterAndIncrement();
            counter4 = toDoList_Service.GetItemCounterAndIncrement();
            counter4 = toDoList_Service.GetItemCounterAndIncrement();
            Assert.AreEqual(counter4, counter5+3);


            Trace.WriteLine(($"4th counter value: {counter4} ; 5th: {counter5}"), "4 count & increment calls, add 3 test");


        }

        [TestMethod]
        public void Load_Settings_Test()
        {
            ToDoList_Service testService = new ToDoList_Service();


            Assert.IsTrue(Directory.Exists(@".\Data\"), "Data Directory Not Created");
            Assert.IsTrue(File.Exists(@".\Data\SettingsData.json"), "Settings File Not Created");
            Assert.IsTrue(File.Exists(@".\Data\ToDoListData.json"), "Data File Not Created");

            Assert.IsNotNull(testService, "ToDoList_Service object not instantiated correctly");
            Assert.IsNotNull(testService.ToDoList, "ToDoList_Service ToDoList object not instantiated correctly");

            System.Console.WriteLine("Directory: {0}", Directory.GetCurrentDirectory());
            string content = string.Join("\n", Directory.EnumerateFiles(Directory.GetCurrentDirectory()));
            Trace.WriteLine(string.Join($"\nDirectory: {0} \n", content), "Unit Test");
            Trace.Write(string.Join($"\nDirectory: {0}", string.Join("\n", Directory.EnumerateFiles(Directory.GetCurrentDirectory() + @"\Data"))), "Unit Test");
        }

    }
}



// list.ForEach(i => Console.Write("{0}\t", i));
//Console.WriteLine(string.Join("\t", list.Cast<string>().ToArray()));

//Assert.Fail("No exception was thrown.");

//        [ExpectedException(typeof(ArgumentOutOfRangeException))]
//Exception and SystemException
//ApplicationException
//InvalidOperationException
//ArgumentException, ArgumentNullException, and ArgumentOutOfRangeException
////NullReferenceException, AccessViolationException, or IndexOutOfRangeException
//StackOverflowException OutOfMemoryException
//ComException, SEHException, and ExecutionEngineException
