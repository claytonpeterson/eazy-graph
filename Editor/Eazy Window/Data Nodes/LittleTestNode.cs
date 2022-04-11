using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class LittleTestNode : NodeView
{
    public float number;

    public LittleTestNode(Vector2 position) : base(position)
    {
        title = "Little Test Node";

        mainContainer.style.backgroundColor = Color.green;

        Add(new TextField());
    }

    protected override PortInformation GetPortInformation()
    {
        var portInfo = new PortInformation
        {
            InputPortCapacity = Port.Capacity.Single,
            OutputPortCapacity = Port.Capacity.Single
        };
        return portInfo;
    }
}
