using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class NumberNode : NodeView, IContainsValue, IUpdate
    {
        FloatField numberField;

        private readonly OutputUpdater output;

        public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
        {
            output = new OutputUpdater(this);

            title = "Number";

            mainContainer.style.backgroundColor = Color.green;

            var portInfo = new PortInformation
            {
                InputPortCapacity = Port.Capacity.Single,
                OutputPortCapacity = Port.Capacity.Single
            };

            outputContainer.Add(
                child: CreatePort(Direction.Output, portInfo.OutputPortCapacity, "Output"));

            AddNumberField();
            Refresh();
        }

        public int Value()
        {
            return (int)numberField.value;
        }

        private void AddNumberField()
        {
            numberField = new FloatField()
            {
                value = Data().age
            };

            numberField.RegisterValueChangedCallback((evt) =>
            {
                Data().age = (int)evt.newValue;
                output.UpdateOutputConnections();
            });

            Add(numberField);
        }

        public void Update()
        {
            
        }
    }
}