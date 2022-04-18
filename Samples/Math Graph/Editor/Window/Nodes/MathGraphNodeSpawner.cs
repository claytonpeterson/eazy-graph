using System;
using UnityEngine;

using skybirdgames.eazygraph.Editor;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class MathGraphNodeSpawner : INodeSpawner
    {
        public NodeView CreateNodeView(Type type, Vector2 position, TestingOutData data)
        {
            if (type == typeof(NumberNode))
            {
                return new NumberNode(position, data);
            }
            else if (type == typeof(OperatorNode))
            {
                return new OperatorNode(position, data, new MathRunner());
            }
            else if (type == typeof(PortalNode))
            {
                return new PortalNode(position, data, new MathRunner());
            }
            return null;
        }
    }
}