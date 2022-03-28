using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class LeafNodeView
{
    public GraphNode LeafNode()
    {
        return new GraphNode(
            title: "Composite Node",
            position: new Vector2(0, 0),
            visualElements: new VisualElement[] { new Button(), new Button() },
            inputPortCapacity: Port.Capacity.Multi,
            outputPortCapacity: Port.Capacity.Multi,
            typeof(CompositeNode));
    }
}
