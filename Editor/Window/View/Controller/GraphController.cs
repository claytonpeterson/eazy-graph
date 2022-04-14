using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphController
{
    private readonly View view;
    private readonly NodeCreator nodeCreator;
    private readonly NodeConnector nodeConnector;

    public GraphController (View view, NodeCreator nodeCreator, NodeConnector nodeConnector)
    {
        this.view = view;
        this.nodeCreator = nodeCreator;
        this.nodeConnector = nodeConnector;
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
        ClearGraphElements(new List<GraphElement>(view.Nodes));
        view.Nodes.Clear();

        ClearGraphElements(new List<GraphElement>(view.Edges));
        view.Edges.Clear();
    }

    private void ClearGraphElements(List<GraphElement> elements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            view.RemoveElement(elements[i]);
        }
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
