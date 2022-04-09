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

   /* public NodeView CreateNode(Vector2 position)
    {
        var node = new NodeView(data, position, null); // nodeBuilder.CreateNode(data, position);
        AddElement(node);
        return node;
    }*/
/*
    public NodeView CreateNode(TestNode test, Vector2 position)
    {
        var portInformation = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Multi
        };

        var nodeView = new NodeView(
            test, 
            position, 
            portInformation);

        AddElement(nodeView);
        return nodeView;
    }
*/
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
/*
    public void AddNodes(Graph graph)
    {
        if (graph == null)
            return;

        foreach (var nodeData in graph.Nodes)
        {
            var portInformation = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Multi
            };

            var nodeView = nodeSpawner.CreateNodeView(
                nodeData.Data,
                nodeData.Position,
                portInformation);

            nodeView.guid = nodeData.Guid;

            AddElement(nodeView);
        }
    }
*/
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
