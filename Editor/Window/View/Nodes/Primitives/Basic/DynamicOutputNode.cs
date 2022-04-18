using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public abstract class DynamicOutputNode : NodeView
    {
        protected DynamicOutputNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            Add(OutputPortButton());
        }

        private Button OutputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddOutputPort("port " + (Ports.OutputPortCount() + 1), Port.Capacity.Single);
                Refresh();
            })
            { text = "Add Output Port" };
        }
    }
}
