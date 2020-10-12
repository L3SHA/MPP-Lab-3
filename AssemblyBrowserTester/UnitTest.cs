using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyBrowser;

namespace AssemblyBrowserTester
{
    [TestClass]
    public class UnitTest
    {
        string _testPath = @"C:\5 semester\MPP\Lab_1\TracerUtilsUnitTest\bin\Debug\netcoreapp3.1\TracerUtils.dll";

        [TestMethod]
        public void PositiveTestMethod()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_testPath);
            var assemblyData = assemblyBrowser.assemblyData;
            Assert.AreEqual(1, assemblyData.NameSpaces.Values.Count);
            NameSpaceData nameSpaceData = null; 
            assemblyData.NameSpaces.TryGetValue("TracerUtils", out nameSpaceData);
            Assert.AreEqual(5, nameSpaceData.TypesList.Count);
            Assert.IsTrue(assemblyData.NameSpaces.ContainsKey("TracerUtils"));
            Assert.AreEqual("TracerUtils.ITracer", nameSpaceData.TypesList[0].Name);
            Assert.AreEqual(5, nameSpaceData.TypesList[1].Properties.Count);
            Assert.AreEqual("threads", nameSpaceData.TypesList[3].Fields[0].Name);
        }

        [TestMethod]
        public void NegativeTestMethod()
        {
            AssemblyBrowser.AssemblyBrowser assemblyBrowser = new AssemblyBrowser.AssemblyBrowser(_testPath);
            var assemblyData = assemblyBrowser.assemblyData;
            Assert.AreNotEqual(0, assemblyData.NameSpaces.Values.Count);
            NameSpaceData nameSpaceData = null;
            assemblyData.NameSpaces.TryGetValue("TracerUtils", out nameSpaceData);
            Assert.AreNotEqual(0, nameSpaceData.TypesList.Count);
            Assert.IsFalse(assemblyData.NameSpaces.ContainsKey("Tracer"));
            Assert.AreNotEqual("System.String", nameSpaceData.TypesList[0].Name);
            Assert.AreNotEqual(0, nameSpaceData.TypesList[1].Properties.Count);
            Assert.AreNotEqual("thread", nameSpaceData.TypesList[3].Fields[0].Name);
        }
    }
}
