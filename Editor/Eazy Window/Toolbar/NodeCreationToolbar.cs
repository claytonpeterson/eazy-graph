using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;

public class NodeCreationToolbar : Toolbar
{
    private readonly EditorGraphView view;

    public NodeCreationToolbar(EditorGraphView view)
    {
        this.view = view;

        Add(new TextElement { text = "Node Creation: " });
        AddButtons();
    }

    private void AddButtons()
    {
        var types = TypeCache.GetTypesDerivedFrom<TestNode>();
        foreach (var button in CreateButtonsForTypes(types))
        {
            Add(button);
        }
    }

    private Button[] CreateButtonsForTypes(TypeCache.TypeCollection types)
    {
        Button[] buttons = new Button[types.Count];
        for (int i = 0; i < types.Count; i++)
        {
            buttons[i] = CreateButton(types[i].Name);
        }
        return buttons;
    }

    private Button CreateButton(string text)
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode("Test Node", new Vector2(0, 0));
        })
        { text = "Add " + text };
    }
}
