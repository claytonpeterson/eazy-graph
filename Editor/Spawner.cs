using System;
using UnityEngine;

public class Spawner : INodeSpawner
{
    public NodeView CreateNodeView(Type type, Vector2 position, Data data)
    {
        if(type == typeof(BigTestNode))
        {
            return new BigTestNode(position, data);
        }
        else if (type == typeof(LittleTestNode))
        {
            return new LittleTestNode(position, data);
        }

        return null;
    }
}
