using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;

namespace AssemblyBrowserTester
{
    [TestClass]
    public class UnitTest
    {
        string _firstTestPath = @"C:\5 semester\MPP\Lab_1\TracerUtilsUnitTest\bin\Debug\netcoreapp3.1\TracerUtils.dll";

        string _secondTestPath = @"C:\5 semester\MPP\Lab_3\ExtensionMethodTest\bin\Debug\netcoreapp3.1\ExtensionMethodTest.dll";

        [TestMethod]
        public void ExtensionMethodTest()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_secondTestPath);
            var assemblyData = assemblyBrowser.assemblyData;
            NameSpaceData nameSpaceData = null;
            assemblyData.NameSpaces.TryGetValue("ExtensionMethodTest", out nameSpaceData);
            Assert.AreEqual("GetExtension", nameSpaceData.TypesList[1].Methods[nameSpaceData.TypesList[1].Methods.Count - 1].Name);
        }

        [TestMethod]
        public void SeveralNameSpacesTestMethod()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_secondTestPath);
            var assemblyData = assemblyBrowser.assemblyData;
            Assert.AreEqual(2, assemblyData.NameSpaces.Count);
        }

        [TestMethod]
        public void PositiveTestMethod()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_firstTestPath);
            var assemblyData = assemblyBrowser.assemblyData;
            Assert.AreEqual(1, assemblyData.NameSpaces.Values.Count);
            NameSpaceData nameSpaceData = null; 
            assemblyData.NameSpaces.TryGetValue("TracerUtils", out nameSpaceData);
            Assert.AreEqual(5, nameSpaceData.TypesList.Count);
            Assert.IsTrue(assemblyData.NameSpaces.ContainsKey("TracerUtils"));
            Assert.AreEqual("ITracer", nameSpaceData.TypesList[0].Name);
            Assert.AreEqual(5, nameSpaceData.TypesList[1].Properties.Count);
            Assert.AreEqual("threads", nameSpaceData.TypesList[3].Fields[0].Name);
        }

        [TestMethod]
        public void NegativeTestMethod()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_firstTestPath);
            var assemblyData = assemblyBrowser.assemblyData;
            Assert.AreNotEqual(0, assemblyData.NameSpaces.Values.Count);
            NameSpaceData nameSpaceData = null;
            assemblyData.NameSpaces.TryGetValue("TracerUtils", out nameSpaceData);
            Assert.AreNotEqual(0, nameSpaceData.TypesList.Count);
            Assert.IsFalse(assemblyData.NameSpaces.ContainsKey("Tracer"));
            Assert.AreNotEqual("String", nameSpaceData.TypesList[0].Name);
            Assert.AreNotEqual(0, nameSpaceData.TypesList[1].Properties.Count);
            Assert.AreNotEqual("thread", nameSpaceData.TypesList[3].Fields[0].Name);
        }
    }
}
