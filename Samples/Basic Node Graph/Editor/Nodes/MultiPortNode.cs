using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MultiPortNode : NodeView
{
    public MultiPortNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.green;
    }

    protected override PortInformation GetPortInformation()
    {
        var portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Multi,
            OutputPortCapacity = Port.Capacity.Multi
        };
        return portInfo;
    }
}
