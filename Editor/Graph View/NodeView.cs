using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class NodeView : Node
{
    public string guid;
    
    public string text;
    
    public bool isRoot = false;

    public VisualElement[] visualElements;

    public string data;

    // TODO break this constructor into 2 objects! one of those objects should be port information
    public NodeView(string title, Vector2 position, VisualElement[] visualElements, Port.Capacity inputPortCapacity = Port.Capacity.Single, Port.Capacity outputPortCapacity = Port.Capacity.Multi, string data = "")
    {
        text = title;

        this.visualElements = visualElements;

        base.title = title;

        this.data = data;

        guid = Guid.NewGuid().ToString();

        SetPosition(new Rect(position.x, position.y, 100, 100));

        var inputPort = AddPort(Direction.Input, inputPortCapacity, "Input");
        var outputPort = AddPort(Direction.Output, outputPortCapacity, "Output");

        AddVisualElements(inputPort, outputPort);
        Refresh();
    }

    public void AddVisualElements(Port inputPort, Port outputPort)
    {
        // Add ports
        inputContainer.Add(inputPort);
        outputContainer.Add(outputPort);

        // Add content
        for(int i = 0; i < visualElements.Length; i++)
        {
            mainContainer.Add(visualElements[i]);
        }
    }

    private Port AddPort(Direction portDirection, Port.Capacity capacity = Port.Capacity.Single, string portName = "")
    {
        var newPort = InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        newPort.name = portName;
        return newPort;
    }

    private void Refresh()
    {
        RefreshExpandedState();
        RefreshPorts();
    }
}
