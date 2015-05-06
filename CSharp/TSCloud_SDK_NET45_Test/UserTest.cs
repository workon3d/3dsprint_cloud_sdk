using TDSPRINT.Cloud.SDK.Datas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Collections.Generic;
using TDSPRINT.Cloud.SDK;

namespace TSCloud_SDK_NET40_Test
{
    /// <summary>
    ///This is a test class for UserTest and is intended
    ///to contain all UserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UserTest : TestBase
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
            base.Initialize();
            _UserClient = new UserClient(_TSCloud);
            Assert.IsTrue(_TSCloud.CurrentUser.IsValid());            
        }
        #endregion


        /// <summary>
        ///A test for User Constructor
        ///</summary>
        [TestMethod()]
        public void UserConstructorTest()
        {
            User target = new User();
            Assert.IsFalse(target.IsValid());
        }

        /// <summary>
        ///A test for Follow
        ///</summary>
        [TestMethod()]
        public void CurrentUserFollowUnfollowTest()
        {
            User target = _TSCloud.CurrentUser;

            bool follow_result = target.Follow(_TargetModelId);
            bool unfollow_result = target.Unfollow(_TargetModelId);

            Assert.IsTrue(follow_result);
            Assert.IsTrue(unfollow_result);
        }


        /// <summary>
        ///A test for Unfollow
        ///</summary>
        [TestMethod()]
        public void FollowUnfollowTest()
        {
            User target = _UserClient.Get(65);

            bool follow_result = target.Follow(_TargetModelId);
            bool unfollow_result = target.Unfollow(_TargetModelId);

            Assert.IsTrue(follow_result);
            Assert.IsTrue(unfollow_result);
        }
    }
}
