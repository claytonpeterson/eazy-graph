using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Programming.Editor
{
    public class ForLoopNode : NodeView, IContainsValue, IUpdate
    {
        public ForLoopNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            title = "for loop";

            mainContainer.style.backgroundColor = Color.blue;

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
