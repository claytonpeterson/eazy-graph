using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public abstract class DynamicInputNode : NodeView
    {
        protected  DynamicInputNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            Add(InputPortButton());
        }

        private Button InputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddInputPort("port " + (Ports.InputPortCount() + 1), Port.Capacity.Single);
                Refresh();
            })
            { text = "Add Input Port" };
        }
    }
}
