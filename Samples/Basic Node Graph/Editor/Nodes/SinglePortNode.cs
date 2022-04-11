using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SinglePortNode : NodeView
{
    public SinglePortNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.red;
    }

    protected override PortInformation GetPortInformation()
    {
        var portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Multi
        };
        return portInfo;
    }
}
