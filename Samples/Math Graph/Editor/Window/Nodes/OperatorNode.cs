using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : SinglePortNode
    {
        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.blue;
        }
    }
}
