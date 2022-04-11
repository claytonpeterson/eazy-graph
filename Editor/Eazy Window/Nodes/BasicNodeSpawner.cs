using System;
using UnityEngine;

public class BasicNodeSpawner : INodeSpawner
{
    public NodeView CreateNodeView(Type type, Vector2 position, TestingOutData data)
    {
        if(type == typeof(SinglePortNode))
        {
            return new SinglePortNode(position, data);
        }
        else if (type == typeof(MultiPortNode))
        {
            return new MultiPortNode(position, data);
        }
        return null;
    }
}
