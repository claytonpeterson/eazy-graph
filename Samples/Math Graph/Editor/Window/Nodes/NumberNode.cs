using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace skybirdgames.eazygraph.Samples.Math.Editor
{
    public class NumberNode : NodeView, IContainsValue
    {
        FloatField numberField;

        public NumberNode(Vector2 position, TestingOutData data) : base(position, data)
        {
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
                value = data.age
            };

            numberField.RegisterValueChangedCallback((evt) =>
            {
                data.age = (int)evt.newValue;
                Debug.Log(evt.newValue);
            });

            Add(numberField);
        }
    }
}