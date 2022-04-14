using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class View : GraphView
{
    private readonly NodeCreator nodeCreator;
    private readonly GraphController graphController;

    // TODO this should pass a "controller" object that includes creation, connection, and erasing the grid
    public View(INodeSpawner nodeSpawner)
    {
        nodeCreator = new NodeCreator(this, nodeSpawner);

        graphController = new GraphController(
            view: this,
            nodeCreator: nodeCreator, 
            nodeConnector: new NodeConnector(this));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        AddManipulators(nodeCreator);
    }

    public List<NodeView> Nodes => nodes.ToList().Cast<NodeView>().ToList();

    public List<Edge> Edges => edges.ToList();

    public void ShowGraph(GraphData graph)
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

    private void AddManipulators(NodeCreator nodeCreator)
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        this.AddManipulator(
            new ContextMenu(
                graphView: this,
                nodeCreator: nodeCreator).CreateContextualMenu("test"));
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        var types = TypeCache.GetTypesDerivedFrom<NodeView>();
        foreach(var type in types)
        {
            evt.menu.AppendAction(
                actionName: type.ToString(),
                action: (x) => CreateNode(type, Input.mousePosition));
        }
    }

    private void CreateNode(Type type, Vector2 position)
    {
        Debug.Log(type);

        //var testNode = Activator.CreateInstance(type) as TestNode;

        nodeCreator.CreateNode(type, position, new TestingOutData());
    }
}
