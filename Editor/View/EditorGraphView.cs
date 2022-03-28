using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor;

public class EditorGraphView : UnityEditor.Experimental.GraphView.GraphView
{
    private readonly INodeSpawner nodeSpawner;

    public EditorGraphView(INodeSpawner nodeSpawner)
    {
        this.nodeSpawner = nodeSpawner;

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(CreateContextualMenu("Create Node"));
    }

    private ContextualMenuManipulator CreateContextualMenu(string contextualMenuText)
    {
        return new ContextualMenuManipulator(menuEvent =>
            menuEvent.menu.AppendAction(contextualMenuText, actionEvent =>
                AddElement(
                    CreateNode(
                        "Test", 
                        actionEvent.eventInfo.mousePosition)), 
                        DropdownMenuAction.AlwaysEnabled));
    }

    private List<GraphNode> Nodes => nodes.ToList().Cast<GraphNode>().ToList();

    private List<Edge> Edges => edges.ToList();

    public GraphNode CreateNode(string data, Vector2 position)
    {
        var node = new GraphNode(data, position, null); // nodeBuilder.CreateNode(data, position);
        AddElement(node);
        return node;
    }

    public GraphNode CreateNode(GraphNode node)
    {
        AddElement(node);
        return node;
    }

    public GraphNode CreateRootNode()
    {
        var node = new GraphNode("root", Vector2.zero, null);
        AddElement(node);
        return node;
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

    private Button NewNodeButton()
    {
        return new Button(clickEvent: () =>
        {
            Debug.Log("hi");
        })
        { text = "Add Node" };
    }

    Object obj;

    // INSANE, should not be here!
    private ObjectField BehaviourObjectField()
    {
        ObjectField objectField = new ObjectField(label: "Behaviour")
        {
            //objectType = typeof(IBehaviour)
        };

        objectField.SetValueWithoutNotify(obj);
        objectField.MarkDirtyRepaint();
        objectField.RegisterValueChangedCallback(evt => {
            obj = evt.newValue;
        });

        return objectField;
    }

    public void AddNodes(Graph graph)
    {
        if (graph == null)
            return;

        Debug.Log("graph to load: " + graph);

        foreach (var nodeData in graph.Nodes)
        {
            /*GraphNode node;
            if(nodeData.Name == "Composite Node")
            {
                node = nodeSpawner.CompositeNode(nodeData.Position);
            }
            else if (nodeData.Name == "Action Node")
            {
                node = nodeSpawner.ActionNode(nodeData.Position);
            }
            else
            {
                node = nodeSpawner.DecoratorNode(nodeData.Position);
            }*/
            var node = nodeSpawner.CreateNode(nodeData.Name, nodeData.Position);
            node.guid = nodeData.Guid;
            AddElement(node);
        }
    }

    private GraphNode GetGraphNodeByGUID(string guid)
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
                    Debug.Log(endNode);

                    LinkNodes(Nodes[i].outputContainer[0].Q<Port>(), (Port)endNode.inputContainer[0]);
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
