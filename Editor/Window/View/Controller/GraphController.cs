
public class GraphController
{
    private readonly View view;
    private readonly NodeCreator nodeCreator;
    private readonly NodeConnector nodeConnector;
    private readonly NodeEraser nodeEraser;

    public GraphController (View view, NodeCreator nodeCreator)
    {
        this.view = view;
        this.nodeCreator = nodeCreator;

        nodeConnector = new NodeConnector(view);
        nodeEraser = new NodeEraser(view);
    }

    public void ShowGraph(GraphData graph)
    {
        ClearGraph();

        if (graph != null)
        {
            AddNodes(graph);
            ConnectNodes(graph);
            UpdateNodes();
        }
    }

    public void ClearGraph()
    {
        nodeEraser.ClearGraph();
    }

    private void AddNodes(GraphData graph)
    {
        nodeCreator.AddNodes(graph);
    }

    private void ConnectNodes(GraphData graph)
    {
        nodeConnector.ConnectNodes(graph);
    }

    private void UpdateNodes()
    {
        foreach(var node in view.Nodes)
        {
            var updateable = (IUpdate)node;
            if (updateable != null)
            {
                updateable.Update();
            }
        }
    }
}
