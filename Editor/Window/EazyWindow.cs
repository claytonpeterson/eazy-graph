using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EazyWindow : EditorWindow
{
    protected EditorGraphView view;

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
        rootVisualElement.Add(CreateView());

        foreach (var toolbar in CreateToolbars())
        {
            rootVisualElement.Add(toolbar);
        }
    }

    private void TeardownWindow()
    {
        rootVisualElement.Clear();
    }

    private EditorGraphView CreateView()
    {
        view = new EditorGraphView(GetNodeSpawner());
        view.StretchToParentSize();
        return view;
    }

    protected abstract INodeSpawner GetNodeSpawner();

    protected abstract Toolbar[] CreateToolbars();
}
