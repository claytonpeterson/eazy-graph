using System;
using UnityEngine;

public class Spawner : INodeSpawner
{
    public NodeView CreateNodeView(Type type, Vector2 position, PortInformation portInfo)
    {
        if(type == typeof(BigTestNode))
        {
            return new BigTestNode(position, portInfo);
        }
        else if (type == typeof(LittleTestNode))
        {
            return new LittleTestNode(position, portInfo);
        }
        
        return null;
    }
}
