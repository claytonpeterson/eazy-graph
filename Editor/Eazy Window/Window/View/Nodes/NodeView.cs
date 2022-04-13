using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public abstract class NodeView : Node
{
    public string guid;

    public Vector2 Position;

    public NodeView(Vector2 position, TestingOutData data)
    {
        Position = new Vector2(position.x, position.y);

        userData = data;
        
        guid = Guid.NewGuid().ToString();

        SetPosition(new Rect(position.x, position.y, 100, 100));

        Refresh();
    }

    public TestingOutData Data()
    {
        return (TestingOutData)userData;
    }

    public Port CreatePort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single, string portName = "")
    {
        var newPort = InstantiatePort(
            Orientation.Horizontal, 
            portDirection, 
            capacity, 
            typeof(float));

        newPort.portName = portName;
        return newPort;
    }

    public virtual void Refresh()
    {
        RefreshExpandedState();
        RefreshPorts();
    }
}
