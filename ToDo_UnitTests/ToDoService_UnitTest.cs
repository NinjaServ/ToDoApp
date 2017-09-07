using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApp.Infrastructure;
using System.IO;
using System.Diagnostics;

namespace ToDoUnitTests
{
    [TestClass]
    public class ToDoService_UnitTest
    {
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
            string content = string.Join("\n", Directory.EnumerateFiles(Directory.GetCurrentDirectory()) ) ;
            Trace.WriteLine(string.Join($"\nDirectory: {0} \n", content), "Unit Test");
            Trace.Write(string.Join($"\nDirectory: {0}", string.Join("\n", Directory.EnumerateFiles(Directory.GetCurrentDirectory()+@"\Data"))), "Unit Test");
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