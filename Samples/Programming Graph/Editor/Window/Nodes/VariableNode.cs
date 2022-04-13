using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class VariableNode : NodeView, IContainsValue, IUpdate
    {
        public VariableNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "variable";

            mainContainer.style.backgroundColor = Color.green;

            Refresh();
        }

        public void Update()
        {

        }

        public int Value()
        {
            return 0;
        }
    }
}
