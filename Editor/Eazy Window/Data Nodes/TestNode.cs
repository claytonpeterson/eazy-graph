using UnityEngine;

public class TestNode : NodeView
{
    public TestNode(Vector2 position) : base(position)
    {
    }

    protected override PortInformation GetPortInformation()
    {
        throw new System.NotImplementedException();
    }
}
