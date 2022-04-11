using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;

public class BigTestNode : NodeView
{
    public BigTestNode(Vector2 position) : base(position)
    {
        title = "Big Test Node";

        mainContainer.style.backgroundColor = Color.red;

        Add(new FloatField());
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
