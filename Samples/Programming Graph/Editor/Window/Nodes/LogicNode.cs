using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class LogicNode : NodeView, IContainsValue, IUpdate
    {
        public LogicNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "logic";

            mainContainer.style.backgroundColor = Color.cyan;

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
