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


        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}

        //#region Additional test attributes
        //// 
        ////You can use the following additional attributes as you write your tests:
        ////
        ////Use ClassInitialize to run code before running the first test in the class
        ////[ClassInitialize()]
        ////public static void MyClassInitialize(TestContext testContext)
        ////{
        ////}
        ////
        ////Use ClassCleanup to run code after all tests in a class have run
        ////[ClassCleanup()]
        ////public static void MyClassCleanup()
        ////{
        ////}
        ////
        ////Use TestInitialize to run code before running each test
        ////[TestInitialize()]
        ////public void MyTestInitialize()
        ////{
        ////}
        ////
        ////Use TestCleanup to run code after each test has run
        ////[TestCleanup()]
        ////public void MyTestCleanup()
        ////{
        ////}
        ////
        //#endregion


        ///// <summary>
        /////A test for ModelClient Constructor
        /////</summary>
        //[TestMethod()]
        //public void ModelClientConstructorTest()
        //{
        //    TSCloud TSCloud = null; // TODO: Initialize to an appropriate value
        //    Hash Configuration = null; // TODO: Initialize to an appropriate value
        //    ModelClient target = new ModelClient(TSCloud, Configuration);
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        ///// <summary>
        /////A test for ModelClient Constructor
        /////</summary>
        //[TestMethod()]
        //public void ModelClientConstructorTest1()
        //{
        //    TSCloud TSCloud = null; // TODO: Initialize to an appropriate value
        //    ModelClient target = new ModelClient(TSCloud);
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        ///// <summary>
        /////A test for ModelClient Constructor
        /////</summary>
        //[TestMethod()]
        //public void ModelClientConstructorTest2()
        //{
        //    ModelClient target = new ModelClient();
        //    Assert.Inconclusive("TODO: Implement code to verify target");
        //}

        ///// <summary>
        /////A test for All
        /////</summary>
        //[TestMethod()]
        //public void AllTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    List<Model> expected = null; // TODO: Initialize to an appropriate value
        //    List<Model> actual;
        //    actual = target.All();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for All
        /////</summary>
        //[TestMethod()]
        //public void AllTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Ftype type = new Ftype(); // TODO: Initialize to an appropriate value
        //    List<Model> expected = null; // TODO: Initialize to an appropriate value
        //    List<Model> actual;
        //    actual = target.All(type);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Copy
        /////</summary>
        //[TestMethod()]
        //public void CopyTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    int TargetModelId = 0; // TODO: Initialize to an appropriate value
        //    HttpStatusCode expected = new HttpStatusCode(); // TODO: Initialize to an appropriate value
        //    HttpStatusCode actual;
        //    actual = target.Copy(ModelId, TargetModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Copy
        /////</summary>
        //[TestMethod()]
        //public void CopyTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int[] ModelIds = null; // TODO: Initialize to an appropriate value
        //    int TargetModelId = 0; // TODO: Initialize to an appropriate value
        //    HttpStatusCode expected = new HttpStatusCode(); // TODO: Initialize to an appropriate value
        //    HttpStatusCode actual;
        //    actual = target.Copy(ModelIds, TargetModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    string ModelName = string.Empty; // TODO: Initialize to an appropriate value
        //    int ParentId = 0; // TODO: Initialize to an appropriate value
        //    string FilePath = string.Empty; // TODO: Initialize to an appropriate value
        //    Hash Meta = null; // TODO: Initialize to an appropriate value
        //    Hash Acl = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(ModelName, ParentId, FilePath, Meta, Acl);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    string ModelName = string.Empty; // TODO: Initialize to an appropriate value
        //    int ParentId = 0; // TODO: Initialize to an appropriate value
        //    string FilePath = string.Empty; // TODO: Initialize to an appropriate value
        //    Hash Meta = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(ModelName, ParentId, FilePath, Meta);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest2()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    string FilePath = string.Empty; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(FilePath);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest3()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    string ModelName = string.Empty; // TODO: Initialize to an appropriate value
        //    int ParentId = 0; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(ModelName, ParentId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest4()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    string ModelName = string.Empty; // TODO: Initialize to an appropriate value
        //    string FilePath = string.Empty; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(ModelName, FilePath);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Create
        /////</summary>
        //[TestMethod()]
        //public void CreateTest5()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Model model = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Create(model);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Delete
        /////</summary>
        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Delete(ModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Download
        /////</summary>
        //[TestMethod()]
        //public void DownloadTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    string strDownloadPath = string.Empty; // TODO: Initialize to an appropriate value
        //    ModelClient.onProgress _onProgress = null; // TODO: Initialize to an appropriate value
        //    HttpStatusCode expected = new HttpStatusCode(); // TODO: Initialize to an appropriate value
        //    HttpStatusCode actual;
        //    actual = target.Download(ModelId, strDownloadPath, _onProgress);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Download
        /////</summary>
        //[TestMethod()]
        //public void DownloadTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    byte[] expected = null; // TODO: Initialize to an appropriate value
        //    byte[] actual;
        //    actual = target.Download(ModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Get
        /////</summary>
        //[TestMethod()]
        //public void GetTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Get(ModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetDownloadURL
        /////</summary>
        //[TestMethod()]
        //public void GetDownloadURLTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = target.GetDownloadURL(ModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetModels
        /////</summary>
        //[TestMethod()]
        //public void GetModelsTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int Page = 0; // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    GetModelsOption[] Options = null; // TODO: Initialize to an appropriate value
        //    Models expected = null; // TODO: Initialize to an appropriate value
        //    Models actual;
        //    actual = target.GetModels(Page, ftype, Options);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetModels
        /////</summary>
        //[TestMethod()]
        //public void GetModelsTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    int FolderId = 0; // TODO: Initialize to an appropriate value
        //    GetModelsOption[] Options = null; // TODO: Initialize to an appropriate value
        //    Models expected = null; // TODO: Initialize to an appropriate value
        //    Models actual;
        //    actual = target.GetModels(ftype, FolderId, Options);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetModels
        /////</summary>
        //[TestMethod()]
        //public void GetModelsTest2()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    GetModelsOption[] Options = null; // TODO: Initialize to an appropriate value
        //    Models expected = null; // TODO: Initialize to an appropriate value
        //    Models actual;
        //    actual = target.GetModels(ftype, Options);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetModels
        /////</summary>
        //[TestMethod()]
        //public void GetModelsTest3()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int FolderId = 0; // TODO: Initialize to an appropriate value
        //    GetModelsOption[] Options = null; // TODO: Initialize to an appropriate value
        //    Models expected = null; // TODO: Initialize to an appropriate value
        //    Models actual;
        //    actual = target.GetModels(FolderId, Options);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetModels
        /////</summary>
        //[TestMethod()]
        //public void GetModelsTest4()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int Page = 0; // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    int FolderId = 0; // TODO: Initialize to an appropriate value
        //    GetModelsOption[] GetModelsOptions = null; // TODO: Initialize to an appropriate value
        //    Models expected = null; // TODO: Initialize to an appropriate value
        //    Models actual;
        //    actual = target.GetModels(Page, ftype, FolderId, GetModelsOptions);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetSysInfo
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("TSCloud_SDK.dll")]
        //public void GetSysInfoTest()
        //{
        //    ModelClient_Accessor target = new ModelClient_Accessor(); // TODO: Initialize to an appropriate value
        //    Hash expected = null; // TODO: Initialize to an appropriate value
        //    Hash actual;
        //    actual = target.GetSysInfo();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Move
        /////</summary>
        //[TestMethod()]
        //public void MoveTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    int TargetModelId = 0; // TODO: Initialize to an appropriate value
        //    HttpStatusCode expected = new HttpStatusCode(); // TODO: Initialize to an appropriate value
        //    HttpStatusCode actual;
        //    actual = target.Move(ModelId, TargetModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Move
        /////</summary>
        //[TestMethod()]
        //public void MoveTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int[] ModelIds = null; // TODO: Initialize to an appropriate value
        //    int TargetModelId = 0; // TODO: Initialize to an appropriate value
        //    HttpStatusCode expected = new HttpStatusCode(); // TODO: Initialize to an appropriate value
        //    HttpStatusCode actual;
        //    actual = target.Move(ModelIds, TargetModelId);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for RemoveMeta
        /////</summary>
        //[TestMethod()]
        //public void RemoveMetaTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Model model = null; // TODO: Initialize to an appropriate value
        //    List<string> KeyList = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.RemoveMeta(model, KeyList);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for RemoveMeta
        /////</summary>
        //[TestMethod()]
        //public void RemoveMetaTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    List<string> KeyList = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.RemoveMeta(ModelId, KeyList);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Update
        /////</summary>
        //[TestMethod()]
        //public void UpdateTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    string ModelName = string.Empty; // TODO: Initialize to an appropriate value
        //    string FilePath = string.Empty; // TODO: Initialize to an appropriate value
        //    Hash MetaJson = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Update(ModelId, ModelName, FilePath, MetaJson);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Update
        /////</summary>
        //[TestMethod()]
        //public void UpdateTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Model model = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.Update(model);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for UpdateMeta
        /////</summary>
        //[TestMethod()]
        //public void UpdateMetaTest()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    Hash Meta = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.UpdateMeta(ModelId, Meta);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for UpdateMeta
        /////</summary>
        //[TestMethod()]
        //public void UpdateMetaTest1()
        //{
        //    ModelClient target = new ModelClient(); // TODO: Initialize to an appropriate value
        //    Model model = null; // TODO: Initialize to an appropriate value
        //    Hash Meta = null; // TODO: Initialize to an appropriate value
        //    Model expected = null; // TODO: Initialize to an appropriate value
        //    Model actual;
        //    actual = target.UpdateMeta(model, Meta);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for index
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("TSCloud_SDK.dll")]
        //public void indexTest()
        //{
        //    ModelClient_Accessor target = new ModelClient_Accessor(); // TODO: Initialize to an appropriate value
        //    int ModelId = 0; // TODO: Initialize to an appropriate value
        //    int Page = 0; // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    List<Model> expected = null; // TODO: Initialize to an appropriate value
        //    List<Model> actual;
        //    actual = target.index(ModelId, Page, ftype);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for index
        /////</summary>
        //[TestMethod()]
        //[DeploymentItem("TSCloud_SDK.dll")]
        //public void indexTest1()
        //{
        //    ModelClient_Accessor target = new ModelClient_Accessor(); // TODO: Initialize to an appropriate value
        //    int Page = 0; // TODO: Initialize to an appropriate value
        //    Ftype ftype = new Ftype(); // TODO: Initialize to an appropriate value
        //    List<Model> expected = null; // TODO: Initialize to an appropriate value
        //    List<Model> actual;
        //    actual = target.index(Page, ftype);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
