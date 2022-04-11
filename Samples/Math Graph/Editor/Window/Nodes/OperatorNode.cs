using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class OperatorNode : NodeView
    {
        public OperatorNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.blue;

            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Single
            };

            inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input A"));

            inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input B"));

            outputContainer.Add(
                child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));

            Refresh();
        }
    }
}
