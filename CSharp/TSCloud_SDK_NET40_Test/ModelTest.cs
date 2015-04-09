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
    public class ModelTest : TestBase
    {
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

        #region Overrided initializer
        public override void Initialize()
        {
            Initialize();
            _ModelClient = new ModelClient(_TSCloud);
            _Model = _ModelClient.Get(14940);
            Assert.IsTrue(_Model.IsValid());
            Assert.IsTrue(_TSCloud.CurrentUser.IsValid());
        }
        #endregion

        /// <summary>
        ///A test for Model Constructor
        ///</summary>
        [TestMethod()]
        public void ModelConstructorTest()
        {
            Model target = new Model();
            Assert.IsFalse(target.IsValid());
            
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
            Model origin = _Model;

            _Model.Description = "changed";
            _Model.Name = "wheel2.stl";

            Model updated = _Model.Update();

            Assert.IsTrue(updated.IsValid());
            Assert.AreEqual(_Model.Description, updated.Description);
            Assert.AreEqual(_Model.Name, updated.Name);

            origin.Update();
        }
    }
}
