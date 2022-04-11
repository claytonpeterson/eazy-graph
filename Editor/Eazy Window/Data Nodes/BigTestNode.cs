using UnityEditor.UIElements;
using UnityEngine;

public class BigTestNode : TestNode
{
    public BigTestNode(Vector2 position, PortInformation portInfo) : base(position, portInfo)
    {
        title = "Big Test Node";

        Add(new FloatField());
    }
}
