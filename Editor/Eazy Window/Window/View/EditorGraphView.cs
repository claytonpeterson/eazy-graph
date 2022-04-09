using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class EditorGraphView : GraphView
{
    private readonly GraphController graphController;

    // TODO this should pass a "controller" object that includes creation, connection, and erasing the grid
    public EditorGraphView(INodeSpawner nodeSpawner)
    {
        graphController = new GraphController(
            view: this,
            nodeCreator: new NodeCreator(this, nodeSpawner), 
            nodeConnector: new NodeConnector(this));

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        AddManipulators();

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

    public List<Edge> Edges => edges.ToList();

    // THIS IS THE IMPORTANT ONE
    public void ShowGraph(Graph graph)
    {
        graphController.ClearGraph();

        if (graph == null)
        {
            Debug.Log("Graph is null");
            return;
        }

        graphController.AddNodes(graph);
        graphController.ConnectNodes(graph);
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
    }
}
