using System;
using UnityEngine;

public class Spawner : INodeSpawner
{
    public NodeView CreateNodeView(Type type, Vector2 position)
    {
        if(type == typeof(BigTestNode))
        {
            return new BigTestNode(position);
        }
        else if (type == typeof(LittleTestNode))
        {
            return new LittleTestNode(position);
        }
        
        return null;
    }
}
