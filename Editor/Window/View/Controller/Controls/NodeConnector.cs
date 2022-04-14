using UnityEditor.Experimental.GraphView;

public class NodeConnector
{
    private View graphView;

    public NodeConnector(View graphView)
    {
        this.graphView = graphView;
    }

    public void ConnectNodes(GraphData graph)
    {
        for (int i = 0; i < graph.Nodes.Count; i++)
        {
            // Look at each connection
            var connections = graph.Connections;

            for (int y = 0; y < connections.Count; y++)
            {
                var connection = connections[y];

                if (graph.Nodes[i].GUID == connection.nodeAGUID)
                {
                    var startNode = GetGraphNodeByGUID(connections[y].nodeAGUID);
                    var endNode = GetGraphNodeByGUID(connections[y].nodeBGUID);

                    var startPort = startNode.GetPort(connections[y].nodeAPortName);
                    var endPort = endNode.GetPort(connections[y].nodeBPortName);

                    LinkNodes(startPort, endPort);
                }
            }
        }
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

    private NodeView GetGraphNodeByGUID(string guid)
    {
        for (int i = 0; i < graphView.Nodes.Count; i++)
        {
            if (graphView.Nodes[i].guid == guid)
                return graphView.Nodes[i];
        }
        return null;
    }
}
