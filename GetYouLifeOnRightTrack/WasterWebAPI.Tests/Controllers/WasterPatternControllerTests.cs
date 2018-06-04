//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using WasterWebAPI.Controllers;
//using WasterWebAPI.Handlers;
//using WasterWebAPI.Models;

//namespace WasterWebAPI.Tests.Controllers
//{
//    using WasterWebAPI.DAL;

//    /// <summary>
//    /// Summary description for WasterPatternControllerTests
//    /// </summary>
//    [TestClass]
//    public class WasterPatternControllerTests
//    {
//        public WasterPatternControllerTests()
//        {
//            //
//            // TODO: Add constructor logic here
//            //
//        }

//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        //
//        // You can use the following additional attributes as you write your tests:
//        //
//        // Use ClassInitialize to run code before running the first test in the class
//        // [ClassInitialize()]
//        // public static void MyClassInitialize(TestContext testContext) { }
//        //
//        // Use ClassCleanup to run code after all tests in a class have run
//        // [ClassCleanup()]
//        // public static void MyClassCleanup() { }
//        //
//        // Use TestInitialize to run code before running each test 
//        // [TestInitialize()]
//        // public void MyTestInitialize() { }
//        //
//        // Use TestCleanup to run code after each test has run
//        // [TestCleanup()]
//        // public void MyTestCleanup() { }
//        //
//        #endregion

//        [TestMethod]
//        public void Add_SomeRecord_CallsSaveRecordOnHandleOnce()
//        {
//            //Arange
//            var recordToSave = new WasterPatternRecordViewModel();
//            var handlerMock = new Mock<IPatternRepository>();
//            var controller = new WasterPatternApiController(handlerMock.Object);

//            //Act
//            controller.Add(recordToSave);

//            //Assert
//            handlerMock.Verify(e => e.Create(new Pattern()));

//            Assert.AreNotEqual(Guid.Empty, recordToSave.Id);
//        }

//        [TestMethod]
//        public void Add_SomeRecord_ReturnedGuidNotEmpty()
//        {
//            //Arange
//            var recordToSave = new WasterPatternRecordViewModel();
//            var handlerMock = new Mock<IPatternRepository>();
//            var controller = new WasterPatternApiController(handlerMock.Object);

//            //Act
//            controller.Add(recordToSave);

//            //Assert
//            Assert.AreNotEqual(Guid.Empty, recordToSave.Id);
//        }

//        [ExpectedException(typeof(ApplicationException))]
//        [TestMethod]
//        public void Add_HandlerThrowsException_ExceptionThrown()
//        {
//            //Arange
//            var recordToSave = new WasterPatternRecordViewModel();
//            var handlerMock = new Mock<IPatternRepository>();
//            var controller = new WasterPatternApiController(handlerMock.Object);
//            handlerMock.Setup(a => a.Create(It.IsAny<Pattern>())).Throws(new ApplicationException());

//            //Act
//            controller.Add(recordToSave);

//            //Assert
//        }


//        [TestMethod]
//        public void Disable_RecordExists_CallsDeleteRecordOnHandlerWithRecordId()
//        {
//            //Arange
//            var existingPattern = new WasterPatternRecordViewModel
//                {
//                    Id = Guid.NewGuid(),
//                    Pattern = "www.pattern.pl"
//                };

//            var handlerMock = new Mock<IPatternRepository>();
//            var controller = new WasterPatternApiController(handlerMock.Object);
//            handlerMock.Setup(a => a.Query(i => true).ToArray()).Returns(new[]
//            {
//                new Pattern{ Value = "any.pl"}, new Pattern{ Id= existingPattern.Id, Value = "www.pattern.pl"}
//            }
//        );

//            //Act
//            controller.Disable(existingPattern);

//            //Assert
//            handlerMock.Verify(handler => handler.Delete(existingPattern.Id));
//        }


//        [TestMethod]
//        public void Disable_NoRecordsExists_DoNothing()
//        {
//            //Arange
//            var existingPattern = new WasterPatternRecordViewModel
//            {
//                Id = Guid.NewGuid(),
//                Pattern = "www.pattern.pl"
//            };

//            var handlerMock = new Mock<IPatternRepository>();
//            var controller = new WasterPatternApiController(handlerMock.Object);
//            handlerMock.Setup(a => a.Query(i => true).ToArray()).Returns(new[]
//            {
//                new Pattern{ Value = "any.pl"}, new Pattern{ Id= existingPattern.Id, Value = "www.pattern.pl"}
//            }
//        );

//            //Act
//            controller.Disable(existingPattern);

//            //Assert
//            handlerMock.Verify(handler => handler.Delete(existingPattern.Id));
//        }
//    }
//}
