using UnityEditor;
using UnityEngine.UIElements;

public abstract class GraphWindow : EditorWindow
{
    private EditorGraphView view;

    private void OnEnable()
    {
        view = AddView();

        AddToolbars(view);
    }

    private void OnDisable()
    {
        rootVisualElement.Clear();
    }

    private EditorGraphView AddView()
    {
        var view = new EditorGraphView(GetNodeSpawner());
        view.StretchToParentSize();
        rootVisualElement.Add(view);
        return view;
    }

    protected abstract INodeSpawner GetNodeSpawner();

    protected abstract void AddToolbars(EditorGraphView view);
}

