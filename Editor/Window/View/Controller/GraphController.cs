
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

            // Update the ui of anything that is updated globally
            for (int i = 0; i < view.Nodes.Count; i++)
            {
                var updateable = (IUpdate)view.Nodes[i];
                if(updateable != null)
                {
                    updateable.Update();
                }
            }
        }
        // here
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
}
