using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class EditorGraphView : GraphView
{
    // Controls
    private readonly NodeCreator nodeCreator;
    private readonly NodeConnector nodeConnector;

    // TODO this should pass a "controller" object that includes creation, connection, and erasing the grid
    public EditorGraphView(INodeSpawner nodeSpawner)
    {
        this.nodeCreator = new NodeCreator(this, nodeSpawner);
        this.nodeConnector = new NodeConnector(this);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        //this.AddManipulator(CreateContextualMenu("Create Node"));
    }

    /*private ContextualMenuManipulator CreateContextualMenu(string contextualMenuText)
    {
        return new ContextualMenuManipulator(menuEvent =>
            menuEvent.menu.AppendAction(contextualMenuText, actionEvent =>
                AddElement(
                    CreateNode(
                        "Test", 
                        actionEvent.eventInfo.mousePosition)), 
                        DropdownMenuAction.AlwaysEnabled));
    }
*/
    public List<NodeView> Nodes => nodes.ToList().Cast<NodeView>().ToList();

    private List<Edge> Edges => edges.ToList();

    // THIS IS THE IMPORTANT ONE
    public void ShowGraph(Graph graph)
    {
        ClearGraph();

        if (graph == null)
        {
            Debug.Log("Graph is null");
            return;
        }

        nodeCreator.AddNodes(graph);

        nodeConnector.ConnectNodes(graph);
    }

    private void ClearGraph()
    {
        ClearGraphElements(new List<GraphElement>(Nodes));
        Nodes.Clear();

        ClearGraphElements(new List<GraphElement>(Edges));
        Edges.Clear();
    }

    private void ClearGraphElements(List<GraphElement> elements)
    {
        for(int i = 0; i < elements.Count; i++)
        {
            RemoveElement(elements[i]);
        }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach(port =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });

        return compatiblePorts;
    }
}
