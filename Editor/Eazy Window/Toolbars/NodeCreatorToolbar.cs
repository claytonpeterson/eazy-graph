using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using System;

public class NodeCreatorToolbar : Toolbar
{
    private readonly EditorGraphView view;

    public NodeCreatorToolbar(EditorGraphView view)
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
        foreach(var type in types)
        {
            CreateButton(type);
        }

        for (int i = 0; i < types.Count; i++)
        {
            buttons[i] = CreateButton(types[i]);
        }
        return buttons;
    }

    private Button CreateButton(Type nodeType)
    {
        var testNode = Activator.CreateInstance(nodeType) as TestNode;

        return new Button(clickEvent: () =>
        {
            view.CreateNode(testNode, new Vector2(0, 0));
        })
        { text = "Add " + testNode };
    }
}
