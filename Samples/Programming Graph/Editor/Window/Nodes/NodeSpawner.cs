using System;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class NodeSpawner : INodeSpawner
    {
        public NodeView CreateNodeView(Type type, Vector2 position, TestingOutData data)
        {
            if (type == typeof(ForLoopNode))
            {
                return new ForLoopNode(position, data);
            }
            else if (type == typeof(VariableNode))
            {
                return new VariableNode(position, data);
            }
            else if (type == typeof(FunctionNode))
            {
                return new FunctionNode(position, data);
            }
            else if (type == typeof(LogicNode))
            {
                return new LogicNode(position, data);
            }
            return null;
        }
    }
}
