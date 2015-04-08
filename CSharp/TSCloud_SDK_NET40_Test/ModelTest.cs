using TDSPRINT.Cloud.SDK.Datas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Collections.Generic;
using TDSPRINT.Cloud.SDK.Types;
using TDSPRINT.Cloud.SDK;

namespace TSCloud_SDK_NET40_Test
{
    
    
    /// <summary>
    ///This is a test class for ModelTest and is intended
    ///to contain all ModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ModelTest
    {
        private TSCloud _TSCloud = null;
        private ModelClient _ModelClient = null;
        private Model _Model = null;

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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Initialize();
        }
        
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        public void Initialize()
        {
            _TSCloud = new TSCloud("http://tp2staging.herokuapp.com");
            User current_user = new User();

            current_user.ApiToken = "inska";
            current_user.Id = 22;
            current_user.Name = "Inseok Lee";
            current_user.Email = "inseok.lee@3dsystems.com";
            current_user.Role = "admin";
            current_user.Company = "test";
            _TSCloud.CurrentUser = current_user;
            _TSCloud.ApiToken = "inska";
            _ModelClient = new ModelClient(_TSCloud);

            _Model = _ModelClient.Get(14940);

        }

        /// <summary>
        ///A test for Model Constructor
        ///</summary>
        [TestMethod()]
        public void ModelConstructorTest()
        {
            Model target = new Model();
            
        }

        /// <summary>
        ///A test for CreateComment
        ///</summary>
        [TestMethod()]
        public void CommentTest()
        {
            List<Comment> comment_list = _Model.GetComments();
            int origin_count = comment_list.Count;
            Comment created = _Model.CreateComment("created");

            Assert.IsTrue(created.IsValid());
            Assert.IsTrue(created.Remove());
            Assert.AreEqual(origin_count, _Model.GetComments().Count);
        }

        /// <summary>
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            Assert.IsTrue(_Model.IsValid());

            _Model.Id = 0;
            Assert.IsFalse(_Model.IsValid());
        }

        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Model target = new Model(); // TODO: Initialize to an appropriate value
            Model expected = null; // TODO: Initialize to an appropriate value
            Model actual;
            actual = target.Update();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
