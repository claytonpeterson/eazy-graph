using System;
using UnityEngine;

namespace skybirdgames.eazygraph.Editor
{
    public interface INodeSpawner
    {
        NodeView CreateNodeView(Type type, Vector2 position, TestingOutData data);
    }
}
