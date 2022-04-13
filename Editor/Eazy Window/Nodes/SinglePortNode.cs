using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SinglePortNode : NodeView
{
    private readonly PortInformation portInfo;

    public SinglePortNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.red;

        portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Single
        };
    }

    protected override void SetupPorts()
    {
        inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));
        inputContainer.Add(
            child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));
        outputContainer.Add(
            child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));
    }
}
