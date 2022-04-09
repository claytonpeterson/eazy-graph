using UnityEngine;

public class GraphController
{
    private readonly NodeCreator nodeCreator;
    private readonly NodeConnector nodeConnector;

    public GraphController (NodeCreator nodeCreator, NodeConnector nodeConnector)
    {
        this.nodeCreator = nodeCreator;
        this.nodeConnector = nodeConnector;
    }
}
