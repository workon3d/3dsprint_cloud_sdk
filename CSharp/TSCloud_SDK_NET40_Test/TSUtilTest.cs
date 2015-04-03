﻿using TDSPRINT.Cloud.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TDSPRINT.Cloud.SDK.Datas;

namespace TSCloud_SDK_NET40_Test
{
    
    
    /// <summary>
    ///This is a test class for TSUtilTest and is intended
    ///to contain all TSUtilTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TSUtilTest
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
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        ///// <summary>
        /////A test for TSUtil Constructor
        /////</summary>
        //[TestMethod()]
        //public void TSUtilConstructorTest()
        //{
        //    TSUtil target = new TSUtil();
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        /// <summary>
        ///A test for ConvertToIds
        ///</summary>
        //[TestMethod()]
        //public void ConvertToIdsTest()
        //{
        //    List<int> ids = null; // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = TSUtil.ConvertToIds(ids);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for ConvertToIds
        /////</summary>
        //[TestMethod()]
        //public void ConvertToIdsTest1()
        //{
        //    int[] ids = null; // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = TSUtil.ConvertToIds(ids);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for ConvertToIds
        /////</summary>
        //[TestMethod()]
        //public void ConvertToIdsTest2()
        //{
        //    List<CommonItem> items = null; // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = TSUtil.ConvertToIds(items);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
