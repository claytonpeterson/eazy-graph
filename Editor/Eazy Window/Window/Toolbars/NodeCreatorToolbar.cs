using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using System;

public class NodeCreatorToolbar : Toolbar
{
    private readonly NodeCreator nodeCreator;

    public NodeCreatorToolbar(NodeCreator nodeCreator)
    {
        this.nodeCreator = nodeCreator;

        Add(new TextElement { text = "Node Creation: " });

        AddButtons();
    }

    private void AddButtons()
    {
        var types = TypeCache.GetTypesDerivedFrom<NodeView>();

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
        return new Button(clickEvent: () =>
        {
            nodeCreator.CreateNode(nodeType, new Vector2(0, 0), new TestingOutData());
        })
        { text = "Add " + nodeType };
    }
}
