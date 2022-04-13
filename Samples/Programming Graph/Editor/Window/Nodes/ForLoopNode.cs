using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class ForLoopNode : NodeView, IContainsValue, IUpdate
    {
        public ForLoopNode(Vector2 position, TestingOutData data) : base(position, data)
        {

        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public int Value()
        {
            throw new System.NotImplementedException();
        }
    }
}
