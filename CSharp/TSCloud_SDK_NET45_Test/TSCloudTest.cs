using TDSPRINT.Cloud.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDSPRINT.Cloud.SDK.Datas;
using Newtonsoft.Json;

namespace TSCloud_SDK_NET40_Test
{
    
    
    /// <summary>
    ///This is a test class for TSCloudTest and is intended
    ///to contain all TSCloudTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TSCloudTest
    {
        private string ApiHost = "http://tp2staging.herokuapp.com";
        private string Email = "inseok.lee@3dsystems.com";
        private string Password = "dldlstjr";
        private TSCloud _TSCloud;
        private int testValue = 0;

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

        public TSCloudTest()
        {
            Console.WriteLine("Constructor");
            testValue++;
            Console.WriteLine(testValue);
        }


        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {   
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            Console.WriteLine("Class cleanup");
        }
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _TSCloud = new TSCloud(ApiHost);
            _TSCloud.Authenticate(Email, Password);
            Console.WriteLine(testValue);
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }

        #endregion


        ///// <summary>
        /////A test for TSCloud Constructor
        /////</summary>
        //[TestMethod()]
        //public void TSCloudConstructorTest()
        //{
        //    string AppHost = ApiHost;
        //    TSCloud target = new TSCloud(AppHost);
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        ///// <summary>
        /////A test for TSCloud Constructor
        /////</summary>
        //[TestMethod()]
        //public void TSCloudConstructorTest1()
        //{
        //    TSCloud target = new TSCloud();
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        /// <summary>
        ///A test for Authenticate
        ///</summary>
        [TestMethod()]
        public void AuthenticateTest()
        {
            //TSCloud target = new TSCloud(ApiHost); // TODO: Initialize to an appropriate value
            User actual;

            actual = _TSCloud.Authenticate(Email, Password);
            Console.WriteLine(actual.Email);
            Assert.AreEqual("inseok.lee@3dsystems.com", actual.Email);
            Assert.AreEqual(24, actual.Id);
        }

        ///// <summary>
        /////A test for AuthenticateByApiToken
        /////</summary>
        //[TestMethod()]
        //public void AuthenticateByApiTokenTest()
        //{
        //    TSCloud target = new TSCloud(); // TODO: Initialize to an appropriate value
        //    string email = string.Empty; // TODO: Initialize to an appropriate value
        //    string api_token = string.Empty; // TODO: Initialize to an appropriate value
        //    User expected = null; // TODO: Initialize to an appropriate value
        //    User actual;
        //    actual = target.AuthenticateByApiToken(email, api_token);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetApiHost
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("TSCloud_SDK.dll")]
        //public void GetApiHostTest()
        //{
        //    TSCloud_Accessor target = new TSCloud_Accessor(); // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = target.GetApiHost();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetDesktopSettings
        /////</summary>
        //[TestMethod()]
        //public void GetDesktopSettingsTest()
        //{
        //    TSCloud target = new TSCloud(); // TODO: Initialize to an appropriate value
        //    Hash expected = null; // TODO: Initialize to an appropriate value
        //    Hash actual;
        //    actual = target.GetDesktopSettings();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for IsOnline
        ///</summary>
        [TestMethod()]
        public void IsOnlineTest()
        {
            bool expected = true;
            bool actual;
            actual = _TSCloud.IsOnline();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsValidHost
        ///</summary>
        [TestMethod()]
        [DeploymentItem("TSCloud_SDK.dll")]
        public void IsValidHostTest()
        {
            TSCloud_Accessor target = new TSCloud_Accessor(); // TODO: Initialize to an appropriate value
            bool expected = true;
            bool actual;
            actual = target.IsValidHost(ApiHost);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for UpdateDesktopSettings
        ///</summary>
        [TestMethod()]
        public void UpdateDesktopSettingsTest()
        {
            Hash settings = new Hash();
            settings.Add("unit_test", true);
            Hash actual;
            actual = _TSCloud.UpdateDesktopSettings(settings);
            Assert.AreEqual(settings.Count, actual.Count );
            
        }

        /// <summary>
        ///A test for serializer_settings
        ///</summary>
        [TestMethod()]
        public void serializer_settingsTest()
        {
            //JsonSerializerSettings expected = null; // TODO: Initialize to an appropriate value
            //JsonSerializerSettings actual;
            //actual = TSCloud.serializer_settings();
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ApiToken
        ///</summary>
        [TestMethod()]
        public void ApiTokenTest()
        {
            Assert.IsFalse(String.IsNullOrEmpty(_TSCloud.ApiToken));
        }

        /// <summary>
        ///A test for AppHost
        ///</summary>
        [TestMethod()]
        public void AppHostTest()
        {
            Assert.IsFalse(String.IsNullOrEmpty(_TSCloud.AppHost));
        }

        /// <summary>
        ///A test for CurrentUser
        ///</summary>
        [TestMethod()]
        public void CurrentUserTest()
        {
            TSCloud target = _TSCloud;
            Assert.IsTrue(_TSCloud.CurrentUser.IsValid());
        }

        /// <summary>
        ///A test for Users
        ///</summary>
        //[TestMethod()]
        //public void UsersTest()
        //{
        //    TSCloud target = _TSCloud;
        //    Assert.IsTrue(_TSCloud.Users.All.Count > 0);
        //}
    }
}
