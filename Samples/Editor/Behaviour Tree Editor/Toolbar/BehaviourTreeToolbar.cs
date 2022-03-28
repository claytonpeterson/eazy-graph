using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class BehaviourTreeToolbar : Toolbar
{
    private readonly NodeSpawner nodeSpawner;
    private readonly EditorGraphView view;

    public BehaviourTreeToolbar(EditorGraphView view)
    {
        this.view = view;

        nodeSpawner = new NodeSpawner();

        Add(new TextElement
        {
            text = "Behaviour Tree Tools: "
        });

        Add(NewActionNode());
        Add(NewCompositeNodeButton());
        Add(NewDecoratorNode());
    }

    private Button NewActionNode()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode(nodeSpawner.ActionNode(Vector2.zero));
        })
        { text = "Add Leaf Node" };
    }

    private Button NewCompositeNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode(nodeSpawner.CompositeNode(Vector2.zero));
        })
        { text = "Add Composite Node" };
    }

    private Button NewDecoratorNode()
    {
        return new Button(clickEvent: () =>
        {
            view.CreateNode(nodeSpawner.DecoratorNode(Vector2.zero));
        })
        { text = "Add Decorator Node" };
    }
}
