using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SinglePortNode : NodeView
{
    public SinglePortNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.red;

        var portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Single
        };

        inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));

        inputContainer.Add(
            child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));

        outputContainer.Add(
            child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));
    }
}
