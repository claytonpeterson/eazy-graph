using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NodeUpdateManipulator : IEdgeConnectorListener
{
    private readonly NodeView node;

    public NodeUpdateManipulator(NodeView node)
    {
        this.node = node;
    }

    public void OnDrop(GraphView graphView, Edge edge)
    {
        Debug.Log(string.Format("connecting: {0} with {1}", edge.input.portName, edge.output.portName));
    }

    public void OnDropOutsidePort(Edge edge, Vector2 position)
    {
        throw new System.NotImplementedException();
    }
}
