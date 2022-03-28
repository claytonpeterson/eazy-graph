
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class NodeSpawner : INodeSpawner
{
    Object obj;

    private ObjectField ObjectFieldContainer(ObjectField objectField)
    {
        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }
    
    public NodeView CreateNode(string name, Vector2 position)
    {
        NodeView node;
        if (name == "Composite Node")
        {
            node = CompositeNode(position);
        }
        else if (name == "Action Node")
        {
            node = ActionNode(position);
        }
        else
        {
            node = DecoratorNode(position);
        }
        return node;
    }

    public NodeView CompositeNode(Vector2 position)
    {
        return new NodeView(
            title: "Composite Node",
            position: position,
            visualElements: new VisualElement[] { },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            typeof(CompositeNode));
    }

    public NodeView ActionNode(Vector2 position)
    {
        ObjectField objectField = new ObjectField(label: "Behaviour")
        {
            objectType = typeof(IBehaviour)
        };

        return new NodeView(
            title: "Action Node",
            position: position,
            visualElements: new VisualElement[] { ObjectFieldContainer(objectField) },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Single,
            typeof(LeafNodeView));
    }

    public NodeView DecoratorNode(Vector2 position)
    {
        ObjectField objectField = new ObjectField(label: "Go to graph: ")
        {
            objectType = typeof(TextAsset)
        };

        return new NodeView(
            title: "Decorator Node",
            position: position,
            visualElements: new VisualElement[] { ObjectFieldContainer(objectField), new Button() },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            typeof(CompositeNode));
    }
}
