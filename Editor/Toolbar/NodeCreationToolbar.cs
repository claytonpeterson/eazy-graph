using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class NodeCreationToolbar : Toolbar
{
    private readonly EditorGraphView view;

    public NodeCreationToolbar(EditorGraphView view)
    {
        this.view = view;

        Add(new TextElement
        {
            text = "Node Creation: "
        });

        Add(NewRootNodeButton());
        Add(NewNodeButton());
        Add(NewTransitionNodeButton());
    }

    private Button NewNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode("Test Node", new Vector2(0, 0));
        })
        { text = "Add Node" };
    }

    private Button NewTransitionNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            var node = new GraphNode("Transition Node", new Vector2(0, 0), null);
            view.CreateNode(node);
        })
        { text = "Add Transition Node" };
    }

    private Button NewRootNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateRootNode();
        })
        { text = "Add Root Node" };
    }
}
