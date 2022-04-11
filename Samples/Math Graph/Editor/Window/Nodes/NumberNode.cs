using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class NumberNode : NodeView
    {
        public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            mainContainer.style.backgroundColor = Color.green;

            Add(new FloatField("number"));

            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Single
            };

            inputContainer.Add(
                child: CreatePort(Direction.Input, portInfo.InputPortCapacity, "Input"));

            outputContainer.Add(
                child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));

            Refresh();
        }
    }
}