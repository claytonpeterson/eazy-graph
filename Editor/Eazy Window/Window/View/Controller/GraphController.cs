using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphController
{
    private readonly EditorGraphView view;
    private readonly NodeCreator nodeCreator;
    private readonly NodeConnector nodeConnector;

    public GraphController (EditorGraphView view, NodeCreator nodeCreator, NodeConnector nodeConnector)
    {
        this.view = view;
        this.nodeCreator = nodeCreator;
        this.nodeConnector = nodeConnector;
    }

    public void ShowGraph(Graph graph)
    {
        ClearGraph();

        if (graph != null)
        {
            AddNodes(graph);
            ConnectNodes(graph);

            // Update the ui of anything that is updated globally
            for (int i = 0; i < view.Nodes.Count; i++)
            {
                var node = view.Nodes[i];

                var updateable = (IUpdate)node;
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

    private void AddNodes(Graph graph)
    {
        nodeCreator.AddNodes(graph);
    }

    private void ConnectNodes(Graph graph)
    {
        nodeConnector.ConnectNodes(graph);
    }
}
