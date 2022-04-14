using UnityEditor.Experimental.GraphView;

public class NodeConnector
{
    private EditorGraphView graphView;

    public NodeConnector(EditorGraphView graphView)
    {
        this.graphView = graphView;
    }

    public void ConnectNodes(Graph graph)
    {
        for (int i = 0; i < graph.Nodes.Count; i++)
        {
            // Look at each connection
            var connections = graph.Connections;

            for (int y = 0; y < connections.Count; y++)
            {
                var connection = connections[y];

                if (graph.Nodes[i].Guid == connection.GuidA)
                {
                    var startNode = GetGraphNodeByGUID(connections[y].GuidA);
                    var endNode = GetGraphNodeByGUID(connections[y].GuidB);

                    var startPort = startNode.GetPort(connections[y].PortAName);
                    var endPort = endNode.GetPort(connections[y].PortBName);

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
