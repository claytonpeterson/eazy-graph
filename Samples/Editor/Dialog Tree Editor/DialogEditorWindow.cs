using UnityEditor;

public class DialogEditorWindow : GraphWindow
{
    [MenuItem("Graph/Open Dialog Editor")]
    public static void OpenGraphWindow()
    {
        var window = GetWindow<DialogEditorWindow>();
        window.titleContent.text = "Dialog Editor";
    }

    public static T Create<T>(string name) where T : NodeData
    {
        T node = CreateInstance<T>();
        string path = string.Format("Assets/{0}.asset", name);
        return node;
    }

    protected override void AddToolbars(EditorGraphView view)
    {
        rootVisualElement.Add(new XMLSerializationToolbar(view));
        rootVisualElement.Add(new DialogEditorToolbar(view));
    }

    protected override INodeSpawner GetNodeSpawner()
    {
        return new NodeSpawner();
    }
}
