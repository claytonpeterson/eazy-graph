using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

[Serializable]
public class BigTestNode : NodeView
{
    public FloatField floatField;

    public BigTestNode(Vector2 position) : base(position)
    {
        title = "Big Test Node";

        mainContainer.style.backgroundColor = Color.red;

        floatField = new FloatField("float field");

        Add(floatField);
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
