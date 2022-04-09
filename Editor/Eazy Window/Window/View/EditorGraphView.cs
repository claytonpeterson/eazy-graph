using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class EditorGraphView : GraphView
{
    private readonly NodeCreator nodeCreator;

    private readonly GraphController graphController;

    // TODO this should pass a "controller" object that includes creation, connection, and erasing the grid
    public EditorGraphView(INodeSpawner nodeSpawner)
    {
        nodeCreator = new NodeCreator(this, nodeSpawner);

        graphController = new GraphController(
            view: this,
            nodeCreator: nodeCreator, 
            nodeConnector: new NodeConnector(this));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        AddManipulators();
    }

    public List<NodeView> Nodes => nodes.ToList().Cast<NodeView>().ToList();

    public List<Edge> Edges => edges.ToList();

    public void ShowGraph(Graph graph)
    {
        graphController.ShowGraph(graph);
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

    private void AddManipulators()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(
            new ContextMenu(
                graphView: this,
                nodeCreator: nodeCreator).CreateContextualMenu("test"));
    }
}
