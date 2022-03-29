using System;
using UnityEngine;

public interface INodeSpawner
{
    NodeView CreateNodeView(Type type, Vector2 position, PortInformation portInfo);
}
