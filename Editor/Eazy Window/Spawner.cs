using System;
using UnityEditor.UIElements;
using UnityEngine;

public class Spawner : INodeSpawner
{
    public NodeView CreateNodeView(Type type, Vector2 position, PortInformation portInfo)
    {
        var testNode = Activator.CreateInstance(type) as TestNode;

        var nodeView = new NodeView(testNode, position, portInfo);

        if(type == typeof(BigTestNode))
        {
            nodeView.Add(new FloatField());
        }
        else if (type == typeof(LittleTestNode))
        {
            nodeView.Add(new IntegerField());
        }

        return nodeView;
    }
}
