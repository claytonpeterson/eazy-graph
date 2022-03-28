using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class LeafNodeView
{
    public NodeView LeafNode()
    {
        return new NodeView(
            title: "Composite Node",
            position: new Vector2(0, 0),
            visualElements: new VisualElement[] { new Button(), new Button() },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            typeof(CompositeNode));
    }
}
