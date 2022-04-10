using NUnit.Framework;
using UnityEditor;

namespace EazyGraph.Editor.Tests.Serialization
{
    public abstract class SerializerTestSuite
    {
        protected const string parentFolder = "Assets/Resources/";
        
        private const string fileName = "test";
        private EditorGraphView editorGraphView;
        private Serializer serializer;

        public abstract Serializer SerializerToTest();

        [SetUp]
        public void Setup()
        {
            serializer = SerializerToTest();

            editorGraphView = new EditorGraphView(new Spawner());
        }

        [Test]
        public void SaveAndLoad_ReturnsGraph()
        {
            serializer.Save(fileName, editorGraphView);

            var graph = serializer.Load(fileName);

            Assert.IsNotNull(graph);
        }

        [TearDown]
        public void TearDown()
        {
            AssetDatabase.DeleteAsset(path: FilePath());
        }

        private string FilePath()
        {
            return string.Format(
                parentFolder + "{0}{1}", 
                fileName, 
                serializer.FileExtension);
        }
    }
}
