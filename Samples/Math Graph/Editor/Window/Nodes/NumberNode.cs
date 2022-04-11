using UnityEditor.UIElements;
using UnityEngine;

public class NumberNode : SinglePortNode
{
    public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
    {
        mainContainer.style.backgroundColor = Color.green;

        Add(new FloatField("number"));
    }
}
