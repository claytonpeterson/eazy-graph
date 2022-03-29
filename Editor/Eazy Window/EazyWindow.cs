using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public abstract class EazyWindow : EditorWindow
{
    private EditorGraphView view;

    private void OnEnable()
    {
        SetupWindow();
    }

    private void OnDisable()
    {
        TeardownWindow();
    }

    private void SetupWindow()
    {
        AddView();
        AddToolbars(CreateToolbars(view));
    }

    private void TeardownWindow()
    {
        rootVisualElement.Clear();
    }

    private void AddView()
    {
        view = new EditorGraphView(GetNodeSpawner());
        view.StretchToParentSize();
        rootVisualElement.Add(view);
    }

    private void AddToolbars(Toolbar[] toolbars)
    {
        foreach (var toolbar in toolbars)
        {
            rootVisualElement.Add(toolbar);
        }
    }

    protected abstract INodeSpawner GetNodeSpawner();

    protected abstract Toolbar[] CreateToolbars(EditorGraphView view);
}
