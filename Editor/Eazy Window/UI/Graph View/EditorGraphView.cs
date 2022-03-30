using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class EditorGraphView : GraphView
{
    private readonly INodeSpawner nodeSpawner;

    public EditorGraphView(INodeSpawner nodeSpawner)
    {
        this.nodeSpawner = nodeSpawner;

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
    private List<NodeView> Nodes => nodes.ToList().Cast<NodeView>().ToList();

    private List<Edge> Edges => edges.ToList();

   /* public NodeView CreateNode(Vector2 position)
    {
        var node = new NodeView(data, position, null); // nodeBuilder.CreateNode(data, position);
        AddElement(node);
        return node;
    }*/

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

    // THIS IS THE IMPORTANT ONE
    public void ShowGraph(Graph graph)
    {
        ClearGraph();

        if (graph == null)
        {
            Debug.Log("Graph is null");
            return;
        }

        AddNodes(graph);

        ConnectNodes(graph);
    }

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

    private NodeView GetGraphNodeByGUID(string guid)
    {
        for(int i = 0; i < Nodes.Count; i++)
        {
            if (Nodes[i].guid == guid)
                return Nodes[i];
        }
        return null;
    }

    public void ConnectNodes(Graph graph)
    {
        for(int i = 0; i < graph.Nodes.Count; i++)
        {
            // Look at each connection
            var connections = graph.Connections;
            for(int y = 0; y < connections.Count; y++)
            {
                var connection = connections[y];

                if (graph.Nodes[i].Guid == connection.GuidA)
                {
                    var endNode = GetGraphNodeByGUID(connections[y].GuidB);
                    LinkNodes(
                        Nodes[i].outputContainer[0].Q<Port>(), 
                        (Port)endNode.inputContainer[0]);
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
        Add(tEdge);
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
