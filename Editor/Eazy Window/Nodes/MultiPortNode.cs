using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MultiPortNode : NodeView
{
    public MultiPortNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.green;

        var portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Multi,
            OutputPortCapacity = Port.Capacity.Multi
        };

        inputContainer.Add(
            child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));

        outputContainer.Add(
            child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));
    }
}
