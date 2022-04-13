using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEditor;
using System;

public class NodeCreatorToolbar : Toolbar
{
    private readonly NodeCreator nodeCreator;

    public NodeCreatorToolbar(NodeCreator nodeCreator, string nameSpace)
    {
        this.nodeCreator = nodeCreator;

        Add(new TextElement { text = "Node Creation: " });

        AddButtons(nameSpace);
    }

    private void AddButtons(string nameSpace)
    {
        var types = TypeCache.GetTypesDerivedFrom<NodeView>();

        foreach (var button in CreateButtonsForTypes(types, nameSpace))
        {
            Add(button);
        }
    }

    private Button[] CreateButtonsForTypes(TypeCache.TypeCollection types, string domainNamespace)
    {
        Button[] buttons = new Button[types.Count];
        
        for (int i = 0; i < types.Count; i++)
        {
            // Only grab if it's in the same namespace
            if (types[i].ToString().Contains(domainNamespace))
            {
                buttons[i] = CreateButton(types[i]);
            }
        }
        return buttons;
    }

    private Button CreateButton(Type nodeType)
    {
        return new Button(clickEvent: () =>
        {
            nodeCreator.CreateNode(nodeType, new Vector2(0, 0), new TestingOutData());
        })
        { text = "Add " + nodeType.Name };
    }
}
