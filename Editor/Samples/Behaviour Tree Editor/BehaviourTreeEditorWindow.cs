using UnityEditor;

public class BehaviourTreeEditorWindow : GraphWindow
{
    [MenuItem("Graph/Behaviour Tree Editor")]
    public static void OpenGraphWindow()
    {
        var window = GetWindow<BehaviourTreeEditorWindow>();
        window.titleContent.text = "Behaviour Tree Editor";
    }

    [MenuItem("Graph/Create Test Graph")]
    public static void CreateTestContainer()
    {
        var testContainer = CreateInstance<GraphData>();
        string path = string.Format("Assets/{0}.asset", "Graph");
        AssetDatabase.CreateAsset(testContainer, path);
        /*
                var spriteNode = Create<SpriteTestNode>("Sprite Node");
                var numberNode = Create<NumberNodeData>("Number Node");
        */
        /*testContainer.AddNode(spriteNode);
        testContainer.AddNode(numberNode);*/
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
        rootVisualElement.Add(new BehaviourTreeToolbar(view));
    }
}
