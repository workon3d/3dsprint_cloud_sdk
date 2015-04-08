using TDSPRINT.Cloud.SDK;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TDSPRINT.Cloud.SDK.Datas;
using System.Collections.Generic;
using TDSPRINT.Cloud.SDK.Types;
using System.Net;

namespace TSCloud_SDK_NET40_Test
{
    
    
    /// <summary>
    ///This is a test class for ModelClientTest and is intended
    ///to contain all ModelClientTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ModelClientTest
    {
        private TSCloud _TSCloud = null;
        private ModelClient _ModelClient = null;

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
        //You can use the following additional attributes as you write your tests:
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }
        
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            Initialize();
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        
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
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void FolderCreateAndDeleteTest()
        {
            Model model = new Model();
            model.Name = "new folder";
            model.Description = "created folder";
            model.Ftype = Ftype.Folder;

            Model created = _ModelClient.Create(model);

            Assert.IsNotNull(created);
            Assert.IsNotNull(created.Id);
            Assert.IsTrue(created.Id > 0);
            Assert.AreEqual(model.Name, created.Name);
            Assert.AreEqual(model.Description, created.Description);
            Assert.AreEqual(model.Ftype, created.Ftype);

            Model delete_result = _ModelClient.Delete(created.Id);

            Assert.IsNotNull(delete_result);
            Assert.AreEqual(HttpStatusCode.OK, delete_result.StatusCode);
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            Model target = _ModelClient.Get(14940);
            Assert.IsTrue(target.IsValid());
            Assert.AreEqual("wheel.stl", target.Name);
            Assert.AreEqual(2153357, target.Size);
            Assert.AreEqual(Ftype.File, target.Ftype);
            Assert.AreEqual(false, target.Readonly);
        }

        /// <summary>
        ///A test for GetDownloadURL
        ///</summary>
        [TestMethod()]
        public void GetDownloadURLTest()
        {
            string target = _ModelClient.GetDownloadURL(14940);

            Assert.IsNotNull(target);
            Assert.IsTrue(target.IndexOf("http://") == 0);
        }

        /// <summary>
        ///A test for GetModels
        ///</summary>
        [TestMethod()]
        public void GetModelsTest()
        {
            Models models = _ModelClient.GetModels(0, Ftype.All);

            Assert.IsTrue(models.Contents.Count > 0);
            Assert.IsNotNull(models.Pagination);
        }

        /// <summary>
        ///A test for RemoveMeta
        ///</summary>
        [TestMethod()]
        public void RemoveMetaTest()
        {
            Model model = _ModelClient.Get(14940);

            if (!model.Meta.ContainsKey("unittest"))
            {
                Hash meta = new Hash();
                meta.Add("unittest", "string");

                model.Meta = meta;
                _ModelClient.Update(model);
            }

            List<String> key_list = new List<String>();
            key_list.Add("unittest");

            bool remove_result = _ModelClient.RemoveMeta(14940, key_list);

            Assert.IsTrue(remove_result);
        }


        /// <summary>
        ///A test for Update
        ///</summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Model target = _ModelClient.Get(14940);
            target.Description = "description";
            target.Name = "wheel2.stl";
            Model updated = _ModelClient.Update(target);

            Assert.AreEqual(target.Name, updated.Name);
            Assert.AreEqual(target.Description, updated.Description);

            target.Name = "wheel.stl";
            target.Description = null;

            // Recover
            _ModelClient.Update(target);
        }

        /// <summary>
        ///A test for UpdateMeta
        ///</summary>
        [TestMethod()]
        public void UpdateMetaTest()
        {
            string content = "Test by Unit test";
            Model target = _ModelClient.Get(14940);
            Hash changed = target.Meta;
            if (changed.ContainsKey("unittest"))
                changed["unittest"] = content;
            else
                changed.Add("unittest", content);

            bool is_updated = _ModelClient.UpdateMeta(14940, changed);

            Assert.IsTrue(is_updated);

            Model updated = _ModelClient.Get(14940);
            Assert.AreEqual(changed["unittest"], updated.Meta["unittest"]);
        }
    }
}
