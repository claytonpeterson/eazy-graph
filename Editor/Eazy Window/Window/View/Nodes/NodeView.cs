using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public abstract class NodeView : Node
{
    public string guid;

    public Vector2 Position;

    public TestingOutData data = new TestingOutData();

    public Type type;
    
    public VisualElement[] visualElements;

    public NodeView(Vector2 position, TestingOutData data)
    {
        type = GetType();
        Position = new Vector2(position.x, position.y);

        title = type.Name;

        this.data = data;

        guid = Guid.NewGuid().ToString();

        SetPosition(new Rect(position.x, position.y, 100, 100));

        SetupPorts();

        Refresh();
    }

    protected abstract PortInformation GetPortInformation();

    private void SetupPorts()
    {
        var portInfo = GetPortInformation();

        inputContainer.Add(
            child: AddPort(Direction.Input, portInfo.InputPortCapacity, "Input"));

        outputContainer.Add(
            child: AddPort(Direction.Output, portInfo.OutputPortCapacity, "Output"));
    }

    private Port AddPort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single, string portName = "")
    {
        var newPort = InstantiatePort(
            Orientation.Horizontal, 
            portDirection, 
            capacity, 
            typeof(float));

        newPort.portName = portName;
        return newPort;
    }

    private void Refresh()
    {
        RefreshExpandedState();
        RefreshPorts();
    }
}
