using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoApp.Infrastructure;

namespace ToDoUnitTests
{
    [TestClass]
    public class ToDoService_UnitTest
    {
        [TestMethod]
        public void Load_Settings_Test()
        {
            ToDoList_Service testService = new ToDoList_Service();

            Assert.IsNotNull(testService, "ToDoList_Service object not instantiated correctly");

        }
    }
}
