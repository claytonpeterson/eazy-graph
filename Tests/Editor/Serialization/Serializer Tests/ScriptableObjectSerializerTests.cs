
namespace EazyGraph.Editor.Tests.Serialization
{
    public class ScriptableObjectSerializerTests : SerializerTestSuite
    {
        private const string fileExtension = ".asset";

        public override Serializer SerializerToTest()
        {
            return new Serializer(
                new ScriptableObjectGraphSaving(),
                new ScriptableObjectLoading(),
                parentFolder,
                fileExtension);
        }
    }
}
