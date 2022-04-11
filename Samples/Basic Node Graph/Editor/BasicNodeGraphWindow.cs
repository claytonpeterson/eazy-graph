using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class BasicNodeGraphWindow : EazyWindow
{
    [MenuItem("Eazy Graph/Samples/Basic Node Graph")]
    public static void ShowWindow()
    {
        var window = GetWindow(typeof(BasicNodeGraphWindow));
        window.Show();
    }

    protected override Toolbar[] CreateToolbars()
    {
        // Create the serializer
        var scriptableObjectSerializer =
            new Serializer(
                saving: new ScriptableObjectGraphSaving(),
                loading: new ScriptableObjectLoading(),
                parentFolder: "Assets/Resources/",
                fileExtension: ".asset");

        return new Toolbar[]
        {
            new SerializationToolbar<GraphData>(view, scriptableObjectSerializer),
            new NodeCreatorToolbar(new NodeCreator(view, new BasicNodeSpawner()))
        };
    }

    protected override INodeSpawner GetNodeSpawner()
    {
        return new BasicNodeSpawner();
    }
}
