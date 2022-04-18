using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Editor
{
    public class DynamicInputOutputNode : NodeView
    {
        public DynamicInputOutputNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            Add(InputPortButton());
            Add(OutputPortButton());
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        protected override void SetupPorts()
        {
            throw new System.NotImplementedException();
        }

        private Button InputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddInputPort("port " + (Ports.InputPortCount() + 1), Port.Capacity.Single);
            })
            { text = "Add Input Port" };
        }

        private Button OutputPortButton()
        {
            return new Button(clickEvent: () =>
            {
                Ports.AddOutputPort("port " + (Ports.OutputPortCount() + 1), Port.Capacity.Single);
            })
            { text = "Add Output Port" };
        }
    }
}
