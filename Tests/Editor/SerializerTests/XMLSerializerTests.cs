using NUnit.Framework;
using UnityEngine;
using UnityEditor;

public class XMLSerializerTests
{
    private const string parentFolder = "Assets/Resources/";
    private const string fileExtension = ".xml";

    private EditorGraphView editorGraphView;
    private Serializer xmlSerializer;

    [SetUp]
    public void Setup()
    {
        Debug.Log("starting test");
        
        editorGraphView = new EditorGraphView(new Spawner());

        xmlSerializer = new Serializer(
            new XMLSaving(),
            new XMLLoading(),
            "Assets/Resources/",
            ".xml");
    }

    [TearDown]
    public void TearDown()
    {
        AssetDatabase.DeleteAsset(path: FilePath("test"));
    }

    [Test]
    public void SavingTest()
    {
        xmlSerializer.Save("test", editorGraphView);

        bool saveFileExists = 
            AssetDatabase.LoadAssetAtPath<TextAsset>(
                FilePath("test"));

        Assert.IsTrue(saveFileExists, "yes");
    }

    [Test]
    public void LoadingTest()
    {
        xmlSerializer.Save("test", editorGraphView);

        var graph = xmlSerializer.Load("test");

        Assert.IsNotNull(graph);
    }

    private string FilePath(string fileName)
    {
        return string.Format(parentFolder + "{0}{1}", fileName, fileExtension);
    }
}
