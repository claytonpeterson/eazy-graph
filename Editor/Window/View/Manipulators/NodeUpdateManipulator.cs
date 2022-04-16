using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace skybirdgames.eazygraph.Editor
{
    public class NodeUpdateManipulator : IEdgeConnectorListener
    {
        private readonly NodeView node;

        public NodeUpdateManipulator(NodeView node)
        {
            this.node = node;
        }

        public void OnDrop(GraphView graphView, Edge edge)
        {
            node.Update();
        }

        public void OnDropOutsidePort(Edge edge, Vector2 position)
        {
        }
    }
}