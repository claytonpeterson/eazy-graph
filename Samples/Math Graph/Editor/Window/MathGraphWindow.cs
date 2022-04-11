using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class MathGraphWindow : EazyWindow
{
    [MenuItem("Eazy Graph/Samples/Math Graph")]
    public static void ShowWindow()
    {
        var window = GetWindow(typeof(MathGraphWindow));
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
            new NodeCreatorToolbar(new NodeCreator(view, GetNodeSpawner()))
        };
    }

    protected override INodeSpawner GetNodeSpawner()
    {
        return new MathGraphNodeSpawner();
    }
}
