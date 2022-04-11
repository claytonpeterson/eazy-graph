using UnityEngine;
using UnityEngine.UIElements;

public class LittleTestNode : TestNode
{
    public float number;

    public LittleTestNode(Vector2 position, PortInformation portInfo) : base(position, portInfo)
    {
        title = "Little Test Node";

        Add(new TextField());
    }
}
