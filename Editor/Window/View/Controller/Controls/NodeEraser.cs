using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

public class NodeEraser
{
    private readonly View view;

    public NodeEraser(View view)
    {
        this.view = view;
    }

    public void ClearGraph()
    {
        ClearGraphElements(new List<GraphElement>(view.Nodes));
        ClearGraphElements(new List<GraphElement>(view.Edges));
    }

    private void ClearGraphElements(List<GraphElement> elements)
    {
        for (int i = 0; i < elements.Count; i++)
        {
            view.RemoveElement(elements[i]);
        }
        elements.Clear();
    }
}
