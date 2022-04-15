using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.Linq;

public class NodeConnector
{
    private View graphView;

    public NodeConnector(View graphView)
    {
        this.graphView = graphView;
    }

    public void ConnectNodes(GraphData graph)
    {
        foreach (NodeData node in graph.Nodes)
        {
            ConnectNode(GetConnections(node, graph.Connections));
        }
    }

    private void ConnectNode(List<ConnectionData> connections)
    {
        foreach (var connection in connections)
        {
            LinkNodes(
                GetPort(connection.nodeAGUID, connection.nodeAPortName),
                GetPort(connection.nodeBGUID, connection.nodeBPortName));
        }
    }

    private bool IsConnected(NodeData node, ConnectionData connection)
    {
        return node.GUID == connection.nodeAGUID;
    }

    private List<ConnectionData> GetConnections(NodeData node, List<ConnectionData> allConnections)
    {
        return allConnections.Where(x => IsConnected(node, x)).ToList();
    }

    private void LinkNodes(Port output, Port input)
    {
        var tEdge = new Edge
        {
            input = input,
            output = output
        };

        tEdge?.input.Connect(tEdge);
        tEdge?.output.Connect(tEdge);
        graphView.Add(tEdge);
    }


    private Port GetPort(string nodeGUID, string portName)
    {
        return GetGraphNodeByGUID(nodeGUID).GetPort(portName);
    }

    private NodeView GetGraphNodeByGUID(string guid)
    {
        foreach (NodeView node in graphView.Nodes)
        {
            if (node.guid == guid)
                return node;
        }
        return null;
    }
}
