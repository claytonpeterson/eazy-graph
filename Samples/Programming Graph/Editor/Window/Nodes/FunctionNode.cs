using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class FunctionNode : NodeView, IContainsValue, IUpdate
    {
        public FunctionNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "function";

            mainContainer.style.backgroundColor = Color.red;
        }

        public void Update()
        {

        }

        public int Value()
        {
            return 1;
        }
    }
}
