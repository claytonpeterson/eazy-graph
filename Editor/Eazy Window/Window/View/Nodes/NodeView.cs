using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public abstract class NodeView : Node
{
    public string guid;
    
    public VisualElement[] visualElements;

    public NodeView(Vector2 position)
    {
        guid = Guid.NewGuid().ToString();

        SetPosition(new Rect(position.x, position.y, 100, 100));

        SetupPorts();

        Refresh();
    }

    // TODO break this constructor into 2 objects! one of those objects should be port information
    public NodeView(string title, Vector2 position, VisualElement[] visualElements, PortInformation portInfo, string data = "")
    {
        this.visualElements = visualElements;
        base.title = title;

        guid = Guid.NewGuid().ToString();

        SetPosition(new Rect(position.x, position.y, 100, 100));

        SetupPorts();

        Refresh();
    }


    public void AddVisualElements(Port inputPort, Port outputPort)
    {
        // Add ports
        inputContainer.Add(inputPort);
        outputContainer.Add(outputPort);
/*
        // Add content
        for(int i = 0; i < visualElements.Length; i++)
        {
            mainContainer.Add(visualElements[i]);
        }*/
    }

    protected abstract PortInformation GetPortInformation();

    private void SetupPorts()
    {
        var portInfo = GetPortInformation();
        var inputPort = AddPort(Direction.Input, portInfo.InputPortCapacity, "Input");
        var outputPort = AddPort(Direction.Output, portInfo.OutputPortCapacity, "Output");
        AddVisualElements(inputPort, outputPort);
    }

    private Port AddPort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single, string portName = "")
    {
        var newPort = InstantiatePort(
            Orientation.Horizontal, 
            portDirection, 
            capacity, 
            typeof(float));

        newPort.name = portName;
        return newPort;
    }

    private void Refresh()
    {
        RefreshExpandedState();
        RefreshPorts();
    }
}
